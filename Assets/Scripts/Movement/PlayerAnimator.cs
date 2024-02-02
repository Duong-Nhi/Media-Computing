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
    void OnDestroy()
    {
        //Unsubscribe events
        AudioManager.beatUpdated -= onBeat;
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


    // Animate the player by changing sprites every beat until reaching the last beat
    void onBeat()
    {
        currentBeat++;
        if(currentBeat < audioManager.nBeats-1){
            animatePlayer();
        }
    }

    void animatePlayer()
    {
        spriteRenderer.sprite = playerAnimationSprites[currentBeat%playerAnimationSprites.Length];
    }

}
