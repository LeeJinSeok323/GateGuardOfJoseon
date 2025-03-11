using Questdll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUpdate : MonoBehaviour
{
    public Text questText;

    private void Update()
    {
        List<Quest> list = QuestManager.Instance.GetQuests();
        if (list != null)
        {
            string questInfo = "";
            foreach (Quest quest in list)
            {
                questInfo +=
                    $"Quest Title: {quest._Name} \n" +
                    $"------------------------------------ \n"+
                    $"Role: {quest._Role} \n" +
                    $"Progress: {quest._Progress} / {quest._Cnt }\n\n";
            }
            questText.text = questInfo;
        }
        else questText.text = "";
    }
}
