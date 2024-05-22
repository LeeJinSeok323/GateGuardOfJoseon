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
//     // 빌런 유형 정하기
//     void setTypeOfVillan(){ 
//         int randomValue = Random.Range(0, 6);
//         switch(randomValue) {
//             case 0: // 호패 미소지
//                 //CheckItem("호패");
//             case 1: // 지명수배자
//                 // 
//             case 2: // 검문 도주
//                 // 추격 중 일정거리내로 가까워지면 
//                 // 활성화 되는 검거버튼을 눌러 체포
//             case 3: // 금주령
//                 // 
//             case 4: // 아편
//                 // 
//             case 5: //도검소지

//             case 6: // 역병
                
//         }
//     }

//     public List<float> eventProbabilities; //

//     // 
//     // void Start()
//     // {
//     //     // 확률의 합이 1이 되도록 정규화
//     //     NormalizeProbabilities();

//     //     // 랜덤한 사건 선택
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

//         // 정규화
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

//         // 누적 확률을 통해 사건 선택
//         for (int i = 0; i < eventProbabilities.Count; i++)
//         {
//             cumulativeProbability += eventProbabilities[i];
//             if (randomValue <= cumulativeProbability)
//             {
//                 // 해당 사건 선택
//                 return "Event " + (char)('A' + i);

//             }
//         }

//         // 여기까지 왔다면 오류 처리
//         return "Error: No event selected";
//     }
// }
