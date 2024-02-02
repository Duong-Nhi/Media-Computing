using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public Sprite collectedSprite;

    public RateUserInput rateUserInput;

    private SpriteRenderer spriteRenderer;

    private bool collected = false;

    // Get SpriteRenderer Component
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // Make NPC Happy and call collectedStar function to include it in rating
    public void makeCollected()
    {
        // Idea: Add special animation
        spriteRenderer.sprite = collectedSprite;
        collected = true;
        rateUserInput.collectedStart();
    }

    public bool isCollected()
    {
        return collected;
    }
}
