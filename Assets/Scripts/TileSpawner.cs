using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    //Add Possibility to spawn different kinds of tiles from prefab 
    public GameObject objectToSpawn;
    //private GameObject objectToDespawn; 
    public Transform parentObject;
    //Otherwise spawn every bar
    public bool everyBeat = false;
    private int nextTileNumber = 5;
    

    public Vector3 lastTilePos = new Vector3 (0,-1,100); //-1 only to fix displacement of the tiles (for some unknown? reason they spawn at y=1 instead of the original tile at y=0)
    void Start()
    {   
        //Subscribe events
        if (everyBeat == true)
        {
            AudioManager.beatUpdated += spawnTile;
        }
        else
        {
            AudioManager.barUpdated += spawnTile;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        if (everyBeat == true)
        {
            AudioManager.beatUpdated -= spawnTile;
        }
        else
        {
            AudioManager.barUpdated -= spawnTile;
        }
    }

    void spawnTile()
    {
        Vector3 nextPosition = lastTilePos + new Vector3(0, 0, 25);
        GameObject newTile = Instantiate(objectToSpawn, nextPosition, Quaternion.identity);
        newTile.transform.SetParent(parentObject);
        lastTilePos = nextPosition;
        //Names the new tiles
        newTile.name = "Tile" + nextTileNumber;
        nextTileNumber += 1;
        //Despawns the oldest tile, only works if the first child in the hierarchy is the oldest (should be the case)
        if (parentObject.transform.childCount > 6) 
        { 
            GameObject tileToDespawn = parentObject.transform.GetChild(0).gameObject;
            Destroy(tileToDespawn);
        }
        
        
    }

    
}
