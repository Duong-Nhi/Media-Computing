using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileRotator : MonoBehaviour
{
    public float rotationSpeed = 720f;
    

    // Rotate the projectile every frame
    void Update()
    {
        Quaternion currentRotation = transform.localRotation;

        // Calculate the new rotation around the Y-axis
        Quaternion deltaRotation = Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);

        // Combine the current rotation with the new rotation
        transform.localRotation = currentRotation * deltaRotation;
    }
}
