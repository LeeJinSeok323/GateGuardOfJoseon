using Questdll;
using UnityEngine;

public class QuestAddTest : MonoBehaviour
{
    private string prefabPath = "Prefabs/";

    private GameObject prefab;

    Quest Q1 = new Quest(
        Script_q._Name,
        Script_q._Role,
        Script_q._Cnt
        );

    void Start()
    {
        prefabPath += Script_q._Obj;
        prefab = Resources.Load<GameObject>(prefabPath);
        QuestManager.Instance.AddQuest(Q1);

    }

    void Update()
    {
        if (QuestManager.Instance.IsClear(Q1._Name, Q1._Cnt))
        {
            QuestManager.Instance.CompleteQuest(Q1._Name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("obj"))
        {
            QuestManager.Instance.AddProgress(Q1._Name);
        }
    }
}