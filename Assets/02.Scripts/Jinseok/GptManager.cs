using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenAi.Unity.V1;
using NpcInfoManagerSpace;

namespace OpenAi.Examples
{
    public class GptManager : MonoBehaviour
    {
        public Dropdown status_dropdown;
        public Dropdown job_dropdown;

        public Text Input; // 플레이어가 입력할 내용
        public Text Output; // NPC별 말풍선 Canvas
        string NpcPrompt;

        private GoogleTTS googleTTS;

        void Start()
        {
            googleTTS = GetComponent<GoogleTTS>();
        }

        public void DoApiCompletion()
        {
            string text = Input.text;

            if (string.IsNullOrEmpty(text))
            {
                Debug.LogError("대화 내용을 입력해주세요.");
                return;
            }

            Debug.Log("Performing Completion in Play Mode");

            Output.text = "Perform Completion...";
            OpenAiChatCompleterV1.Instance.Complete(
                text,
                s => {
                    Output.text = s;
                    googleTTS.RunTTS(s, SystemLanguage.Korean); // GPT의 응답을 TTS로 재생
                },
                e => Output.text = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
            );
        }

        public void DoAddToDialogue()
        {
            Api.V1.MessageV1 message = new Api.V1.MessageV1();
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "user");
            message.content = Input.text;

            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        public void NpcSetting()
        {
            if (OpenAiChatCompleterV1.Instance.dialogue != null)
            {
                Debug.Log("클리어");
                OpenAiChatCompleterV1.Instance.dialogue.Clear();
            }
            Api.V1.MessageV1 message = new Api.V1.MessageV1();
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "system");
            message.content = PromptSettings();
            Debug.Log(message.role);
            Debug.Log(message.content);
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        string PromptSettings()
        {
            NpcPrompt = $"당신은 조선시대 {UIInfoManager.Status}신분으로 {UIInfoManager.Job}직업 역할에 적합한 말투로 문지기의 질문에 대답해야 합니다. 이름은{UIInfoManager.Name}이며 나이는 {UIInfoManager.Age}이고 {UIInfoManager.Hometown}출신입니다. 일상 설명:{UIInfoManager.NpcDaily} 소지 물건:{UIInfoManager.Item} 통행 목적:{UIInfoManager.PassPurpose} 문지기와의 관계: 매우나쁨";
            return NpcPrompt;
        }
    }
}
