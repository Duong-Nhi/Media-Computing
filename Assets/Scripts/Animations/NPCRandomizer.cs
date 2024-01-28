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
        characterIndex = 0;
    }

    // Implement functionalities for changing between sprites including animation
}
