using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderIfClose : MonoBehaviour
{   
    public Transform player;

    public float renderDistance = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // Check the distance between the player and NPC
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= renderDistance)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
