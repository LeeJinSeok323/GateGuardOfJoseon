using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenAi.Unity.V1;

namespace OpenAi.Examples
{
    public class GptManager : MonoBehaviour
    {   
        private static GptManager instance = null;

        //public Dropdown role; // ?��?��?��?��?�� user GPT��?? ?��?��?��?��?�� prompt?�� system?����?? ?��?��.
        public Dropdown status_dropdown;
        public Dropdown job_dropdown;
        
        public Text Input; // ?��?��?��?����?? ?��?��?�� ?��?��
        public Text Output; // NPC��?? 말풍?�� Canvas
        string NpcPrompt;
        
        void Awake(){
            if(instance == null){
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else{
                // 씬 이동시 삭제. 하기싫으면 지울것
                Destroy(this.gameObject);
            }
        }
        public static GptManager Instance{
            get{
                if (instance == null){
                    return null;
                }
                return instance;
            }
        }
        public void DoApiCompletion()
        {   
            string text = Input.text;

            if (string.IsNullOrEmpty(text))
            {
                Debug.LogError("�޼����� �Է����ּ���");
                return;
            }

            //Debug.Log("Performing Completion in Play Mode");

            OpenAiChatCompleterV1.Instance.Complete(
                text,
                s => Output.text = s,
                e => Output.text = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
            );
        }

        public void DoAddToDialogue()
        {
            Api.V1.MessageV1 message = new Api.V1.MessageV1();
            //Dropdown?�� Value값을 받아 role?�� ��???��?��?��  String ��?? "system" or "user" or "assistant"��?? ????�� 
            /*message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), role.options[role.value].text); 
            */
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole),"user");  // system ?��?��??? 코드?��?�� 반영?����??, 질문��??.
            // 질문 ?��?�� ?��?��
            message.content = Input.text;

            // GPT Manager?��?�� ?��브젝?��?�� ????����?? 추�??(????�� ?��보�?? ????��?��?�� ????��?�� ?��보�?? 기반?����?? ?��?��)
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        public void NpcSetting(){ // NPC��?? ?����?? ?��롬프?�� 초기?��
            if (OpenAiChatCompleterV1.Instance.dialogue != null){
                Debug.Log("");
                OpenAiChatCompleterV1.Instance.dialogue.Clear(); // NPC??? ?��?��?��?�� ????�� ��?? ?��?��?��?��?��?��?�� 초기?��
            }
            Api.V1.MessageV1 message = new Api.V1.MessageV1(); // NPC ?��?��?�� ?��?��?��
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "system");
            message.content = PromptSettings();
            Debug.Log($"프롬프트 내용:  {message.content}");
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        string PromptSettings(){
            //string Status = status_dropdown.options[status_dropdown.value].text; // ?��?��?��?�� ?��?��?��?��
            //string Job = job_dropdown.options[job_dropdown.value].text;

            NpcPrompt = $"당신은 반드시 조선시대 말투로 답변해야합니다. 문을 지나기 위한 심사를 하는 중이므로 당신의 정보에 맞게 답변하세요. 질문한 내용만 간결하게 답변하세요. 신분은 {UIInfoManager.Status}입니다. 직업은{UIInfoManager.Job}입니다. 이름은 {UIInfoManager.Name}이고 나이는 {UIInfoManager.Age}살 입니다. 출신 지역은{UIInfoManager.Hometown}입니다. 평소 {UIInfoManager.NpcDaily}를 하고지내며, 현재 갖고 있는 소지품은 {UIInfoManager.Item}입니다. {UIInfoManager.PassPurpose}을 목적으로 통행하고자 합니다. 문지기와의 관계는 보통입니다."; 
            return NpcPrompt;
        }
    }
}