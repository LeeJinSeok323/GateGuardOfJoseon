//EventManager.cs
using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    private static List<EventData> eventList = new List<EventData>(); // 이벤트 데이터를 저장하는 리스트

    // 임시 코드 영역, 실제 게임 환경에서는 적절한 값을 사용해야 합니다.
    #region 임시코드
    public static int Payment = 100;
    public static int SetPayment = 100;
    #endregion

    // 이벤트 데이터를 저장하는 클래스
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
        // 정적으로 이벤트 데이터를 정의합니다.
        eventList.Add(new EventData { eventID = "tevent1" });
        eventList.Add(new EventData { eventID = "tevent2" });
        eventList.Add(new EventData { eventID = "tevent3" });
        eventList.Add(new EventData { eventID = "sevent1" });
        eventList.Add(new EventData { eventID = "sevent2" });
        // 추가 이벤트 정의...
    }

    void Update()
    {
        // 필요에 따라 다른 로직을 추가할 수 있습니다.
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