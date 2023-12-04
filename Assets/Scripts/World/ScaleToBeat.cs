using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToBeat : MonoBehaviour
{   
    public LevelManager levelManager;

    public bool scaleZ = true;
    public bool scaleX = false;
    public bool scaleY = false;
    // Start is called before the first frame update
    void Awake()
    {
        Vector3 newScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (scaleZ == true)
        {
            newScale.z = levelManager.distancePerBeat * levelManager.beatsPerTile;//*transform.localScale.z
        }
        if (scaleX == true)
        {
            newScale.x = levelManager.distancePerBeat * levelManager.beatsPerTile;//*transform.localScale.z
        }
        if (scaleY == true)
        {
            newScale.y = levelManager.distancePerBeat * levelManager.beatsPerTile;//*transform.localScale.z
        }
        transform.localScale = newScale;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
