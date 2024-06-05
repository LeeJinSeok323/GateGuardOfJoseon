using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NpcInfoManagerSpace {
    public class NpcInfoManager : MonoBehaviour {
        // �ν��Ͻ� ���� ����
        [SerializeField] private string iname;
        [SerializeField] private int iage;
        [SerializeField] private string inpcDaily;
        [SerializeField] private string iitem;
        [SerializeField] private string ihometown;
        [SerializeField] private string ipassPurpose;

        // ���� ����
        public static string Name;
        public static int Age;
        public static string NpcDaily;
        public static string Item;
        public static string Hometown;
        public static string PassPurpose;

        public void OnResetNpcBtnDown() {
            // �ν��Ͻ� ������ ���� ���� ������ �Ҵ�
            Name = iname;
            Age = iage;
            NpcDaily = inpcDaily;
            Item = iitem;
            Hometown = ihometown;
            PassPurpose = ipassPurpose;
        }
    }
}
