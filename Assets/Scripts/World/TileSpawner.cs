using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    //Add Possibility to spawn different kinds of tiles from prefab 
    public GameObject objectToSpawn;
    public GameObject LetterObject;
    public GameObject PackageObject;
    //private GameObject objectToDespawn; 
    public Transform parentObject;

    public LevelManager levelManager;
    //Otherwise spawn every bar
    public bool everyBeat = false;
    private int nextTileNumber = 1;

    public float tileLength = 25;

    public int nStartingTiles = 4;
    
    //
    private int currentBeat;


    private Vector3 lastTilePos; //-1 only to fix displacement of the tiles (for some unknown? reason they spawn at y=1 instead of the original tile at y=0)
    void Start()
    {   
        if (everyBeat == true)
        {
            tileLength = levelManager.distancePerBeat;
        }
        //Spawn first tiles
        lastTilePos = new Vector3 (0,-1,-tileLength);
        
        for (int i=0; i<nStartingTiles; i++)
        {
            Vector3 nextPosition = lastTilePos + new Vector3(0, 0, tileLength);
            GameObject newTile = Instantiate(objectToSpawn, nextPosition, Quaternion.identity);
            newTile.transform.SetParent(parentObject);

            (int, int) indicators = levelManager.levelSequence[i];
            switch (indicators.Item1)
            {
                case 1:
                    Instantiate(LetterObject, nextPosition + new Vector3(-4, 0.2F, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(PackageObject, nextPosition + new Vector3(-4, 0.2F, 0), Quaternion.identity);
                    break;
            }
            switch (indicators.Item2)
            {
                case 1:
                    Instantiate(LetterObject, nextPosition + new Vector3(4, 0.2F, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(PackageObject, nextPosition + new Vector3(4, 0.2F, 0), Quaternion.identity);
                    break;
            }

            lastTilePos = nextPosition;
            //Names the new tiles
            newTile.name = "Tile" + nextTileNumber;
            nextTileNumber += 1;
        }


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
        currentBeat++;
        Vector3 nextPosition = lastTilePos + new Vector3(0, 0, tileLength);
        GameObject newTile = Instantiate(objectToSpawn, nextPosition, Quaternion.identity);
        newTile.transform.SetParent(parentObject);
        // Spawn indicators what input is expected
        (int, int) indicators = levelManager.levelSequence[currentBeat+nStartingTiles-1];
        switch (indicators.Item1)
        {
            case 1:
                Instantiate(LetterObject, nextPosition + new Vector3(-4, 0.2F, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(PackageObject, nextPosition + new Vector3(-4, 0.2F, 0), Quaternion.identity);
                break;
        }
        switch (indicators.Item2)
        {
            case 1:
                Instantiate(LetterObject, nextPosition + new Vector3(4, 0.2F, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(PackageObject, nextPosition + new Vector3(4, 0.2F, 0), Quaternion.identity);
                break;
        }

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
