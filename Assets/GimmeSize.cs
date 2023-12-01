using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GimmeSize : MonoBehaviour
{
    float targetSize = 5;
    

    // Start is called before the first frame update
    void Start()
    {
        
        GameObject streetTile = this.gameObject;
        var rend = streetTile.GetComponent<Renderer>();
        Vector3 currentSize = rend.bounds.size;
        Vector3 scale = streetTile.transform.localScale;
        Debug.Log(currentSize);
        currentSize.x = targetSize;
        currentSize.y = targetSize;
        currentSize.z = targetSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
