using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNPC : MonoBehaviour
{
    public float spawnChance = 1f;

    public GameObject npc;

    private System.Random randomGen = new System.Random();

    void Start()
    { 
        //  Check if we want to spawn (possibly by random chance)
        float randomValue = (float)randomGen.NextDouble();
        if (randomValue > spawnChance)
        {
            //Destroy self including bounding box
            Destroy(gameObject);
        }

        //spawn instance
        Vector3 originalPosition = npc.transform.position;
        npc.transform.position = new Vector3(0f,0.1f,0f);
        Instantiate(npc, transform);
        npc.transform.position = originalPosition;
       
        
    }
}
