using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universe;
using OpenAi.Examples;
using UnityEngine.UI;
using System;
public class OnSubmitInputBox : MonoBehaviour
{   
    public InputField inputField;
    GptManager gpt;
    chatgpt_q quest;
    PlayerCtrl2 player;
    UniverseStart dalle;
    void Start()
    {
        quest = GameObject.Find("QuestCompiler").GetComponent<chatgpt_q>();
        gpt = GameObject.Find("GptManager").GetComponent<GptManager>();
        dalle = GameObject.Find("OpenAiChatCompleterV1").GetComponent<UniverseStart>();
    }

    public void OnInputboxSubmit(){
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCtrl2>();    
        if(player.nearNpcRole == "NPC"){
            gpt.DoApiCompletion();
        }
        else if(player.nearNpcRole == "DallE"){
            dalle.TestImageAIDallE();            
        }
        else if(player.nearNpcRole == "Quest"){
            quest.OnclickGPT();
        }
        else{
            Debug.Log("OnSubmit:주변에 대화 가능한 NPC가 없습니다");
        }
        inputField.text ="";
    }
}
