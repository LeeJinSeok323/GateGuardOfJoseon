using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDestory : MonoBehaviour
{
    private static DonDestory _instance;

    public static DonDestory Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject singletonObject = new GameObject();
                _instance = singletonObject.AddComponent<DonDestory>();
                singletonObject.name = typeof(DonDestory).ToString() + " (Singleton)";
                DontDestroyOnLoad(singletonObject);
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // 중복된 오브젝트 파괴
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
