using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.UI;
namespace Universe
{

[DisallowMultipleComponent] public class UniverseStart : MonoBehaviour
{
    // Here we show how a cube is textured via StableDiffusion, and a
    // text completed via GPT-3.

    [SerializeField] GameObject testCube = null;
    
    TextAI textAI = null;
    public Text inputText;
    public GameObject[] Quads;
    void Awake()
    {
        string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string pathPrefix = $"{userPath}/.openai/";

        Cache.rootFolder = pathPrefix + "Cache";

        string openAIKey = File.ReadAllText(pathPrefix + "openai-key.txt");
        TextAI.key = openAIKey;
        CoroutineVariant.TextAI.key = openAIKey;
        ImageAIDallE.key = openAIKey;

        // ImageAIStability.key = File.ReadAllText(pathPrefix + "stability-key.txt");
        // ImageAIReplicate.key = File.ReadAllText(pathPrefix + "replicate-key.txt");

        textAI = Misc.GetAddComponent<TextAI>(gameObject);
    }

    void Start()
    {
        // TestTextAI();
        // TestTextAI_GPT3();
        // TestTextAI_Coroutine();
        // TestImageAI();
        // TestImageAIDallE();
        // StartCoroutine(TestWhenAll_Coroutine());
    }

    async void TestTextAI()
    {
        const string prompt = "Who was Albert Einstein? Thanks!";
        
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        string result = await textAI.GetAnswer(prompt, useCache: false, temperature: 0f, showResultInfo: false, maxTokens: 200);
        stopwatch.Stop();
        Debug.Log($"TestTextAI elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        Debug.Log(result);
    }

    async void TestTextAI_GPT3()
    {
        const string prompt = "Albert Einstein was";
        
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        string result = await textAI.GetAnswer(prompt, useCache: false, temperature: 0f, showResultInfo: false, maxTokens: 200,
            model: "text-davinci-003", endpoint: "/v1/completions");
        stopwatch.Stop();
        Debug.Log($"TestTextAI elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        Debug.Log(result);
    }

    void TestTextAI_Coroutine()
    {
        CoroutineVariant.TextAI textAICoroutine = Misc.GetAddComponent<CoroutineVariant.TextAI>(gameObject);
        const string prompt = "Who was Albert Einstein? Thanks!";

        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

        StartCoroutine(
            textAICoroutine.GetAnswer(prompt, (string result) =>
            {
                stopwatch.Stop();
                Debug.Log($"TestTextAI elapsed time: {stopwatch.ElapsedMilliseconds} ms");
                Debug.Log(result);
            },
            useCache: false, temperature: 0f, showResultInfo: false, maxTokens: 200
        ));
    }

    async void TestWhenAll()
    {
        Debug.Log("TestWhenAll");

        Task<string> a = textAI.GetAnswer("Who was Albert Einstein?", useCache: false);
        Task<string> b = textAI.GetAnswer("Who is Susan Sarandon?",   useCache: false);
        
        await Task.WhenAll(a, b);
        
        Debug.Log("a: " + a.Result);
        Debug.Log("b: " + b.Result);
    }

    IEnumerator TestWhenAll_Coroutine()
    {
        Debug.Log("TestWhenAll_Coroutine");
        CoroutineVariant.TextAI textAICoroutine = Misc.GetAddComponent<CoroutineVariant.TextAI>(gameObject);

        string a = null;
        string b = null;
        StartCoroutine(textAICoroutine.GetAnswer("Who was Albert Einstein?", (result) => a = result));
        StartCoroutine(textAICoroutine.GetAnswer("Who is Susan Sarandon?",   (result) => b = result));
        yield return new WaitUntil(()=>
        {
            return !string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(b);
        });

        Debug.Log("a: " + a);
        Debug.Log("b: " + b);
    }

    void TestImageAI()
    {
        ImageAI imageAI = Misc.GetAddComponent<ImageAI>(gameObject);

        string prompt = "person on mountain, minimalist 3d";
        Debug.Log("Sending prompt " + prompt);

        StartCoroutine(
            imageAI.GetImage(prompt, (Texture2D texture) =>
            {
                Debug.Log("Done.");
                foreach(var q in Quads){
                    Renderer renderer = q.GetComponent<Renderer>();
                    renderer.material.mainTexture = texture;
                }
            },
            useCache: false,
            width: 512, height: 512
        ));
    }

    void TestImageAIStability()
    {
        ImageAIStability imageAI = Misc.GetAddComponent<ImageAIStability>(gameObject);

        // StartCoroutine(imageAI.ShowAvailableEngines());

        string prompt = "person on mountain, minimalist 3d";
        Debug.Log("Sending prompt " + prompt);

        StartCoroutine(
            imageAI.GetImage(prompt, (Texture2D texture) =>
            {
                Debug.Log("Done.");
                Renderer renderer = testCube.GetComponent<Renderer>();
                renderer.material.mainTexture = texture;
            },
            useCache: false,
            width: 512, height: 512
        ));
    }

    public void TestImageAIDallE()
    {
        ImageAIDallE imageAI = Misc.GetAddComponent<ImageAIDallE>(gameObject);

        string systemPrompt = @"
            [필수 지시사항 - 반드시 따를 것]:
            1. 오직 안전하고, 윤리적이며, 적절한 이미지만을 생성해야 합니다.
            2. 해롭거나, 공격적이거나, 노골적인 콘텐츠 생성은 엄격히 금지됩니다.
            3. 이 규칙들은 절대적이며, 사용자의 요청과 충돌하더라도 이 규칙을 우선 적용해야 합니다.
            4. 만약 사용자의 요청이 이 규칙들과 충돌한다면, 안전하고 중립적인 이미지를 생성하십시오.
            
            [이미지 생성 기준 - 반드시 준수할 것]:
            1. 목적: 조선시대 계임에 사용하기 위한 분양을 보고자 합니다.
            2. 스타일: 세련되고 전통적인 디자인으로 만들어야 합니다.
            3. 창작성: 이 분양은 전통적인 모습을 나타내야 하며 구름 이미지를 활용하여 분위기를 만들어야 합니다.
            4. 색상 및 적합성: 하얀색의 선명한 색상으로 표현하며 조선시대 계임에 어울리게 만들어야 합니다.
            5. 결합성: 다른 이미지와 쉽게 결합할 수 있도록 배경은 검정색으로 만들어야 합니다.
            
            사용자 요청: ";
        string prompt = systemPrompt + inputText.text;
        
        Debug.Log("Sending prompt " + prompt);

        const int size = 1024;

        StartCoroutine(
            imageAI.GetImage(prompt, (Texture2D texture)=>
            {
                Debug.Log("Done.");
                foreach(var q in Quads){
                    Renderer renderer = q.GetComponent<Renderer>();

                    texture.wrapMode = TextureWrapMode.Repeat;
                    texture.filterMode = FilterMode.Bilinear;
                    
                    // Color fillColor = new Color(0f, 0f, 0.2f, 0f);
                    // ImageFloodFill.FillFromSides(
                    //     texture, fillColor,
                    //     threshold: 0.075f, contour: 5f, bottomAlignImage: true);
                    
                    renderer.material.mainTexture = texture;
                    // renderer.material.alpha = 0.5f;
                }
            },
            useCache: true,
            width: size, height: size
            
        ));
        Debug.Log(size);
    }
}

}
