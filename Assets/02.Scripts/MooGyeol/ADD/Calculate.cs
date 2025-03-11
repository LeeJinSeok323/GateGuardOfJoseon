using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    public Text saveMoney;
    public Text Pay;
    public Text Miss;
    public Text Catch;
    public Text H_vil;
    public Text H_Boss;
    public Text Cal;
    public Text Cal_money;

    private int C_money;
    private int C_Miss;
    private int C_Catch;
    private int C_H_vil;
    private int C_H_boss;

    private void Update()
    {
       GameMgr m = GameMgr.Instance;
        C_money = m.money;
        C_Miss = m.missCnt;
        C_Catch = m.catchCnt;
        C_H_vil = m.townHappinessPoint;
        C_H_boss = m.bossSatisfaction;

        FillText();
    }

    public void FillText()
    {
        saveMoney.text = C_money.ToString();
        Pay.text = C_Catch * 50 + "("+ C_Catch +")";

        Miss.text = 100 * C_Miss + " " +"(" + C_Miss.ToString()+ ")";
        Catch.text = 50 * C_Catch + " " + "(" + C_Catch.ToString() +")";

        H_vil.text = C_H_vil.ToString();
        H_Boss.text = C_H_boss.ToString();

        Cal.text = C_money.ToString() + " + " + C_Catch * 50 + " - " + 100 * C_Miss + " + " + 50 * C_Catch + " = ";
        Cal_money.text = (C_money + 250 + (-100 * C_Miss) + (50 * C_Catch)).ToString();
    }
}
