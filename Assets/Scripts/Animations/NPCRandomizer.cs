using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRandomizer : MonoBehaviour
{
    public Sprite[] idleSprites;
    public Sprite[] happySprites;
    public Sprite[] sadSprites;
    public Sprite[] dizzySprites;

    public float percentSpawn = 1f;

    private System.Random randomGen = new System.Random();
    private int characterIndex = 0;
    private SpriteRenderer spriteRenderer;

    // Spawn a random NPC with a random chance (default 100% chance)
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int randomIndex = randomGen.Next(0, idleSprites.Length);

        //  Only spawn NPC with a random chance
        float randomValue = (float)randomGen.NextDouble();
        if (randomValue > percentSpawn)
        {
            Destroy(gameObject);
        }
        // Spawn random NPC
        spriteRenderer.sprite = idleSprites[randomIndex];
        characterIndex = randomIndex;
    }

  
    // Functions that control the sprite of the NPC (Happy sad dizzy)
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
