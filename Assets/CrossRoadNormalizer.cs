using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRoadNormalizer : MonoBehaviour
{
    public GameObject reference;
    public GameObject roadAsset;
    // Start is called before the first frame update
    void Start()
    {
        // Crossroads and roads need to fit seemlessly, so here we ge the factor of their sizedifference
        Vector3 roadSize = roadAsset.GetComponent<Renderer>().bounds.size;
        Vector3 crossSize = this.GetComponent<Renderer>().bounds.size;
        float finalresizeX = crossSize.x / roadSize.x;
        float finalresizeZ = crossSize.z / roadSize.z;

        Vector3 refSize = reference.GetComponent<Renderer>().bounds.size;

        //Scaling variable that helps scale every tile to the size of a reference object
        float resizeX = refSize.x / GetComponent<Renderer>().bounds.size.x;
        float resizeY = refSize.y / GetComponent<Renderer>().bounds.size.y;
        float resizeZ = refSize.z / GetComponent<Renderer>().bounds.size.z;

        resizeX *= transform.localScale.x;
        resizeY = transform.localScale.y;
        resizeZ *= transform.localScale.z;

        //Sets new scale and offset
        transform.localScale = new Vector3(resizeX * finalresizeX, resizeY, resizeX * finalresizeX);
        float offset = 0.01f;
        transform.position += new Vector3(0, offset, 0) ;
    }

    
}
