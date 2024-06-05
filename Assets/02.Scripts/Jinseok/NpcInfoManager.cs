using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NpcInfoManagerSpace {
    public class NpcInfoManager : MonoBehaviour {
        // 인스턴스 변수 선언
        [SerializeField] private string iname;
        [SerializeField] private int iage;
        [SerializeField] private string inpcDaily;
        [SerializeField] private string iitem;
        [SerializeField] private string ihometown;
        [SerializeField] private string ipassPurpose;

        // 정적 변수
        public static string Name;
        public static int Age;
        public static string NpcDaily;
        public static string Item;
        public static string Hometown;
        public static string PassPurpose;

        public void OnResetNpcBtnDown() {
            // 인스턴스 변수의 값을 정적 변수에 할당
            Name = iname;
            Age = iage;
            NpcDaily = inpcDaily;
            Item = iitem;
            Hometown = ihometown;
            PassPurpose = ipassPurpose;
        }
    }
}
