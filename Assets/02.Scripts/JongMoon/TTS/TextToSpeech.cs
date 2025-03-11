using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Text;

public class TextToSpeech : MonoBehaviour
{
    public Text uiText; // UI Text ������Ʈ�� �����մϴ�.
    public Button playButton; // Button ������Ʈ�� �����մϴ�.
    private string apiKey = "AIzaSyCrV3rmAJ9poj76ORk1ul4N8OR_Nlqt1Hs"; // Google Cloud API Ű
    public GameObject GptManager;
    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �޼��� ���
        playButton.onClick.AddListener(OnPlayButtonClicked);
    }

    void Update(){
        // uiText = GptManager.Output;
    }
    public void OnPlayButtonClicked()
    {
        // �ؽ�Ʈ�� �о TTS ȣ��
        StartCoroutine(CallGoogleTTS(uiText.text));
    }

    IEnumerator CallGoogleTTS(string text)
    {
        string url = $"https://texttospeech.googleapis.com/v1/text:synthesize?key={apiKey}";

        var requestData = new SynthesizeRequest()
        {
            input = new Input() { text = text },
            voice = new VoiceSelectionParams() { languageCode = "ko-KR", ssmlGender = "MALE" },
            audioConfig = new AudioConfig() { audioEncoding = "LINEAR16" }
        };

        string json = JsonUtility.ToJson(requestData);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var response = JsonUtility.FromJson<SynthesizeResponse>(request.downloadHandler.text);
                byte[] audioBytes = Convert.FromBase64String(response.audioContent);
                PlayAudioClip(audioBytes);
            }
        }
    }

    void PlayAudioClip(byte[] audioBytes)
    {
        AudioClip audioClip = WavUtility.ToAudioClip(audioBytes);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    [Serializable]
    public class SynthesizeRequest
    {
        public Input input;
        public VoiceSelectionParams voice;
        public AudioConfig audioConfig;
    }

    [Serializable]
    public class Input
    {
        public string text;
    }

    [Serializable]
    public class VoiceSelectionParams
    {
        public string languageCode;
        public string ssmlGender;
    }

    [Serializable]
    public class AudioConfig
    {
        public string audioEncoding;
    }

    [Serializable]
    public class SynthesizeResponse
    {
        public string audioContent;
    }
}
