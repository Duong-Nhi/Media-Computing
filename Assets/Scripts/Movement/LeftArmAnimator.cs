using UnityEngine;

public class LeftArmAnimator : MonoBehaviour
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

    //Test
    private int currentBeat = 0;

    void Awake()
    {
        //Subscribe events
        //AudioManager.beatUpdated += onBeat;
        RateUserInput.taskStartLetterLeft += onTaskStartLetterLeft;
        RateUserInput.taskStartPackageLeft += onTaskStartPackageLeft;


        RateUserInput.taskDoneLeft += onTaskDoneLeft;
        RateUserInput.taskNextLeft += onTaskNextLeft;
        RateUserInput.taskThrowLeft += onTaskThrowLeft;


        RateUserInput.autoAttackLeftLetter += onAutoAttackLeftLetter;
        RateUserInput.autoAttackLeftPackage += onAutoAttackLeftPackage;

    }

    void onBeat()
    {
        currentBeat++;
        spriteRenderer.sprite = packageArmSprites[currentBeat%packageArmSprites.Length];

    }
    void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(spriteRenderer);
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

    void Update()
    {
        //smth
    }

    // Called by an Animation Event at the end of the animation
    
    void onTaskStartLetterLeft(){
        spriteRenderer.sprite = letterArmSprites[0];
        currentSpriteIdx = 0;
        taskId = 1;
        CancelInvoke("BackToIdle");
    }

    void onTaskStartPackageLeft(){
        spriteRenderer.sprite = packageArmSprites[0];
        currentSpriteIdx = 0;
        taskId = 2;
        CancelInvoke("BackToIdle");
    }

    void onTaskNextLeft(){
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

    void onTaskThrowLeft(){
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

    void onTaskDoneLeft(){
        spriteRenderer.sprite = idleSprite;
        currentSpriteIdx = 0;
        taskId = 0;
    }

    void onAutoAttackLeftLetter()
    {
        spriteRenderer.sprite = letterArmSprites[letterArmSprites.Length-1];
        if (!throwingIsInvoked)
        {
            // Call YourFunction and set isFunctionInvoked to true
            Invoke("BackToIdle", 0.5f);
            throwingIsInvoked = true;
        }
        else
        {
            // If the function is already invoked, reset the timer
            CancelInvoke("BackToIdle");
            Invoke("BackToIdle", 0.5f);
        }
        
    }

    void onAutoAttackLeftPackage()
    {
        spriteRenderer.sprite = packageArmSprites[packageArmSprites.Length-1];
        if (!throwingIsInvoked)
        {
            // Call YourFunction and set isFunctionInvoked to true
            Invoke("BackToIdle", 0.5f);
            throwingIsInvoked = true;
        }
        else
        {
            // If the function is already invoked, reset the timer
            CancelInvoke("BackToIdle");
            Invoke("BackToIdle", 0.5f);
        }
    }

    void BackToIdle()
    {
        spriteRenderer.sprite = idleSprite;
        throwingIsInvoked = false;
    }
    
    
}
