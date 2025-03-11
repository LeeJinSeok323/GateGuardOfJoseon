using Questdll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Becon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
        } 
    }
}
