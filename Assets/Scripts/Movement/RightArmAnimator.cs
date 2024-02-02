using UnityEngine;

public class RightArmAnimator : MonoBehaviour
{

    public LevelManager levelManager;
    public AudioManager audioManager;
    public RateUserInput userInputRater;
    public Sprite[] letterArmSprites;
    public Sprite[] packageArmSprites;

    public Sprite[] letterArmSpritesGlow;
    public Sprite[] packageArmSpritesGlow;  

    public Sprite idleSprite;
    private int currentSpriteIdx = 0;
    private int taskId = 0;
    private SpriteRenderer spriteRenderer;

    private bool throwingIsInvoked = false;

    void Awake()
    {
        //Subscribe events
        RateUserInput.taskStartLetterRight += onTaskStartLetterRight;

        RateUserInput.taskStartPackageRight += onTaskStartPackageRight;
        RateUserInput.taskDoneRight += onTaskDoneRight;
        RateUserInput.taskNextRight += onTaskNextRight;
        RateUserInput.taskThrowRight += onTaskThrowRight;

        RateUserInput.autoAttackRightLetter += onAutoAttackRightLetter;
        RateUserInput.autoAttackRightPackage += onAutoAttackRightPackage;
    }

    void OnDestroy()
    {
        //Subscribe events
        RateUserInput.taskStartLetterRight -= onTaskStartLetterRight;

        RateUserInput.taskStartPackageRight -= onTaskStartPackageRight;
        RateUserInput.taskDoneRight -= onTaskDoneRight;
        RateUserInput.taskNextRight -= onTaskNextRight;
        RateUserInput.taskThrowRight -= onTaskThrowRight;

        RateUserInput.autoAttackRightLetter -= onAutoAttackRightLetter;
        RateUserInput.autoAttackRightPackage -= onAutoAttackRightPackage;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Check if the SpriteRenderer component is found
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found");
        }

        spriteRenderer.sprite = idleSprite;

        //Check if glow enabled
        if(levelManager.glowEnabled)
        {
            letterArmSprites = letterArmSpritesGlow;
            packageArmSprites = packageArmSpritesGlow;
        }

    }

    // Following functions handle the displayed sprites on all events related to the left arm

    // Called by an Animation Event at the end of the animation -> Indicate the new task
    void onTaskStartLetterRight(){
        spriteRenderer.sprite = letterArmSprites[0];
        currentSpriteIdx = 0;
        taskId = 1;
        CancelInvoke("BackToIdle");
    }

    void onTaskStartPackageRight(){
        spriteRenderer.sprite = packageArmSprites[0];
        currentSpriteIdx = 0;
        taskId = 2;
        CancelInvoke("BackToIdle");
    }

    // Displays the next sprite in the array
    void onTaskNextRight(){
        if(taskId == 1){
            spriteRenderer.sprite = letterArmSprites[Mathf.Min(currentSpriteIdx+1, letterArmSprites.Length-1)];
            currentSpriteIdx ++;
        }
        else{
            if(taskId == 2){
                spriteRenderer.sprite = packageArmSprites[Mathf.Min(currentSpriteIdx+1, packageArmSprites.Length-1)];
                currentSpriteIdx ++;
            }
            else
            {
                // Add here if new tasks added
            }
        }
    }

    // display the last sprite, as the item is thrown
    void onTaskThrowRight(){
        if(taskId == 1){
            spriteRenderer.sprite = letterArmSprites[letterArmSprites.Length-1];
            currentSpriteIdx ++;
        }
        else{
            if(taskId == 2){
                spriteRenderer.sprite = packageArmSprites[packageArmSprites.Length-1];
                currentSpriteIdx ++;
            }
            else
            {
                // Add here if new tasks added
            }
        }
    }

    // go back to idle sprite if task is done
    void onTaskDoneRight(){
        spriteRenderer.sprite = idleSprite;
        currentSpriteIdx = 0;
        taskId = 0;
    }
    
    // Animate autoattacks by displaying throwsprite for 0.5 seconds
    void onAutoAttackRightLetter()
    {
        spriteRenderer.sprite = letterArmSprites[letterArmSprites.Length-1];
        if (!throwingIsInvoked)
        {
            // display thrown hand for 0.5 seconds
            Invoke("BackToIdle", 0.5f);
            throwingIsInvoked = true;
        }
        else
        {
            // reset timer and display thrown hand for 0.5 seconds
            CancelInvoke("BackToIdle");
            Invoke("BackToIdle", 0.5f);
        }
        
    }

    // Animate autoattacks by displaying throwsprite for 0.5 seconds
    void onAutoAttackRightPackage()
    {
        spriteRenderer.sprite = packageArmSprites[packageArmSprites.Length-1];
        if (!throwingIsInvoked)
        {
            // display thrown hand for 0.5 seconds
            Invoke("BackToIdle", 0.5f);
            throwingIsInvoked = true;
        }
        else
        {
            // reset timer and display thrown hand for 0.5 seconds
            CancelInvoke("BackToIdle");
            Invoke("BackToIdle", 0.5f);
        }
    }

    // Go back to idle animation
    void BackToIdle()
    {
        spriteRenderer.sprite = idleSprite;
        throwingIsInvoked = false;
    }

}
