using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPromptComb : MonoBehaviour
{
    public string prompt;
    public void OnButtonClick(Button btn)
    {
        string content = btn.gameObject.name;
        if(!prompt.Contains(content))
            prompt += content + " ";
    }

    public void ClearPrompt()
    {
        prompt = "";
    }

    public string GetFinalPrompt()
    {
        return prompt.Trim();
    }
}