using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    
    void Update()
    {
        // Face the camera by adjusting the rotation
        transform.LookAt(new Vector3(Camera.main.transform.position.x, 1f, Camera.main.transform.position.z));
        transform.Rotate(Vector3.forward, 0f); // Optional: Adjust the rotation to match your sprite's orientation
    }
}
