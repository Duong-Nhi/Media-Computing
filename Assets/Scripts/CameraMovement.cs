using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    
    public Vector3 offset = new Vector3 (0,40,-5);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
        else
        {
            //Add Error
        }
    }
}
