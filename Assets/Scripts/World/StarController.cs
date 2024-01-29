using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour
{
    public Sprite collectedSprite;

    public RateUserInput rateUserInput;

    private SpriteRenderer spriteRenderer;

    

    private bool collected = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeCollected()
    {
        // add animation
        spriteRenderer.sprite = collectedSprite;
        collected = true;
        rateUserInput.collectedStart();
    }

    public bool isCollected()
    {
        return collected;
    }
}
