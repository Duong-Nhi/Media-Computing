using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomizer : MonoBehaviour
{
    public Transform player;
    public Sprite[] idleSprites;
    public Sprite[] happySprites;
    public Sprite[] sadSprites;
    public Sprite[] dizzySprites;

    public float percentSpawn = 1f;

    public float renderDistance = 100f;

    private System.Random randomGen = new System.Random();
    private int characterIndex = 0;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randomIndex = randomGen.Next(0, idleSprites.Length);

        //  Check if we want to spawn
        float randomValue = (float)randomGen.NextDouble();
        if (randomValue > percentSpawn)
        {
            Destroy(gameObject);
        }
        spriteRenderer.sprite = idleSprites[randomIndex];
        characterIndex = randomIndex;
    }

    // Implement functionalities for changing between sprites including animation

    void Update()
    {
        // Check the distance between the player and NPC
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= renderDistance)
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void makeHappy()
    {
        // add animation
        spriteRenderer.sprite = happySprites[characterIndex];
    }

    public void makeSad()
    {
        // add animation
        spriteRenderer.sprite = sadSprites[characterIndex];
    }

    public void makeDizzy()
    {
        // add animation
        spriteRenderer.sprite = dizzySprites[characterIndex];
    }
}
