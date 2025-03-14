using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Threading.Tasks;
namespace CoroutineVariant
{

public class TextAI : MonoBehaviour
{
    // Same as TextAI.cs but using coroutines instead of async await.

    public static string key = null;

    const int callCountMaxForSecurity = 150;
    static int callCount = 0;

    public IEnumerator GetAnswer(string prompt, System.Action<string> callback, bool useCache = false, string cacheKey = null, float secondsDelay = 0f, int maxTokens = 100, string[] stop = null, float temperature = 0.7f, float presencePenalty = 0f, float frequencyPenalty = 0f, string suffix = null, bool showResultInfo = false, int responseLengthMaxGoal = 0, string model = TextAIParams.defaultModel, string endpoint = TextAIParams.defaultEndpointAnswer)
    {
        return GetCompletion(prompt, callback, useCache, cacheKey, secondsDelay, maxTokens, stop, temperature, presencePenalty, frequencyPenalty, suffix, showResultInfo, responseLengthMaxGoal, model, endpoint);
    }

    public IEnumerator GetCompletion(string prompt, System.Action<string> callback, bool useCache = false, string cacheKey = null, float secondsDelay = 0f, int maxTokens = 100, string[] stop = null, float temperature = 0.7f, float presencePenalty = 0f, float frequencyPenalty = 0f, string suffix = null, bool showResultInfo = false, int responseLengthMaxGoal = 0, string model = TextAIParams.defaultModel, string endpoint = TextAIParams.defaultEndpointCompletion)
    {
        TextAIParams aiParams = new TextAIParams()
        {
            prompt = prompt,
            maxTokens = maxTokens,
            temperature = temperature,
            presencePenalty = presencePenalty,
            frequencyPenalty = frequencyPenalty,
            stop = stop,
            suffix = suffix,
            model = model,
            endpoint = endpoint
        };

        if (responseLengthMaxGoal > 0)
        {
            aiParams.maxTokens = GetApproximateTokensNeededForResponseLength(prompt, responseLengthMaxGoal);
        }

        return GetCompletion(callback, aiParams, useCache, cacheKey, secondsDelay, showResultInfo);
    }

    public IEnumerator GetCompletion(System.Action<string> callback, TextAIParams aiParams, bool useCache = false, string cacheKey = null, float secondsDelay = 0f, bool showResultInfo = false)
    {
        if (secondsDelay > 0f)
        {
            yield return new WaitForSeconds(secondsDelay);
        }
        
        Cache cache = new Cache("TextAI", "json");

        string cacheContent = null;
        if (useCache)
        {
            if (cacheKey == null) { cacheKey = aiParams.prompt; }
            cacheKey = Cache.ToKey(cacheKey);
            cacheContent = cache.GetText(cacheKey);
            if (!string.IsNullOrEmpty(cacheContent))
            {
                string result = GetResultFromJsonString(cacheContent,
                    showResultInfo: showResultInfo, cacheWasUsed: true);
                callback?.Invoke(result);
            }
        }

        if (string.IsNullOrEmpty(cacheContent))
        {
            aiParams.CapMaxTokensToAllowed();

            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("TextAI OpenAI.com key not set.");
                yield return null;
            }            
            else if (callCount < callCountMaxForSecurity)
            {
                callCount++;

                var serializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };

                if (aiParams.messages == null && aiParams.endpoint.Contains("chat"))
                {
                    aiParams.messages = new List<TextAIMessage>
                    {
                        // new TextAIMessage("system", "You are a helpful assistant."),
                        new TextAIMessage("user", aiParams.prompt)
                    };
                    aiParams.prompt = null;
                }

                string jsonString = JsonConvert.SerializeObject(aiParams, Formatting.None, serializerSettings);
                // Debug.Log(jsonString);

                string apiUrl = "https://api.openai.com" + aiParams.endpoint;
                UnityWebRequest www = UnityWebRequest.PostWwwForm(apiUrl, "");
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("Authorization", "Bearer " + key);
                www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonString));
                www.downloadHandler = new DownloadHandlerBuffer();

                yield return www.SendWebRequest();
                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                    www.Dispose();
                    yield return null;
                }
                else
                {
                    string text = www.downloadHandler.text;
                    string result = GetResultFromJsonString(text, showResultInfo: showResultInfo);
                    if (useCache) { cache.SetText(cacheKey, text); }

                    www.Dispose();

                    callback?.Invoke(result);
                }
            }
            else
            {
                Debug.Log("OpenAI Call count limit reached.");
                yield return null;
            }
        }
    }

    string GetResultFromJsonString(string jsonString, bool showResultInfo = false, bool cacheWasUsed = false)
    {
        string result = null;

        var jsonData = JsonConvert.DeserializeObject(jsonString) as Newtonsoft.Json.Linq.JObject;
        try
        {
            if (jsonData.SelectToken("choices[0]") == null)
            {
                Debug.LogWarning("GetResultFromJsonString found no result.");
                Debug.Log(jsonString);
                return null;
            }
            else
            {
                if (jsonData.SelectToken("choices[0].message") != null)
                {
                    result = jsonData.SelectToken("choices[0].message.content").ToString();
                }
                else
                {
                    result = jsonData.SelectToken("choices[0].text").ToString();
                }

                if (showResultInfo)
                {
                    Debug.Log("- Finish reason: " + jsonData.SelectToken("choices[0].finish_reason").ToString());
                    int promptTokens     = jsonData.SelectToken("usage.prompt_tokens").    ToObject<int>();
                    int completionTokens = jsonData.SelectToken("usage.completion_tokens").ToObject<int>();

                    // Price is here and may change: https://openai.com/api/pricing/
                    // Below only reflects the GPT-4/ 8k model at the time of writing.
                    const float promptCostPerThousandInUsd = 0.03f;
                    const float completionCostPerThousandInUsd = 0.06f;
                    float usd = (promptTokens     * promptCostPerThousandInUsd +
                                 completionTokens * completionCostPerThousandInUsd) * 0.001f;

                    string info = promptTokens + " prompt tokens + " + 
                        completionTokens + " completion tokens = " +
                        "$" + usd;
                    if (cacheWasUsed) { info += " (but $0 as retrieved from cache)"; }
                    
                    Debug.Log(info);
                }
            }
        }
        catch (System.Exception exception)
        {
            Debug.LogWarning(exception.Message);
            Debug.Log(jsonString);
        }

        return result;
    }

    public static int GetApproximateTokensNeededForResponseLength(string prompt, int responseLengthGoal)
    {
        const int approximateTokenToCharRatio = 4;
        return (prompt.Length + responseLengthGoal) / approximateTokenToCharRatio;
    }
}

}