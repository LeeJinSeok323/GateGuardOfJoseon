using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OpenAi.Unity.V1;
namespace OpenAi.Examples
{
    public class GptManager : MonoBehaviour
    {   
        private static GptManager _instance = null;

        public Text Input; // ???????????????? ????????? ??????
        [SerializeField]
        private Text output; // NPC???? 말풍??? Canvas

        string NpcPrompt;
        GameObject closestNpc;
        GameObject player;
        [SerializeField]
        private TextToSpeech tts;
        public float detectionRadius = 5f; // 콜라이더 감지 반경
         [SerializeField]
        private Color gizmoColor = Color.yellow; 
        string previousText = "";
        public static GptManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<GptManager>();
                    singletonObject.name = typeof(GptManager).ToString() + " (Singleton)";

                }
                return _instance;
            }
        }

        private void Awake()
        {
            // 만약 인스턴스가 이미 존재하고 현재 인스턴스와 다르다면, 중복된 인스턴스를 파괴
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        void Start(){
            player = GameObject.FindWithTag("Player");
            GameObject googleTTSObject = GameObject.Find("GoogleTTSTest");
            if(googleTTSObject != null)
                tts = googleTTSObject.GetComponent<TextToSpeech>();
            else
                Debug.Log("TTS 못찾음");

            if(tts == null)
                Debug.Log("컴포넌트 못찾음");
            if (player == null)
            {
                Debug.LogError("Player not found. Make sure the player has the 'Player' tag.");
            }
            FindNearestTextComponent();

        }
        private float timeElapsed = 0f;
        void Update(){
            timeElapsed += Time.deltaTime;
            if(timeElapsed > 2f){
                timeElapsed = 0f;
                Debug.Log(timeElapsed);
                FindNearestTextComponent();
            }
            //playTTS();
        }
        void playTTS(){
            if(output != null){
                if(output.text != previousText){
                    tts.uiText.text = output.text;
                    tts.OnPlayButtonClicked();
                    previousText = output.text;
                }
            }
        }
        void FindNearestTextComponent()
        {
            if (player == null) return;

            Collider[] colliders = Physics.OverlapSphere(player.transform.position, detectionRadius);
            float nearestDistance = float.MaxValue;
            Text nearestText = null;

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == player) continue;

                Text[] texts = collider.GetComponentsInChildren<Text>();
                foreach (Text text in texts)
                {
                    float distance = Vector3.Distance(player.transform.position, text.transform.position);
                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;
                        nearestText = text;
                    }
                }
            }

            if (nearestText != null)
            {
                output = nearestText;
                Debug.Log($"Found nearest Text component: {output.name}");
                nearestText =null;
            }
            else
            {
                output = null;
                Debug.LogWarning("No Text component found in nearby Colliders");
            }
        }

     
        private void OnDrawGizmos()
        {
            if (player != null)
            {
                Gizmos.color = gizmoColor;
                Gizmos.DrawWireSphere(player.transform.position, detectionRadius);
            }
        }
        public void DoApiCompletion()
        {   
            string text = Input.text;
            
            if (string.IsNullOrEmpty(text))
            {
                Debug.LogError("??????? ??????????");
                return;
            }

            //Debug.Log("Performing Completion in Play Mode");

            OpenAiChatCompleterV1.Instance.Complete(
                text,
                s => output.text = s,
                e => output.text = $"ERROR: StatusCode: {e.responseCode} - {e.error}"
            );

            
        }

        public void DoAddToDialogue()
        {
            Api.V1.MessageV1 message = new Api.V1.MessageV1();
            //Dropdown??? Value값을 받아 role??? ?????????????  String ???? "system" or "user" or "assistant"???? ?????? 
            /*message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), role.options[role.value].text); 
            */
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole),"user");  // system ????????? 코드?????? 반영???????, 질문????.
            // 질문 ?????? ??????
            message.content = Input.text;

            // GPT Manager?????? ???브젝?????? ?????????? 추???(?????? ???보??? ???????????? ????????? ???보??? 기반??????? ??????)
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        public void NpcSetting(){ // NPC???? ??????? ???롬프??? 초기???
            if (OpenAiChatCompleterV1.Instance.dialogue != null){
                Debug.Log("");
                OpenAiChatCompleterV1.Instance.dialogue.Clear(); // NPC??? ???????????? ?????? ???? ????????????????????? 초기???
            }
            Api.V1.MessageV1 message = new Api.V1.MessageV1(); // NPC ????????? ?????????
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "system");
            message.content = PromptSettings();
            Debug.Log($"프롬프트 내용:  {message.content}");
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        string PromptSettings(){
            //string Status = status_dropdown.options[status_dropdown.value].text; // ???????????? ????????????
            //string Job = job_dropdown.options[job_dropdown.value].text;
    
    
            NpcPrompt = $"당신은 반드시 조선시대 말투로 답변해야합니다. 문을 지나기 위한 심사를 하는 중이므로 당신의 정보에 맞게 답변하세요. 질문한 내용만 간결하게 답변하세요. 신분은 {UIInfoManager.Status}입니다. 직업은{UIInfoManager.Job}입니다. 이름은 {UIInfoManager.Name}이고 나이는 {UIInfoManager.Age}살 입니다. 출신 지역은{UIInfoManager.Hometown}입니다. 평소 {UIInfoManager.NpcDaily}를 하고지내며, 현재 갖고 있는 소지품은 {UIInfoManager.Item}입니다. {UIInfoManager.PassPurpose}을 목적으로 통행하고자 합니다. 문지기와의 관계는 보통입니다."; 
            //NpcPrompt += "  플레이어 요청에 따라 응답 형식 구분: 1. 도움 요청 → Quest@ + + 대답 3.일반 질문 → Text@ + 대답";
            return NpcPrompt;
        }
    }
}