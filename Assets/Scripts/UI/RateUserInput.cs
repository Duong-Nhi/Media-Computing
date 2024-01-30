using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RateUserInput : MonoBehaviour

{

    public Image feedBackImage;

    public Image starFeedBackImage;

    public Image[] feedBackImages;
    public AudioManager audioManager;
    public LevelManager levelManager;
    public InputManager inputManager;

    public PlayerMovement player;

    public Text scoreText;

    public Text starsText;

    public Text comboText;

    public Text endText;

    public Canvas endScreen;

    public int[] multiplicatorThresholds = new int[] {5, 10 , 20, 50};
    public float[] trophyThresholds = new float[] {0.15f, 0.3f, 0.4f, 0.6f, 0.8f, 0.9f};

    public Image[] trophyImages;
    public Image[] trophyImagesEnd;

    public Sprite[] trophySprites;
    public int throphiesEarned = 0;
    public string lastFeedback;

    public float playerOffset = 20;
    private int currentScore = 0;
    private int maximalPossibleScore = 0;
    public Image[] starImages;
    public Image[] starImagesEnd;
    public Sprite[] starSprites;
    public int currentStars = 0;
    private int currentCombo = 0;

    private int currentBadCombo = 0;
    private int maxCombo = 0;


    // Task per side (i.e. 2 for package and 1 for letter) Maybe extended with different package types (large takes more hits)
    private class Task {
        public int type;
        public int startBeat;
        public int totalHits;
        public int[] scores; //Array of size totalHits?

        public bool finished;
    }
    // On each input check closest beat -> Add score accordingly (Maybe penalty if its not the right one )

    private Task currentLeftTask;
    private Task currentRightTask;

    private bool feedbackIsInvoked = false;

    // EVENTS
    public delegate void TaskDoneLeftEventDelegate();
    public static event TaskDoneLeftEventDelegate taskDoneLeft;

    public delegate void TaskDoneRightEventDelegate();
    public static event TaskDoneRightEventDelegate taskDoneRight;

    public delegate void TaskStartPackageLeftEventDelegate();
    public static event TaskStartPackageLeftEventDelegate taskStartPackageLeft;

    public delegate void TaskStartPackageRightEventDelegate();
    public static event TaskStartPackageRightEventDelegate taskStartPackageRight;

    public delegate void TaskStartLetterLeftEventDelegate();
    public static event TaskStartLetterLeftEventDelegate taskStartLetterLeft;

    public delegate void TaskStartLetterRightEventDelegate();
    public static event TaskStartLetterRightEventDelegate taskStartLetterRight;

    public delegate void TaskNextLeftEventDelegate();
    public static event TaskNextLeftEventDelegate taskNextLeft;

    public delegate void TaskNextRightEventDelegate();
    public static event TaskNextRightEventDelegate taskNextRight;

    public delegate void TaskThrowLeftEventDelegate();
    public static event TaskThrowLeftEventDelegate taskThrowLeft;

    public delegate void TaskThrowRightEventDelegate();
    public static event TaskThrowRightEventDelegate taskThrowRight;

    public delegate void DropPackageLeftEventDelegate();
    public static event DropPackageLeftEventDelegate dropPackageLeft;

    public delegate void DropLetterLeftEventDelegate();
    public static event DropLetterLeftEventDelegate dropLetterLeft;

    public delegate void DropPackageRightEventDelegate();
    public static event DropPackageRightEventDelegate dropPackageRight;

    public delegate void DropLetterRightEventDelegate();
    public static event DropLetterRightEventDelegate dropLetterRight;

    public delegate void AutoAttackLeftLetterEventDelegate();
    public static event AutoAttackLeftLetterEventDelegate autoAttackLeftLetter;

    public delegate void AutoAttackLeftPackageEventDelegate();
    public static event AutoAttackLeftPackageEventDelegate autoAttackLeftPackage;

    public delegate void AutoAttackRightLetterEventDelegate();
    public static event AutoAttackRightLetterEventDelegate autoAttackRightLetter;

    public delegate void AutoAttackRightPackageEventDelegate();
    public static event AutoAttackRightPackageEventDelegate autoAttackRightPackage;

    public delegate void DeliverPackageLeftEventDelegate();
    public static event DeliverPackageLeftEventDelegate deliverPackageLeft;

    public delegate void DeliverLetterLeftEventDelegate();
    public static event DeliverLetterLeftEventDelegate deliverLetterLeft;

    public delegate void DeliverPackageRightEventDelegate();
    public static event DeliverPackageRightEventDelegate deliverPackageRight;

    public delegate void DeliverLetterRightEventDelegate();
    public static event DeliverLetterRightEventDelegate deliverLetterRight;

    //Same in Bad

    public delegate void DeliverPackageLeftBadEventDelegate();
    public static event DeliverPackageLeftBadEventDelegate deliverPackageLeftBad;

    public delegate void DeliverLetterLeftBadEventDelegate();
    public static event DeliverLetterLeftBadEventDelegate deliverLetterLeftBad;

    public delegate void DeliverPackageRightBadEventDelegate();
    public static event DeliverPackageRightBadEventDelegate deliverPackageRightBad;

    public delegate void DeliverLetterRightBadEventDelegate();
    public static event DeliverLetterRightBadEventDelegate deliverLetterRightBad;

    //private static RateUserInput instance;
    int currentBeat = -1;

    void Awake()
    {
        /*if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }*/
        //Subscribe events
        InputManager.onMailLeft += onMailLeft;
        InputManager.onMailRight += onMailRight;
        InputManager.onPackageLeft += onPackageLeft;
        InputManager.onPackageRight += onPackageRight;
        AudioManager.beatUpdated += onBeat;
    }
    void OnDestroy()
    {
        //Subscribe events
        InputManager.onMailLeft -= onMailLeft;
        InputManager.onMailRight -= onMailRight;
        InputManager.onPackageLeft -= onPackageLeft;
        InputManager.onPackageRight -= onPackageRight;
        AudioManager.beatUpdated -= onBeat;
    }

    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GetComponent<Text>();
        scoreText.text = "0";
        
        currentLeftTask = null;
        currentRightTask = null;

        endScreen.enabled = false;

        maximalPossibleScore = calcMaxPossiblePoints();
        Debug.Log("Maximum Possible Score: "+ maximalPossibleScore.ToString());

        feedBackImage.enabled = false;
        starFeedBackImage.enabled = false;

        Invoke("setBeatZero", 0.5f);
    }

    void setBeatZero()
    {
        currentBeat = 0;
    }

    // Display the current score every frame -> Maybe instead of every frame, only every user input (only time it changes)
    void Update()
    {

    }


    void onBeat()
    {
        currentBeat +=1;
        int newThrophiesEarned = calcTrophiesEarned();
        if (newThrophiesEarned > throphiesEarned)
        {

        if(PlayerPrefs.GetInt("lvlNum")==0){
            PlayerPrefs.SetInt("trophy", Math.Min(newThrophiesEarned, PlayerPrefs.GetInt("trophy")));
        }
        if(PlayerPrefs.GetInt("lvlNum")==1){
            PlayerPrefs.SetInt("trophy1", Math.Min(newThrophiesEarned, PlayerPrefs.GetInt("trophy1")));
        }
        if(PlayerPrefs.GetInt("lvlNum")==2){
            PlayerPrefs.SetInt("trophy2", Math.Min(newThrophiesEarned, PlayerPrefs.GetInt("trophy2")));
        }

            // Play Sound Effect?
            audioManager.playTrophy(Camera.main.transform.position);
            throphiesEarned = newThrophiesEarned;
        }
        // On last beat display the endscreen after 1 sec
        if(currentBeat >= audioManager.nBeats-1){
            //throphiesEarned = calcTrophiesEarned();
            displayEndScreen();
        }

        //int currentBeat = AudioManager.lastBeat;
        if (currentLeftTask != null)
        {
            if(currentBeat >= currentLeftTask.startBeat + currentLeftTask.totalHits)
            {
                // Do some event, as package has to be dropped here
                //EVENT: DROP PACKAGE LEFT -> Left arm should subscribe here and go back to idle; NPC Spawner should spawn package on the ground left of player
                if(taskDoneLeft!=null)
                {
                    taskDoneLeft();
                }
                // Drop event
                if(!currentLeftTask.finished && currentLeftTask.type == 1 && dropLetterLeft != null)
                {
                    dropLetterLeft();
                    currentCombo = 0;
                    currentBadCombo += 1;
                    comboText.text = "";
                }
                else
                {
                    if(!currentLeftTask.finished && currentLeftTask.type == 2 && dropPackageLeft != null)
                    {
                        dropPackageLeft();
                        currentCombo = 0;
                        currentBadCombo += 1;
                        comboText.text = "";
                    }
                    else
                    {
                        //Add new types here
                    }
                }
                

                // Some logic with the score (dropped letter)
                currentLeftTask = null;
            }
        }
        //Drop Task if not completed:
        if (currentRightTask != null)
        {
        if(currentBeat >= currentRightTask.startBeat + currentRightTask.totalHits)
            {
                // Do some event, as package has to be dropped here
                //EVENT: DROP PACKAGE RIGHT -> Left arm should subscribe here and go back to idle; NPC Spawner should spawn package on the ground left of player
                if(taskDoneRight!=null)
                {
                    taskDoneRight();
                }
                // Drop event
                if(!currentRightTask.finished && currentRightTask.type == 1 && dropLetterRight != null)
                {
                    dropLetterRight();
                    currentCombo = 0;
                    currentBadCombo += 1;
                    comboText.text = "";
                }
                else
                {
                    if(!currentRightTask.finished && currentRightTask.type == 2 && dropPackageRight != null)
                    {
                        dropPackageRight();
                        currentCombo = 0;
                        currentBadCombo += 1;
                        comboText.text = "";
                    }
                    else
                    {
                        //Add new types here
                    }
                }

                // Some logic with the score (droppped package)
                currentRightTask = null;
            }
        }

        // Check LevelManager beat tuple -> if not (0,0) or other special tiles -> Create Task object 
        (int taskLeftId, int taskRightId) = getTaskFromLevel(currentBeat);

        int leftHits = levelManager.idToHits(taskLeftId);
        // This means we have to create a task object for this
        if (leftHits>0)
        {
            //assert currentLeftTask == null;
            Task newLeftTask = new Task()
            {
                type = taskLeftId,
                startBeat = currentBeat,
                totalHits = leftHits,
                scores = new int[leftHits-1],
                finished = false
            };
            currentLeftTask = newLeftTask;
            //check for what type
            if(taskLeftId == 1 && taskDoneLeft!=null)
            {
                taskStartLetterLeft();
            }
            else
            {
                if(taskLeftId == 2 && taskStartPackageLeft!=null)
                {
                    taskStartPackageLeft();
                }
                else{
                    // add events for new task types here
                }
            }

        }

        int rightHits = levelManager.idToHits(taskRightId);
        // This means we have to create a task object for this
        if (rightHits>0)
        {
            //assert currentLeftTask == null;
            Task newRightTask = new Task()
            {
                type = taskRightId,
                startBeat = currentBeat,
                totalHits = rightHits,
                scores = new int[rightHits-1],
                finished = false
            };
            currentRightTask = newRightTask;
            //check for what type
            if(taskRightId == 1 && taskStartLetterRight!=null)
            {
                taskStartLetterRight();
            }
            else
            {
                if(taskRightId == 2 && taskStartPackageRight!=null)
                {
                    taskStartPackageRight();
                }
                else{
                    // add events for new task types here
                }
            }

        }
        adaptDistortion(currentBadCombo);
    }


    // HandleTasks -> If task completed return true and the scoring of task
    private (bool, int, bool) handleEvents(Task task, int eventType)
    {
        bool completed = false;
        int score = 0;
        bool animation = false;

        (float distance, int beat) = distanceToClosestBeat();
        int hitIndex = beat - task.startBeat - 1;

        // Check if task is of correct type
        if (!(task.type == eventType)){
            // some event ? Incorrect input type, maybe sound or visual error

            //extra custom penalty for thowing wrong thing
            score -= 1000;
            currentCombo = 0;
        }
        else {
            // Only update if that beat not scored already -> First input counts
            if(hitIndex <= -1 )
            {
                //Treat as a bad attempt to hit first index
                if(task.scores[0] == 0)
                {
                    (string feedback, int rating) = RatingHelper(levelManager.distancePerBeat - distance);
                    displayFeedback(feedback);
                    if(feedback == "Perfect" || feedback == "Good")
                    {
                        if(currentBadCombo > 0){
                            currentBadCombo = Mathf.Min(currentBadCombo, 10);
                            currentBadCombo -= 2;
                            currentBadCombo = Mathf.Max(currentBadCombo, 0);
                        }
                        else
                        {
                            currentCombo += 1;
                            currentBadCombo = 0;
                        }
                        
                    }
                    else
                    {
                        currentCombo = 0;
                        if(feedback == "Miss")
                        {
                            currentBadCombo += 1;
                        }
                    }
                    task.scores[0] = rating;
                    // do something with feedback? Event? 

                    animation = true;

                    
                }
                else{
                    // This beat was already hit! So penalty for additional throw
                    currentCombo = 0;
                    task.scores[0] = -1000;

                }
                if (0 == task.totalHits-2){
                    // throw package event as task is already done

                    animation = true;
                    completed = true;
                }
                
            }
            else if (hitIndex >= task.totalHits-1) {

                //Treat as bad attempt to hit last index
                if(task.scores[hitIndex-1] == 0)
                {
                    (string feedback, int rating) = RatingHelper(levelManager.distancePerBeat - distance);
                    displayFeedback(feedback);
                    if(feedback == "Perfect" || feedback == "Good")
                    {
                        if(currentBadCombo > 0){
                            currentBadCombo = Mathf.Min(currentBadCombo, 10);
                            currentBadCombo -= 2;
                            currentBadCombo = Mathf.Max(currentBadCombo, 0);
                        }
                        else
                        {
                            currentCombo += 1;
                            currentBadCombo = 0;
                        }
                    }
                    else
                    {
                        currentCombo = 0;
                        if(feedback == "Miss")
                        {
                            currentBadCombo += 1;
                        }
                    }
                    task.scores[hitIndex-1] = rating;
                    // do something with feedback? Event? 
                    // throw package event
                    animation = true;
                    completed = true;
                }
                else{
                    // This beat was already hit! So penalty for additional throw
                    // Can this be even reached? Task should be finished already
                    currentCombo = 0;
                    task.scores[hitIndex-1] = -1000;

                }

                
            }
            else {
                // Check if this beat is already scored
                if(task.scores[hitIndex] == 0)
                {
                    (string feedback, int rating) = RatingHelper(distance);
                    displayFeedback(feedback);
                    if(feedback == "Perfect" || feedback == "Good")
                    {
                        if(currentBadCombo > 0){
                            currentBadCombo = Mathf.Min(currentBadCombo, 10);
                            currentBadCombo -= 2;
                            currentBadCombo = Mathf.Max(currentBadCombo, 0);
                        }
                        else
                        {
                            currentCombo += 1;
                            currentBadCombo = 0;
                        }
                    }
                    else
                    {
                        currentCombo = 0;
                        if(feedback == "Miss")
                        {
                            currentBadCombo += 1;
                        }
                    }

                    task.scores[hitIndex] = rating;
                    animation = true;
                    // do something with feedback? Event? 
                }
                else
                {
                    // HERE FEEDBACK FOR WRONG INPUT -> EVENT?
                    currentCombo = 0;
                    task.scores[hitIndex] = -1000;
                }

                if(hitIndex == task.totalHits-2)
                {
                    // throw package event as this was the last action
                    //EVENT: TASK DONE LEFT -> LeftArm: Back to idle position left
                    completed = true;
                    animation = true;
                }
            }
        }
        if (completed)
        {
            // sum over scores as task score
            for (int i=0; i<task.totalHits-1; i++)
            {
                score += task.scores[i];
            }
            // Apply multiplicator of combo
            float multiplicator = 1f;
            for(int i=0; i< multiplicatorThresholds.Length; i++)
            {
                if(currentCombo >= multiplicatorThresholds[i])
                {
                    multiplicator += 0.25f;
                }
            }
            score = (int) multiplicator * score;
            
        }
        if (currentCombo > 0){
            comboText.text = "Combo " + currentCombo.ToString() +"x";
            if(currentCombo > maxCombo)
            {
                maxCombo = currentCombo;
            }
        }
        else
        {
            comboText.text = "";
        }

        adaptDistortion(currentBadCombo);

        return (completed, score, animation);
    }

    void onMailLeft()
    {
        if(currentLeftTask != null && !currentLeftTask.finished)
        {
            (bool completed, int score, bool animation) = handleEvents(currentLeftTask, 1);

            if (completed)
            {
                //currentLeftTask = null;
                currentScore += score;
                //EVENT: TASK DONE LEFT -> LeftArm: Back to idle position left
                if(animation && taskThrowLeft != null)
                {
                    taskThrowLeft();
                }
                currentLeftTask.finished = true;

                //Deliver Event depending on score
                if(score > 0)
                {
                    if(deliverLetterLeft != null)
                    {
                        deliverLetterLeft();
                    }
                }
                else{
                    if(deliverLetterLeftBad != null)
                    {
                        deliverLetterLeftBad();
                    }
                }
            }
            else
            {
                if(animation && taskNextLeft != null)
                {
                    taskNextLeft();
                }
                
            }
            scoreText.text = currentScore.ToString();
        }
        else {
            // Idea is to implement some kind of auto attacks to trigger events in scene
            if(autoAttackLeftLetter != null && currentBeat < audioManager.nBeats-1)
            {
                autoAttackLeftLetter();
            }
        }
    }

    void onMailRight()
    {
        if (currentRightTask != null && !currentRightTask.finished)
        {
            (bool completed, int score, bool animation) = handleEvents(currentRightTask, 1);

            if (completed)
            {
                //currentRightTask = null;
                currentScore += score;

                if(animation && taskThrowRight!= null)
                {
                    taskThrowRight();
                }
                currentRightTask.finished = true;

                //Deliver events
                if(score > 0)
                {
                    if(deliverLetterRight != null)
                    {
                        deliverLetterRight();
                    }
                }
                else{
                    if(deliverLetterRightBad != null)
                    {
                        deliverLetterRightBad();
                    }
                }
            }
            else
            {
                if(animation && taskNextRight != null)
                {
                    taskNextRight();
                }
            }
            scoreText.text = currentScore.ToString();
        }
        else {
            // Idea is to implement some kind of auto attacks to trigger events in scene
            if(autoAttackRightLetter != null && currentBeat < audioManager.nBeats-1)
            {
                autoAttackRightLetter();
            }
        }
    }

    void onPackageLeft()
    {   
        if (currentLeftTask != null && !currentLeftTask.finished) 
        {
            (bool completed, int score, bool animation) = handleEvents(currentLeftTask, 2);
            if (completed)
            {
                //currentLeftTask = null;
                currentScore += score;

                if(animation && taskThrowLeft != null)
                {
                    taskThrowLeft();
                }
                currentLeftTask.finished = true;
                //Deliver events
                if(score > 1500)
                {
                    if(deliverPackageLeft != null)
                    {
                        deliverPackageLeft();
                    }
                }
                else{
                    if(deliverPackageLeftBad != null)
                    {
                        deliverPackageLeftBad();
                    }
                }
            }
            else
            {
                if(animation && taskNextLeft != null)
                {
                    taskNextLeft();
                }
                
            }
            scoreText.text = currentScore.ToString();
        }
        else {
            // Idea is to implement some kind of auto attacks to trigger events in scene
            if(autoAttackLeftPackage != null && currentBeat < audioManager.nBeats-1)
            {
                autoAttackLeftPackage();
            }
        }
    }

    void onPackageRight()
    {
        if (currentRightTask != null && !currentRightTask.finished)
        {
            (bool completed, int score, bool animation) = handleEvents(currentRightTask, 2);
            if (completed)
            {
                //currentRightTask = null;
                currentScore += score;
                if(animation && taskThrowRight!= null)
                {
                    taskThrowRight();
                }
                currentRightTask.finished = true;
                //Deliver events
                if(score > 1500)
                {
                    if(deliverPackageRight != null)
                    {
                        deliverPackageRight();
                    }
                }
                else{
                    if(deliverPackageRightBad != null)
                    {
                        deliverPackageRightBad();
                    }
                }
            }
            else
            {
                if(animation && taskNextRight != null )
                {
                    taskNextRight();
                }
            }
            scoreText.text = currentScore.ToString();
        }
        else {
            // Idea is to implement some kind of auto attacks to trigger events in scene
            if(autoAttackRightPackage != null && currentBeat < audioManager.nBeats-1)
            {
                autoAttackRightPackage();
            }
        }
    }

    void OnGUI()
        {   
            GUILayout.BeginArea(new Rect(500, 0, 475, 900));

            int[] scores = new int[3];
            if (currentLeftTask != null)
            {
                scores = currentLeftTask.scores;
            }
            //GUILayout.Box(String.Format("LeftScores = {0}, {1}, {2}", scores[0], scores[1], scores[2]));
            GUILayout.EndArea();
        }

    public void collectedStart()
    {
        currentStars += 1;


        if(PlayerPrefs.GetInt("lvlNum")==0){
            PlayerPrefs.SetInt("stars", currentStars-1);
        }
        if(PlayerPrefs.GetInt("lvlNum")==1){
            PlayerPrefs.SetInt("stars1", currentStars-1);
        }
        if(PlayerPrefs.GetInt("lvlNum")==2){
            PlayerPrefs.SetInt("stars2", currentStars-1);
        }



        currentScore += 10000;
        starImages[currentStars-2].sprite = starSprites[1];
        PlayerPrefs.SetInt("stars",currentStars);

        starsText.text = "Stars: " + currentStars.ToString();

        starFeedBackImage.enabled = true;
        Invoke("resetImageStar", 0.5f);

    }

    void resetImageStar()
    {
        starFeedBackImage.enabled = false;
    }


    // Rename? With task i mean the elements of the level i.e. (1,0)
    private (int, int) getTaskFromLevel(int beat)
    {
        int leftTask = 0;
        int rightTask = 0;

        // fist check for task id 1
        int taskHitCount = levelManager.idToHits(1);
        //beat -1 because we index beats starting from 1, array from 0; taskHitCount-1 as the current beat counts as first hit and taskhitcount-1 as last
        (int left, int right) = levelManager.levelSequence[beat-1 + taskHitCount-1];
        if (left == 1)
        {
            leftTask = 1;
        }
        if (right == 1)
        {
            rightTask = 1;
        }
        //-------------------------
        // next check for task id 2
        taskHitCount = levelManager.idToHits(2);
        //beat -1 because we index beats starting from 1, array from 0; taskHitCount-1 as the current beat counts as first hit and taskhitcount-1 as last
        (left, right) = levelManager.levelSequence[beat-1 + taskHitCount-1];
        if (left == 2 && leftTask == 0)
        {
            leftTask = 2;
        }
        if (right == 2 && rightTask == 0)
        {
            rightTask = 2;
        }

        return (leftTask, rightTask);
    }

    // Calculates distance between player and the closest beat
    private (float, int) distanceToClosestBeat()
    {
        float minDistance = 0;
        int closestBeat = 0;

        // -1 as at beat 1 we start in origin (0,0,0)
        float lastBeatPos = (audioManager.timelineInfo.elapsedBeats-1) * levelManager.distancePerBeat;
        float nextBeatPos = (audioManager.timelineInfo.elapsedBeats) * levelManager.distancePerBeat;

        float playerPosition = player.transform.position.z - playerOffset;

        // minimum distance from player to next or latest beat
        if (nextBeatPos - playerPosition < playerPosition - lastBeatPos)
        {
            minDistance = nextBeatPos - playerPosition;
            closestBeat = audioManager.timelineInfo.elapsedBeats + 1;
        }
        else
        {
            minDistance = playerPosition - lastBeatPos;
            closestBeat = audioManager.timelineInfo.elapsedBeats;
        }
        
        return (minDistance, closestBeat);
    }
    

    private (string, int) RatingHelper(double pDistance) 
    {   
        //Normalize distance
        float distance = Mathf.Abs((float) pDistance)/(float) levelManager.distancePerBeat;

        //Debug.Log("Distance to closest beat: " + Mathf.Abs((float) pDistance).ToString() + "; Normalized: " + distance.ToString());
        string feedback;
        int score;
        switch (distance) 
        {
            case <= 0.1f:
                //Add bonuspoints for streaks?
                feedback = "Perfect";
                score = 1000;
                //currentScore += 1000;
                break;
            case <= 0.15f:
                feedback = "Good";
                score = 500;
                //currentScore += 800;
                break;
            case <= 0.3f:
                feedback = "Okay";
                score = 250;
                //currentScore += 500;
                break;
            default:
                feedback = "Miss";
                score = -1000;
                //currentScore -= 1000;
                break;
        }

        return (feedback, score);
    }

    private int calcTrophiesEarned()
    {
        int trophies = 0;
        for(int i=0; i<trophyThresholds.Length; i++)
        {
            if((float) currentScore/ (float) maximalPossibleScore > trophyThresholds[i])
            {
                trophies += 1;
               
                if(trophies > 3){
                    trophyImages[trophies-4].sprite = trophySprites[1];

                }else{
                    trophyImages[trophies-1].sprite = trophySprites[0];
                }


            }
        }
        return trophies;
    }

    void displayEndScreen()
    {
        //Text endText = endScreen.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        if (endText != null)
        {
            endText.text = "Score: "+ currentScore.ToString();
        }
        endScreen.enabled = true;
        

        for(int i = 0; i < 3; i++){
            starImagesEnd[i].sprite = starImages[i].sprite;
            trophyImagesEnd[i].sprite = trophyImages[i].sprite;

        }
    }

    private int calcMaxPossiblePoints(){
        int maxPoints = 0;
        int maxPossibleCombo = 0;
        for(int i=0; i<audioManager.nBeats; i++){
            (int taskLeftId, int taskRightId) = levelManager.levelSequence[i];

            if (taskLeftId == 1)
            {
                maxPossibleCombo += 1;
            }
            if (taskLeftId == 2)
            {
                maxPossibleCombo += 3;
            }
            if (taskRightId == 1)
            {
                maxPossibleCombo += 1;
            }
            if (taskRightId == 2)
            {
                maxPossibleCombo += 3;
            }

            // Apply multiplicator of combo
            float multiplicator = 1f;
            for(int j=0; j< multiplicatorThresholds.Length; j++)
            {
                if(maxPossibleCombo >= multiplicatorThresholds[j])
                {
                    multiplicator += 0.25f;
                }
            }

            if (taskLeftId == 1)
            {
                maxPoints += (int) (1000f * multiplicator);
            }
            if (taskLeftId == 2)
            {
                maxPoints += (int) (3000f * multiplicator);
            }
            if (taskRightId == 1)
            {
                maxPoints += (int) (1000f * multiplicator);
            }
            if (taskRightId == 2)
            {
                maxPoints += (int) (3000f * multiplicator);
            }
        }
        return maxPoints;
    }

    void displayFeedback(string feedback)
    {
        feedBackImage.enabled = true;
        switch (feedback)
        {
            case "Perfect":
                feedBackImage.sprite = feedBackImages[0].sprite;
                break;
            case "Good":
                feedBackImage.sprite = feedBackImages[1].sprite;
                break;
            case "Okay":
                feedBackImage.sprite = feedBackImages[2].sprite;
                break;
            case "Miss":
                feedBackImage.sprite = feedBackImages[3].sprite;
                break;
            default:
                feedBackImage.sprite = feedBackImages[3].sprite;
                Debug.Log("Unknow Feedback to be displayed");
                break;
        }
        

        if (!feedbackIsInvoked)
        {
            // Call YourFunction and set isFunctionInvoked to true
            Invoke("resetImage", 0.5f);
            feedbackIsInvoked = true;
        }
        else
        {
            // If the function is already invoked, reset the timer
            CancelInvoke("resetImage");
            Invoke("resetImage", 0.5f);
        }
    }

    void resetImage()
    {
        feedBackImage.enabled = false;
        feedbackIsInvoked = false;
    }

    void adaptDistortion(int pCurrentBadCombo)
    {  
        float strength = Mathf.Min(pCurrentBadCombo/10f, 1f);
        audioManager.applyEffect(strength);

    }

}
