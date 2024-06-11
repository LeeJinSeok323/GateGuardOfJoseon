using BitSplash.AI.GPT; // ChatGPT 라이브러리를 사용하기 위한 네임스페이스 포함
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class chatgpt_q : MonoBehaviour
{
    public Text AnswerText; // 대화 내용을 화면에 표시할 TMP_Text 변수
    ChatGPTConversation Conversation; // ChatGPT 대화를 관리하는 변수
    //bool askedAgain = true; // 질문을 반복하는 것을 방지하기 위한 플래그 변수
    public Text TextInput;
    private ParseInputText ps;
    private string InputString;

    Example_q example_Q = null;

    public void OnclickGPT()
    {
        // 대화를 시작하는 코드
        Conversation = ChatGPTConversation.Start(this)
            .System("Answer Only Unity C# class code") // 기본출력 설정
            .MaximumLength(2048) // 최대 토큰 길이를 2048로 설정
            .SaveHistory(false); // 이전 대화 내용을 저장 설정
        Conversation.Temperature = 0.7f; // 대화의 창의성 정도 설정
        Conversation.Top_P = 1f; // 최상위 확률 설정
        Conversation.Presence_Penalty = 0f; // 주제 반복 감소 설정
        Conversation.Frequency_Penalty = 0f; // 빈도 반복 감소 설정

        InputString = TextInput.text;
        ps = GetComponent<ParseInputText>();
        ps.ParseInput(InputString);
            
        // 대화 시작하기
        Conversation.Say(Script_q.tempCode + Script_q.say);
        //AnswerText.text += "GPT: "; // 사용자 입력을 화면에 표시
    }

    /// <summary>
    /// 대화에서 응답이 도착했을 때 호출되는 메서드
    /// </summary>
    /// <param name="text">응답 텍스트</param>
    void OnConversationResponse(string text)
    {
        string s = text.Replace("csharp", "").Replace("```", "");

        AnswerText.text += s + "\r\n"; // AI 응답을 화면에 표시
        Script_q.scripts = s;

        example_Q = GetComponent<Example_q>();
        example_Q.Compiler();

    }

    /// <summary>
    /// API 호출 중 오류가 발생했을 때 호출되는 메서드
    /// </summary>
    /// <param name="text">오류 메시지</param>
    void OnConversationError(string text)
    {
        Debug.Log("Error : " + text); // 오류 메시지를 로그에 기록
        Conversation.RestartConversation(); // 대화를 재시작
    }
}
