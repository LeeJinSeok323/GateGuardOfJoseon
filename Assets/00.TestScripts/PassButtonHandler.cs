using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassButtonHandler : MonoBehaviour
{
    private Transform GatePoint;
    private float radius;
    //private GameObject closestNPC;

    private void Start()
    {
        GatePoint = GameObject.FindGameObjectWithTag("Point").transform;
        radius = 3.0f;
    }

    private int CheckRadiusNPC()
    {
        Collider[] colliders = Physics.OverlapSphere(GatePoint.position, radius);
        float closestDistance = Mathf.Infinity;
        int id = 999;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("NPC"))
            {
                float distance = Vector3.Distance(GatePoint.position, collider.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    id = NpcManager.Instance.GetIdByObject(collider.gameObject);
                    
                }
            }
        }
        return id;
    }

    public void OnClickPassButton()
    {
        int id = CheckRadiusNPC();
        if(id != 999)
        {
            NpcManager.Instance.PassGate(id);
            NpcManager.Instance.Remove(id);
        }
        

        //Npc npc = go.GetComponent<Npc>();
        //if(npc != null)
        //{
        //    NpcManager.Instance.PassGate(npc.ID);
        //    NpcManager.Instance.Remove(npc.ID);
        //}


    }

    public void OnClickDeninedButton()
    {
        int id = CheckRadiusNPC();
        if (id != 999)
        {
            NpcManager.Instance.DeninedGate(id);
            NpcManager.Instance.Remove(id);
        }

    }



}
