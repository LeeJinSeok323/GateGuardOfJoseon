using BitSplash.AI.GPT; // ChatGPT ���̺귯���� ����ϱ� ���� ���ӽ����̽� ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;
using UnityEngine.Rendering.LookDev;

public class chatgpt_q : MonoBehaviour
{
    public Text AnswerText; // ��ȭ ������ ȭ�鿡 ǥ���� TMP_Text ����
    ChatGPTConversation Conversation; // ChatGPT ��ȭ�� �����ϴ� ����
    //bool askedAgain = true; // ������ �ݺ��ϴ� ���� �����ϱ� ���� �÷��� ����
    public Text TextInput;
    private ParseInputText ps;
    private string InputString;

    Example_q example_Q = null;

    public void OnclickGPT()
    {
        // ��ȭ�� �����ϴ� �ڵ�
        Conversation = ChatGPTConversation.Start(this)
            .System("Answer Only Unity C# class code") // �⺻��� ����
            .MaximumLength(2048) // �ִ� ��ū ���̸� 2048�� ����
            .SaveHistory(false); // ���� ��ȭ ������ ���� ����
        Conversation.Temperature = 0.7f; // ��ȭ�� â�Ǽ� ���� ����
        Conversation.Top_P = 1f; // �ֻ��� Ȯ�� ����
        Conversation.Presence_Penalty = 0f; // ���� �ݺ� ���� ����
        Conversation.Frequency_Penalty = 0f; // �� �ݺ� ���� ����

        InputString = TextInput.text;
        ps = GetComponent<ParseInputText>();
        ps.ParseInput(InputString);
            
        // ��ȭ �����ϱ�
        Conversation.Say(Script_q.tempCode + Script_q.say);
        //AnswerText.text += "GPT: "; // ����� �Է��� ȭ�鿡 ǥ��
    }

    /// <summary>
    /// ��ȭ���� ������ �������� �� ȣ��Ǵ� �޼���
    /// </summary>
    /// <param name="text">���� �ؽ�Ʈ</param>
    void OnConversationResponse(string text)
    {
        string s = text.Replace("csharp", "").Replace("```", "");

        AnswerText.text += s + "\r\n"; // AI ������ ȭ�鿡 ǥ��
        Script_q.scripts = s;

        example_Q = GetComponent<Example_q>();
        example_Q.Compiler();

    }

    /// <summary>
    /// API ȣ�� �� ������ �߻����� �� ȣ��Ǵ� �޼���
    /// </summary>
    /// <param name="text">���� �޽���</param>
    void OnConversationError(string text)
    {
        Debug.Log("Error : " + text); // ���� �޽����� �α׿� ���
        Conversation.RestartConversation(); // ��ȭ�� �����
    }
}
