using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToBeat : MonoBehaviour
{   
    public GameObject reference;
    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(transform.localScale.x , transform.localScale.y , levelManager.distancePerBeat * levelManager.beatsPerTile*transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
