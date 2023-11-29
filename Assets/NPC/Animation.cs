using UnityEngine;

public class SpriteChangeController : MonoBehaviour
{
    public Sprite newSprite;  // Reference to the new sprite

    private Animator animator;
    private int animationCount = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the animation has finished playing
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            // Reset the animation count when the animation starts
            animationCount = 0;
        }

        // Check if the animation has played twice
        if (animationCount >= 2)
        {
            // Trigger the next sprite change
            ChangeSprite();
            // Reset the animation count
            animationCount = 0;
        }
    }

    // Called by an Animation Event at the end of the animation
    public void OnAnimationEnd()
    {
        // Increment the animation count when the animation ends
        animationCount++;
    }

    // Method to change the sprite
    private void ChangeSprite()
    {
        // Change the sprite of the GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or newSprite is null. Make sure to assign the new sprite in the Inspector.");
        }
    }
}
