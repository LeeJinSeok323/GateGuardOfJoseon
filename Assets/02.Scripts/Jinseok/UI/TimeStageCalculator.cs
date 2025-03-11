using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeStageCalculator : MonoBehaviour
{
    private const int START_HOUR = 9;
    private const int END_HOUR = 18;
    private const int MINUTES_PER_STAGE = 10;
    private const int TOTAL_MINUTES = (END_HOUR - START_HOUR) * 60;
    private const int TOTAL_STAGES = TOTAL_MINUTES / MINUTES_PER_STAGE;

    public Text timeText; // Inspector���� �Ҵ��� Text ������Ʈ

    // We don't use Stage though, gpt made it just leave it  (by jinseok)
    public int CurrentStage { get; private set; }
    public string CurrentTime { get; private set; }

    private float timer = 0f;
    private float stageLength; // ���� ���� �ð����� �� ���������� ���� (��)

    [Header ("Total Minute for One Day")]
    public float totalGameTimeInMinutes = 2f;
    void Start()
    {
        // ��ü ���� �ð��� �����մϴ� (��: 10��)
        stageLength = (totalGameTimeInMinutes * 60f) / TOTAL_STAGES;
        UpdateStageAndTime(0);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= stageLength)
        {
            timer -= stageLength;
            UpdateStageAndTime(CurrentStage + 1);
        }
        // End Day 
        
    }

    void UpdateStageAndTime(int newStage)
    {
        if(newStage/TOTAL_STAGES >= 1){
            GameMgr.Instance.AbleNextDay = true;
            GameMgr.Instance.AddMaxLightAngle();
        }
        if(newStage%10 == 0){
            GameMgr.Instance.AddMaxLightAngle();
        }
        CurrentStage = newStage % TOTAL_STAGES;
        int totalMinutes = (CurrentStage * MINUTES_PER_STAGE) + (START_HOUR * 60);
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;
        CurrentTime = $"{hours:D2}:{minutes:D2}";
        
        // Text ������Ʈ�� �ð� ǥ��
        if (timeText != null)
        {
            timeText.text = CurrentTime;
        }
        
        // Debug.Log($"Stage: {CurrentStage + 1}/{TOTAL_STAGES}, Time: {CurrentTime}");
    }
}