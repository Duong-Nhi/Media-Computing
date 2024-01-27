using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public AudioManager audioManager;
    public Sprite[] playerAnimationSprites;  
    private int currentBeat;

    private SpriteRenderer spriteRenderer;


    void Awake()
    {
        //Subscribe events
        AudioManager.beatUpdated += onBeat;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer component is found
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found");
        }

    }

    void Update()
    {
        //smth
    }

    // Called by an Animation Event at the end of the animation
    
    void onBeat()
    {
        currentBeat++;
        animatePlayer();
    }

    void animatePlayer()
    {
        spriteRenderer.sprite = playerAnimationSprites[currentBeat%playerAnimationSprites.Length];
    }

}
