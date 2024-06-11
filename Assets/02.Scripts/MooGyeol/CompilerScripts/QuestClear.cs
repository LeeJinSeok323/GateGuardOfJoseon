using Questdll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestClear : MonoBehaviour
{
    public void ClickButtom()
    {
        QuestManager.Instance.CompleteQuest("test");
    }

};
