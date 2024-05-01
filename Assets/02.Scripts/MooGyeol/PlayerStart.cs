using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    public GameObject Player;

    void Start()
    {
        GameObject player = Instantiate(Player, this.transform.position, this.transform.rotation);
        DontDestroyOnLoad(player);
    }

    

}
