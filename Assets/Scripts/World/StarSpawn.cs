using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    private BoxCollider collider;
    public GameObject[] stars;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
        collider.enabled = false;
    }

    // Spawns a star here; Called from tilespawner if the tile where this starSpawner is placed on is marked as a starSpawn
    public void spawnStar(int starIndex)
    {
        GameObject newStar = Instantiate(stars[starIndex], transform);
        newStar.transform.position = newStar.transform.position + new Vector3(0f, 0f, 0f);
        collider = GetComponent<BoxCollider>();
        collider.enabled = true;
    }
}
