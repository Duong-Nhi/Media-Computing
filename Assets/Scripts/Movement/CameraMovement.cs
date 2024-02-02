using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    
    // offset to player position
    public Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        // Follow player every frame with the offset given by the parameter
        if (player != null)
        {
            transform.position = player.position + offset;
        }
        else
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
