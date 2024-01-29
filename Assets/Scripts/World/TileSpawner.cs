using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public AudioManager audioManager;
    public LevelManager levelManager;
    public GameObject[] tileHouses;

    public GameObject[] tileScenery;
    public GameObject[] tileScenery2;

    public GameObject[] specialTiles;

    public GameObject road;
    public GameObject roadFlipped;
    public GameObject finishRoad;
    public GameObject LetterObject;
    public GameObject PackageObject;


    public Transform parentObject;
    private System.Random randomGen = new System.Random();

    private List<GameObject> tileObjPerBeatLeft = new List<GameObject>();

    private List<GameObject> tileObjPerBeatRight = new List<GameObject>();

    private int starsSpawned = 0;


    // Minimum distances between special tiles
    public int[] minSpecialDistances = new int[] {
            5  //Min distance between crossroads
        };

    // How many tiles should be generated before music starts
    public int nStartingTiles = 4;
    
    // How are the tiles positioned relative to the beat lines in distance per line
    public float tileBeatOffset = 0.5F;

    // Used to record distance to last Spawn of each tile to avoid same tile close to each other
    private int[] currHouseDistances;
    private int[] currSceneryDistances;
    private int[] currSpecialDistances;

    // Currently must be set per hand: Mask of sequences that we look for the i-th special tile
    private (int,int)[][] specialTileMasks =
    {
        new (int, int)[] { (0,0), (0,0) , (0,0), (0,0)}
    };

    // Keep track of beats
    private int currentBeat = 0;

    void Start()
    {   
        //subscribe to events
        AudioManager.beatUpdated += onBeat;
        road.transform.localPosition = Vector3.zero;

        //init distances (TODO: Set all to inf unless they should not spawn close to start of the map)
        currHouseDistances = new int[tileHouses.Length];
        currSceneryDistances = new int[tileScenery.Length];
        currSpecialDistances = new int[specialTiles.Length];

        //Spawn first n Tiles by simulating the first nTiles * beatsPerTile beats
        for (int i=0; i< nStartingTiles*levelManager.beatsPerTile; i++)
        {
            onBeat();
        }

        // Spawn tiles before beat 1

        for(int i=1; i<4; i++)
        {
            int beat = -i*4;
            //spawn road
            GameObject newRoad = Instantiate(road, new Vector3(0, 0, (beat) * levelManager.distancePerBeat) + new Vector3(0,-1,tileBeatOffset * levelManager.distancePerBeat), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
  
            //spawn house right
            GameObject newHouse1 = Instantiate(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(0, 0, (beat) * levelManager.distancePerBeat) + new Vector3(17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat), transform.rotation * Quaternion.Euler(0f, 0f, 0f));

            //spawn house left
            GameObject newHouse2 = Instantiate(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(0, 0, (beat) * levelManager.distancePerBeat) + new Vector3(-17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat), transform.rotation * Quaternion.Euler(0f, 180f, 0f));

            //Disable Scale to plane script for houses
            ScaleToPlane scaleToPlaneScript = newHouse1.GetComponent<ScaleToPlane>();
            if (scaleToPlaneScript != null)
            {
                // Disable the script component
                scaleToPlaneScript.enabled = false;
            }
            scaleToPlaneScript = newHouse2.GetComponent<ScaleToPlane>();
            if (scaleToPlaneScript != null)
            {
                // Disable the script component
                scaleToPlaneScript.enabled = false;
            }

            newRoad.transform.SetParent(parentObject);
            newHouse1.transform.SetParent(parentObject);
            newHouse2.transform.SetParent(parentObject);
            //spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(-17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat), transform.rotation * Quaternion.Euler(0f, 0f, 0f));
            //spawn house right

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        //unsubscribe from events
        AudioManager.beatUpdated -= onBeat;
    }

    void onBeat()
    {
        currentBeat += 1;
        //check if we should gen nextTile
        if ((currentBeat-1) % levelManager.beatsPerTile == 0)
        {
            genNextTile();
        }
        //spawn indicators
        spawnIndicators(new Vector3(0, 0, (currentBeat-1) * levelManager.distancePerBeat));
    }
    void genNextTile()
    {
        //increment distances -> This is not efficient: Better replace distances with last position placed and calculated distance from that
        for(int i=0; i<currHouseDistances.Length;i++)
        {
            currHouseDistances[i] += 1;
        }
        for(int i=0; i<currSceneryDistances.Length;i++)
        {
            currSceneryDistances[i] += 1;
        }
        for(int i=0; i<currSpecialDistances.Length;i++)
        {
            currSpecialDistances[i] += 1;
        }

        GameObject toSpawn = null;
        int specialTileSpawnIndex = 0;

        //Iterate over special tiles
        for (int i=0; i<specialTiles.Length; i++)
        {
            if (canSpawnSpecialTile(i))
            {
                // Spawn tile with probability
                //Debug.Log("special tile fits");
                System.Random rand = new System.Random();
                double randomValue = rand.NextDouble();
                //Debug.Log($"Random Value: {randomValue}, SpawnProbability: {spawnProbability(currSpecialDistances[i], minSpecialDistances[i], 5)}");
                if (randomValue < spawnProbability(currSpecialDistances[i], minSpecialDistances[i], 5))
                {
                    toSpawn = specialTiles[i];
                    specialTileSpawnIndex = i;
                    currSpecialDistances[i] = 0;
                    //Debug.Log("spawned");
                }
                break;
            }
        }

        if (toSpawn != null)
        {
            // Here a crossroad is spawned? Only special tile we have currently
            GameObject spawnedSpecial = spawnGameObject(toSpawn, new Vector3(0,-1,tileBeatOffset * levelManager.distancePerBeat));
            spawnGameObject(roadFlipped, new Vector3(-20,-0.99F, tileBeatOffset * levelManager.distancePerBeat), false, true);
            spawnGameObject(roadFlipped, new Vector3(-40, -0.99F, tileBeatOffset * levelManager.distancePerBeat), false, true);
            spawnGameObject(roadFlipped, new Vector3(-60, -0.99F, tileBeatOffset * levelManager.distancePerBeat), false, true);
            spawnGameObject(roadFlipped, new Vector3(20, -0.99F, tileBeatOffset * levelManager.distancePerBeat), false, true);
            spawnGameObject(roadFlipped, new Vector3(40, -0.99F, tileBeatOffset * levelManager.distancePerBeat), false, true);
            spawnGameObject(roadFlipped, new Vector3(60, -0.99F, tileBeatOffset * levelManager.distancePerBeat), false, true);

            //for(int i=0; i < levelManager.beatsPerTile; i++)
            for(int i=0; i < specialTileMasks[specialTileSpawnIndex].Length; i++)
            {
                tileObjPerBeatRight.Add(spawnedSpecial);
                tileObjPerBeatLeft.Add(spawnedSpecial);
            }
        }
        else
        {
            
            
            //Check if finish beat is on the next 4 beats
            /*bool finish = false;
            for(int i=0; i<levelManager.beatsPerTile; i++)
            {
                if (levelManager.levelSequence[currentBeat-1+i].Item1 == 4 && levelManager.levelSequence[currentBeat-1+i].Item2 == 4)
                {
                    finish = true;
                    break;
                }
            }*/

            if(currentBeat >= audioManager.nBeats-8 && currentBeat < audioManager.nBeats-4)
            {
                //spawn finishline
                spawnGameObject(finishRoad, new Vector3(0,-1,tileBeatOffset * levelManager.distancePerBeat));
            }
            else
            {
                //spawn road
                spawnGameObject(road, new Vector3(0,-1,tileBeatOffset * levelManager.distancePerBeat));
            }

            
            // Check if house or scenery is spawned for left and right
            bool sceneryLeft = true;
            bool houseLeft = false;
            bool sceneryRight = true;

            bool starLeft = false;
            bool starRight = false;

            for(int i=0; i<levelManager.beatsPerTile; i++)
            {
                if (levelManager.levelSequence[currentBeat-1+i].Item1 != 0 )
                {
                    if(levelManager.levelSequence[currentBeat-1+i].Item1 == 5)
                    {
                        starLeft = true;
                    }
                    else{
                        sceneryLeft = false;
                        houseLeft = true;
                        break;
                    }
                }
                if(levelManager.levelSequence[currentBeat-1+i].Item1 == 5)
                {
                    starLeft = true;
                }
            }
            for(int i=0; i<levelManager.beatsPerTile; i++)
            {
                if (levelManager.levelSequence[currentBeat-1+i].Item2 != 0 )
                {
                    if(levelManager.levelSequence[currentBeat-1+i].Item2 == 5)
                    {
                        starRight = true;
                    }
                    else{
                        sceneryRight = false;
                        break;
                    }
                }
            }

            // Spawn tiles depending on results
            GameObject spawnedLeft = null;
            GameObject spawnedRight = null;

            if (sceneryLeft)
            {
                if(starLeft && (float)randomGen.NextDouble() < 0.5f)
                {
                    spawnedLeft = spawnGameObject(tileHouses[randomGen.Next(0, tileScenery.Length)], new Vector3(-17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat), true);
                }
                else {
                    spawnedLeft = spawnGameObject(tileScenery[randomGen.Next(0, tileScenery.Length)], new Vector3(-17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat), true);
                }
                
                spawnGameObject(tileScenery2[randomGen.Next(0, tileScenery2.Length)], new Vector3(-35F, -1F, tileBeatOffset * levelManager.distancePerBeat), true);
                spawnGameObject(tileScenery2[randomGen.Next(0, tileScenery2.Length)], new Vector3(-52.5F, -1F, tileBeatOffset * levelManager.distancePerBeat), true);

                for(int i=0; i < levelManager.beatsPerTile; i++)
                {
                    tileObjPerBeatLeft.Add(spawnedLeft);
                }
            }
            else{
                spawnedLeft = spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(-17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat), houseLeft);
                spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(-35F, -1F, tileBeatOffset * levelManager.distancePerBeat));
                spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(-52.5F, -1F, tileBeatOffset * levelManager.distancePerBeat));

                for(int i=0; i < levelManager.beatsPerTile; i++)
                {
                    tileObjPerBeatLeft.Add(spawnedLeft);
                }
            }
            if (sceneryRight)
            {
                if(starLeft && (float)randomGen.NextDouble() < 0.5f)
                {
                    spawnedRight = spawnGameObject(tileHouses[randomGen.Next(0, tileScenery.Length)], new Vector3(17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat));
                }
                else
                {
                    spawnedRight = spawnGameObject(tileScenery[randomGen.Next(0, tileScenery.Length)], new Vector3(17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat));
                }
                spawnGameObject(tileScenery2[randomGen.Next(0, tileScenery2.Length)], new Vector3(35F, -1F, tileBeatOffset * levelManager.distancePerBeat));
                spawnGameObject(tileScenery2[randomGen.Next(0, tileScenery2.Length)], new Vector3(52.5F, -1F, tileBeatOffset * levelManager.distancePerBeat));

                for(int i=0; i < levelManager.beatsPerTile; i++)
                {
                    tileObjPerBeatRight.Add(spawnedRight);
                }
            }
            else{
                spawnedRight = spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(17.5F,-1F,tileBeatOffset * levelManager.distancePerBeat));
                spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(35F, -1F, tileBeatOffset * levelManager.distancePerBeat));
                spawnGameObject(tileHouses[randomGen.Next(0, tileHouses.Length)], new Vector3(52.5F, -1F, tileBeatOffset * levelManager.distancePerBeat));

                for(int i=0; i < levelManager.beatsPerTile; i++)
                {
                    tileObjPerBeatRight.Add(spawnedRight);
                }
            }

            if(starRight && starsSpawned<3)
            {
                GameObject star = spawnedRight.transform.Find("StarSpawn").gameObject;
                StarSpawn starSpawnScript = star.GetComponent<StarSpawn>();
                if (starSpawnScript != null)
                {
                    starSpawnScript.spawnStar(starsSpawned);
                }
                starsSpawned += 1;
            }

            if(starLeft && starsSpawned<3)
            {
                GameObject star = spawnedLeft.transform.Find("StarSpawn").gameObject;
                StarSpawn starSpawnScript = star.GetComponent<StarSpawn>();
                if (starSpawnScript != null)
                {
                    starSpawnScript.spawnStar(starsSpawned);
                }
                starsSpawned += 1;
            }
        }
    }

    GameObject spawnGameObject(GameObject toSpawn, Vector3 offset, bool left = false, bool flipped = false)
    {
        GameObject newTile;
        Vector3 nextPosition = new Vector3(0, 0, (currentBeat-1) * levelManager.distancePerBeat) + offset;
        if (left)
        {
            newTile = Instantiate(toSpawn, nextPosition, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }
        else if (flipped) 
        {
            newTile = Instantiate(toSpawn, nextPosition, transform.rotation * Quaternion.Euler(0f, 90f, 0f));
        }
        else
        {
            newTile = Instantiate(toSpawn, nextPosition, Quaternion.identity);
        }
        newTile.transform.SetParent(parentObject);
        //newTile.transform.rotation = toSpawn.transform.rotation;

        //Disable Scale to plane script for houses
        ScaleToPlane scaleToPlaneScript = newTile.GetComponent<ScaleToPlane>();
        if (scaleToPlaneScript != null)
        {
            // Disable the script component
            scaleToPlaneScript.enabled = false;
        }
        newTile.transform.SetParent(parentObject);

        // enable spawn npc script for houses
        if(newTile.transform.Find("NPC") != null)
        {
            GameObject npc = newTile.transform.Find("NPC").gameObject;
            if (npc != null)
            {
                SpawnNPC spawnNPC = npc.GetComponent<SpawnNPC>();
                if (spawnNPC != null)
                {
                    spawnNPC.enabled = true;
                }
            }
        }
        




        return newTile;
    }

    bool canSpawnSpecialTile(int tileIdx)
    {
        (int,int)[] mask = specialTileMasks[tileIdx];
        bool canSpawn = true;
        for (int i=0; i<mask.Length; i++)
        {
            //Debug.Log("Here comes a new check if crossroad fits");
            if (levelManager.levelSequence[currentBeat-1+i] != mask[i])
            {  
                //Debug.Log($"Tile {i}: {levelManager.levelSequence[currentBeat-1+i]}");
                canSpawn = false;
                break;
            }
        }
        return canSpawn;
    }

    //halfValueDist = parameter that can be tuned to increase how fast the probability increases (at this distance we have 50% spawn chance)
    double spawnProbability(int currDistance, int minDistance, int halfValueDist)
    {
        double spawnProbability = 0;
        //Debug.Log($"currDist = {currDistance}, minDist = {minDistance}");
        if (currDistance > minDistance)
        {
            //After reaching threshold of minimal distance we have a curve starting from 0 and slowly approaching 100%
            double delta = (currDistance-minDistance);
            spawnProbability = (delta/halfValueDist)/(delta/halfValueDist+1);
            //Debug.Log($"Reached. Probability calculated: {spawnProbability}");
        }
        return spawnProbability;
    }

    void spawnIndicators(Vector3 nextPosition)
    {
        (int, int) indicators = levelManager.levelSequence[currentBeat-1];
        switch (indicators.Item1)
        {
            case 1:
                Instantiate(LetterObject, nextPosition + new Vector3(-4, -1.8F, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(PackageObject, nextPosition + new Vector3(-4, -1.8F, 0), Quaternion.identity);
                break;
        }
        switch (indicators.Item2)
        {
            case 1:
                Instantiate(LetterObject, nextPosition + new Vector3(4, -1.8F, 0), Quaternion.identity);
                break;
            case 2:
                Instantiate(PackageObject, nextPosition + new Vector3(4, -1.8F, 0), Quaternion.identity);
                break;
        }
        //TODO: Set parent object so that scene does not get cluttered
    }


    //Function that maps beat to its gameobject tile
    public GameObject getObjectLeft(int beat)
    {
        GameObject currObjLeft = tileObjPerBeatLeft[beat-1];
        return currObjLeft;
    }

    public GameObject getObjectRight(int beat)
    {
        GameObject currObjRight = tileObjPerBeatRight[beat-1];
        return currObjRight;
    }
    
}