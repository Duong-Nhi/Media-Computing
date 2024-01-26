using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scale2 : MonoBehaviour
{
    public GameObject reference;
    public GameObject roadAsset;
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

        transform.localScale = new Vector3(0.6798319F, resizeY, 0.888346F);


    }
}
