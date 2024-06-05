using System.Collections;
using UnityEngine;
using System.Text;
using UnityEngine.Networking;

public class GoogleTTS : MonoBehaviour
{
    // TTS���� ����� ����� �ҽ�
    private AudioSource mAudio;

    // ���ڿ��� ��� �ٲٱ⿡ ������ ����Ѵ�.
    private StringBuilder mStrBuilder;

    // ���� TTS�� �̿��� �������� �� �ּ�
    private string mPrefixURL;

    void Start()
    {
        mPrefixURL = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=";

        mAudio = GetComponent<AudioSource>();
        mStrBuilder = new StringBuilder();
    }

    // �ܺο��� ȣ��Ǹ� ���ڿ�, �� �޾� �ڷ�ƾ�� �����Ų��.
    public void RunTTS(string text, SystemLanguage language = SystemLanguage.Korean)
    {
        StartCoroutine(DownloadTheAudio(text, language));
    }

    // ������� �ٿ�ε� �޴´�.
    IEnumerator DownloadTheAudio(string text, SystemLanguage language = SystemLanguage.Korean)
    {
        mStrBuilder.Clear();

        // �ؽ�Ʈ �� Origin URL
        mStrBuilder.Append(mPrefixURL);

        // TTS�� ��ȯ�� �ؽ�Ʈ
        mStrBuilder.Append(text);
        mStrBuilder.Replace('\n', '.');

        // ��� �ν��� ���� �±� �߰� &tl=
        mStrBuilder.Append("&tl=");

        // ��� �ĺ�
        switch (language)
        {
            case SystemLanguage.Korean:
                {
                    mStrBuilder.Append("ko-kr");
                    break;
                }

            case SystemLanguage.English:
            default:
                {
                    mStrBuilder.Append("en-gb");
                    break;
                }
        }

        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(mStrBuilder.ToString(), AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                mAudio.clip = DownloadHandlerAudioClip.GetContent(www);
                mAudio.Play();
            }
        }
    }
}
