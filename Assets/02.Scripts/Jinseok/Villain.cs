// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Villain : MonoBehaviour
// {
//     NpcInfoManager npc;
    
//     void Start(){
//         npc = new NpcInfoManager();
//     }
    
//     void VillainClassify(){
//         float randomValue = Random.Range(0.0f, 1.0f);

//         if (randomValue < 0.67f){
//             Debug.Log("Villan NPC");
//             npc.isVillain = true;
//         }
//         else{
//             Debug.Log("Normal NPC");
//             npc.isVillain = false;
//         }
//     }
//     // ���� ���� ���ϱ�
//     void setTypeOfVillan(){ 
//         int randomValue = Random.Range(0, 6);
//         switch(randomValue) {
//             case 0: // ȣ�� �̼���
//                 //CheckItem("ȣ��");
//             case 1: // ���������
//                 // 
//             case 2: // �˹� ����
//                 // �߰� �� �����Ÿ����� ��������� 
//                 // Ȱ��ȭ �Ǵ� �˰Ź�ư�� ���� ü��
//             case 3: // ���ַ�
//                 // 
//             case 4: // ����
//                 // 
//             case 5: //���˼���

//             case 6: // ����
                
//         }
//     }

//     public List<float> eventProbabilities; //

//     // 
//     // void Start()
//     // {
//     //     // Ȯ���� ���� 1�� �ǵ��� ����ȭ
//     //     NormalizeProbabilities();

//     //     // ������ ��� ����
//     //     string selectedEvent = SelectRandomEvent();
//     //     Debug.Log("Selected Event: " + selectedEvent);
//     // }

//     //  
//     void NormalizeProbabilities()
//     {
//         float totalProbability = 0f;
//         foreach (float probability in eventProbabilities)
//         {
//             totalProbability += probability;
//         }

//         // ����ȭ
//         for (int i = 0; i < eventProbabilities.Count; i++)
//         {
//             eventProbabilities[i] /= totalProbability;
//         }
//     }

//     // 
//     string SelectRandomEvent()
//     {
//         float randomValue = Random.Range(0f, 1f);
//         float cumulativeProbability = 0f;

//         // ���� Ȯ���� ���� ��� ����
//         for (int i = 0; i < eventProbabilities.Count; i++)
//         {
//             cumulativeProbability += eventProbabilities[i];
//             if (randomValue <= cumulativeProbability)
//             {
//                 // �ش� ��� ����
//                 return "Event " + (char)('A' + i);

//             }
//         }

//         // ������� �Դٸ� ���� ó��
//         return "Error: No event selected";
//     }
// }
