//EventManager.cs
using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private static List<EventData> eventList = new List<EventData>(); // �̺�Ʈ �����͸� �����ϴ� ����Ʈ

    // �ӽ� �ڵ� ����, ���� ���� ȯ�濡���� ������ ���� ����ؾ� �մϴ�.
    #region �ӽ��ڵ�
    public static int Payment = 100;
    public static int SetPayment = 100;
    #endregion

    // �̺�Ʈ �����͸� �����ϴ� Ŭ����
    public class EventData
    {
        public string eventID;
    }

    void Start()
    {
        InitializeEvents();
    }

    void InitializeEvents()
    {
        // �������� �̺�Ʈ �����͸� �����մϴ�.
        eventList.Add(new EventData { eventID = "tevent1" });
        eventList.Add(new EventData { eventID = "tevent2" });
        eventList.Add(new EventData { eventID = "tevent3" });
        eventList.Add(new EventData { eventID = "sevent1" });
        eventList.Add(new EventData { eventID = "sevent2" });
        // �߰� �̺�Ʈ ����...
    }

    void Update()
    {
        // �ʿ信 ���� �ٸ� ������ �߰��� �� �ֽ��ϴ�.
    }

    public static void ExecuteEvent(string eventID)
    {
        foreach (EventData eventData in eventList)
        {
            if (eventData.eventID == eventID)
            {
                ProcessEvent(eventData);
            }
        }
    }

    private static void ProcessEvent(EventData eventData)
    {
        switch (eventData.eventID)
        {
            case "tevent1":
                GameMgr.Instance.townHappinessPoint += 2;
                break;
            case "tevent2":
                GameMgr.Instance.townHappinessPoint -= 5;
                break;
            case "tevent3":
                GameMgr.Instance.townHappinessPoint -= (int)((1 - GameMgr.Instance.townHappinessPoint / 100.0) * 0.2 * 10);
                break;
            case "sevent1":
                GameMgr.Instance.bossSatisfaction += (int)((Payment - SetPayment) * 0.2);
                break;
            case "sevent2":
                GameMgr.Instance.bossSatisfaction -= (int)((SetPayment - Payment) * 0.2);
                break;
        }
    }
}