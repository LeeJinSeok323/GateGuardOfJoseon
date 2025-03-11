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
        private Text output; // NPC???? ��ǳ??? Canvas

        string NpcPrompt;
        GameObject closestNpc;
        GameObject player;
        [SerializeField]
        private TextToSpeech tts;
        public float detectionRadius = 5f; // �ݶ��̴� ���� �ݰ�
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
            // ���� �ν��Ͻ��� �̹� �����ϰ� ���� �ν��Ͻ��� �ٸ��ٸ�, �ߺ��� �ν��Ͻ��� �ı�
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
                Debug.Log("TTS ��ã��");

            if(tts == null)
                Debug.Log("������Ʈ ��ã��");
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
            //Dropdown??? Value���� �޾� role??? ?????????????  String ???? "system" or "user" or "assistant"???? ?????? 
            /*message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), role.options[role.value].text); 
            */
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole),"user");  // system ????????? �ڵ�?????? �ݿ�???????, ����????.
            // ���� ?????? ??????
            message.content = Input.text;

            // GPT Manager?????? ???����?????? ?????????? ��???(?????? ???��??? ???????????? ????????? ???��??? ���??????? ??????)
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        public void NpcSetting(){ // NPC???? ??????? ???����??? �ʱ�???
            if (OpenAiChatCompleterV1.Instance.dialogue != null){
                Debug.Log("");
                OpenAiChatCompleterV1.Instance.dialogue.Clear(); // NPC??? ???????????? ?????? ???? ????????????????????? �ʱ�???
            }
            Api.V1.MessageV1 message = new Api.V1.MessageV1(); // NPC ????????? ?????????
            message.role = (Api.V1.MessageV1.MessageRole)System.Enum.Parse(
                typeof(Api.V1.MessageV1.MessageRole), "system");
            message.content = PromptSettings();
            Debug.Log($"������Ʈ ����:  {message.content}");
            OpenAiChatCompleterV1.Instance.dialogue.Add(message);
        }

        string PromptSettings(){
            //string Status = status_dropdown.options[status_dropdown.value].text; // ???????????? ????????????
            //string Job = job_dropdown.options[job_dropdown.value].text;
    
    
            NpcPrompt = $"����� �ݵ�� �����ô� ������ �亯�ؾ��մϴ�. ���� ������ ���� �ɻ縦 �ϴ� ���̹Ƿ� ����� ������ �°� �亯�ϼ���. ������ ���븸 �����ϰ� �亯�ϼ���. �ź��� {UIInfoManager.Status}�Դϴ�. ������{UIInfoManager.Job}�Դϴ�. �̸��� {UIInfoManager.Name}�̰� ���̴� {UIInfoManager.Age}�� �Դϴ�. ��� ������{UIInfoManager.Hometown}�Դϴ�. ��� {UIInfoManager.NpcDaily}�� �ϰ�������, ���� ���� �ִ� ����ǰ�� {UIInfoManager.Item}�Դϴ�. {UIInfoManager.PassPurpose}�� �������� �����ϰ��� �մϴ�. ��������� ����� �����Դϴ�."; 
            //NpcPrompt += "  �÷��̾� ��û�� ���� ���� ���� ����: 1. ���� ��û �� Quest@ + + ��� 3.�Ϲ� ���� �� Text@ + ���";
            return NpcPrompt;
        }
    }
}