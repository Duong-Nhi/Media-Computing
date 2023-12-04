using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleNormalizer : MonoBehaviour
{
    public GameObject reference;
    // Start is called before the first frame update
    void Start()
    {

        Vector3 refSize = reference.GetComponent<Renderer>().bounds.size;


        float resizeX = refSize.x / GetComponent<Renderer>().bounds.size.x;
        float resizeY = refSize.y / GetComponent<Renderer>().bounds.size.y;
        float resizeZ = refSize.z / GetComponent<Renderer>().bounds.size.z;

        resizeX *= transform.localScale.x;
        resizeY = transform.localScale.y;
        resizeZ *= transform.localScale.z;

        transform.localScale = new Vector3(resizeX, resizeY, resizeZ);


    }
}

