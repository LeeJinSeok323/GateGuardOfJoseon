using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenAi.Unity.V1;


namespace OpenAi.Examples
{
    public class GptManager : MonoBehaviour
    {
        //public Dropdown role; // 플레이어는 user GPT가 학습해야할 prompt는 system으로 설정.
        public Dropdown status_dropdown;
        public Dropdown job_dropdown;

        public Text Input; // 플레이어가 입력할 내용
        public Text Output; // NPC별 말풍선 Canvas
        string NpcPrompt;

        public void DoApiCompletion()
        {
            string text = Input.text;

            if (string.IsNullOrEmpty(text))
            {
                Debug.LogError("대화 내용을 입력해주세요.");
                return;
            }

            //Debug.Log("Performing Completion in Play Mode");

            Output.text = "Perform Completion...";
            OpenAiChatCompleterV1.Instance.Complete(
                text,
                s => Output.text = s,
                e => Output.text = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
            );
            Debug.Log(Output.text);
        }

        public void DoAddToDialogue()
        {
            Api.V1.MessageV1 message = new Api.V1.MessageV1();
            //Dropdown의 Value값을 받아 role을 지정하여  String 값 "system" or "user" or "assistant"로 저장 
            /*message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), role.options[role.value].text); 
            */
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "user");  // system 입력은 코드에서 반영하고, 질문만.
            // 질문 내용 입력
            message.content = Input.text;

            // GPT Manager역할 오브젝트에 대화를 추가(대화 정보를 저장하여 저장된 정보를 기반으로 응답)
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        public void NpcSetting()
        { // NPC별 정보 프롬프트 초기화
            if (OpenAiChatCompleterV1.Instance.dialogue != null)
            {
                Debug.Log("이전 NPC 대화정보 초기화");
                OpenAiChatCompleterV1.Instance.dialogue.Clear(); // NPC와 나누었던 대화 및 사전입력데이터 초기화
            }
            Api.V1.MessageV1 message = new Api.V1.MessageV1(); // NPC 데이터 재생성
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "system");
            message.content = PromptSettings();
            Debug.Log($"프롬프트{message.content}");
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        string PromptSettings()
        {
            //string Status = status_dropdown.options[status_dropdown.value].text; // 드랍다운 테스팅용
            //string Job = job_dropdown.options[job_dropdown.value].text;

            NpcPrompt = $"당신은 조선시대 {UIInfoManager.Status}신분으로 {UIInfoManager.Job}직업 역할에 적합한 말투로 문지기의 질문에 대답해야 합니다. 이름은{UIInfoManager.Name}이며 나이는 {UIInfoManager.Age}이고 {UIInfoManager.Hometown}출신입니다. 일상 설명:{UIInfoManager.NpcDaily} 소지 물건:{UIInfoManager.Item} 통행 목적:{UIInfoManager.PassPurpose} 문지기와의 관계: 매우나쁨";
            return NpcPrompt;
        }
    }
}