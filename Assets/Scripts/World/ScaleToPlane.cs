using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;
using UnityEngine;

public class ScaleToPlane : MonoBehaviour
{
    public GameObject plane;
    public float planeScale = 0.5F;

    // Make sure it is called after scale normalizer
    void Start()
    {
        float newPlaneScale = plane.transform.localScale.x; // Assumes its square
        Assert.AreEqual(plane.transform.localScale.x, plane.transform.localScale.z);

        transform.localScale = new Vector3(transform.localScale.x * newPlaneScale/planeScale, transform.localScale.y * newPlaneScale/planeScale, transform.localScale.z * newPlaneScale/planeScale);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
