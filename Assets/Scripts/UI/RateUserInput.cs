using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RateUserInput : MonoBehaviour

{
    public AudioManager audioManager;
    public LevelManager levelManager;
    public InputManager inputManager;

    public PlayerMovement player;

    public Text scoreText;

    public string lastFeedback;
    private int currentScore = 0;


    // Task per side (i.e. 2 for package and 1 for letter) Maybe extebd with different package types (large takes more hits)
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


    int currentBeat = 0;

    void Awake()
    {
        //Subscribe events
        InputManager.onMailLeft += onMailLeft;
        InputManager.onMailRight += onMailRight;
        InputManager.onPackageLeft += onPackageLeft;
        InputManager.onPackageRight += onPackageRight;
        AudioManager.beatUpdated += onBeat;
    }

    // Start is called before the first frame update
    void Start()
    {
        //scoreText = GetComponent<Text>();
        scoreText.text = "0 Points";
        
        currentLeftTask = null;
        currentRightTask = null;
    }

    // Display the current score every frame -> Maybe instead of every frame, only every user input (only time it changes)
    void Update()
    {

    }


    void onBeat()
    {
        currentBeat +=1;
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
                }
                else
                {
                    if(!currentLeftTask.finished && currentLeftTask.type == 2 && dropPackageLeft != null)
                    {
                        dropPackageLeft();
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
                }
                else
                {
                    if(!currentRightTask.finished && currentRightTask.type == 2 && dropPackageRight != null)
                    {
                        dropPackageRight();
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
            score -= 100; 
        }
        else {
            // Only update if that beat not scored already -> First input counts
            if(hitIndex <= -1 )
            {
                //Treat as a bad attempt to hit first index
                if(task.scores[0] == 0)
                {
                    (string feedback, int rating) = RatingHelper(levelManager.distancePerBeat - distance);
                    task.scores[0] = rating;
                    // do something with feedback? Event? 

                    animation = true;

                    
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
                    task.scores[hitIndex-1] = rating;
                    // do something with feedback? Event? 
                    // throw package event
                    animation = true;
                    completed = true;
                }

                
            }
            else {
                // Check if this beat is already scored
                if(task.scores[hitIndex] == 0)
                {
                    (string feedback, int rating) = RatingHelper(distance);
                    task.scores[hitIndex] = rating;
                    animation = true;
                    // do something with feedback? Event? 
                }
                else
                {
                    // HERE FEEDBACK FOR WRONG INPUT -> EVENT?
                    task.scores[hitIndex] -= 100;
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
            
        }
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
                if(animation && taskDoneLeft != null)
                {
                    taskNextLeft();
                }
                currentLeftTask.finished = true;
                //Deliver Event
                if(deliverLetterLeft != null)
                {
                    deliverLetterLeft();
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
            if(autoAttackLeftLetter != null)
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

                if(animation && taskDoneRight!= null)
                {
                    taskNextRight();
                }
                currentRightTask.finished = true;
                //Deliver Event
                if(deliverLetterRight != null)
                {
                    deliverLetterRight();
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
            if(autoAttackRightLetter != null)
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

                if(animation && taskDoneLeft != null)
                {
                    taskNextLeft();
                }
                currentLeftTask.finished = true;
                //Deliver Event
                if(deliverPackageLeft != null)
                {
                    deliverPackageLeft();
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
            if(autoAttackLeftPackage != null)
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
                if(animation && taskDoneRight!= null)
                {
                    taskNextRight();
                }
                currentRightTask.finished = true;
                //Deliver Event
                if(deliverPackageRight != null)
                {
                    deliverPackageRight();
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
            if(autoAttackRightPackage != null)
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

/*
    void onMailLeft()
    {
        (float distance, int beat) = distanceToClosestBeat();
        (int task,_) = getTaskFromLevel(beat);
        if (task == 1){
            // TODO: do something with feedback. maybe move rate user input somewhere else (input manager) and make events if score changes
            (string feedback, int score) = RatingHelper(distance); 
            lastFeedback = feedback;
        }
        else
        {
            lastFeedback = "Wrong Delivery";
            currentScore -= 1000;
        }
        scoreText.text = currentScore.ToString();
    }
    void onMailRight()
    {
        (float distance, int beat) = distanceToClosestBeat();
        (_, int task) = getTaskFromLevel(beat);
        if (task == 1){
            // TODO: do something with feedback. maybe move rate user input somewhere else (input manager) and make events if score changes
            (string feedback, int score) = RatingHelper(distance); 
            lastFeedback = feedback;
        }
        else
        {
            lastFeedback = "Wrong Delivery";
            currentScore -= 1000;
        }
        scoreText.text = currentScore.ToString();
        
    }
    void onPackageLeft()
    {
        (float distance, int beat) = distanceToClosestBeat();
        (int task,_) = getTaskFromLevel(beat);
        if (task == 2){
            // TODO: do something with feedback. maybe move rate user input somewhere else (input manager) and make events if score changes
            (string feedback, int score) = RatingHelper(distance); 
            lastFeedback = feedback;
        }
        else
        {
            lastFeedback = "Wrong Delivery";
            currentScore -= 1000;
        }
        scoreText.text = currentScore.ToString();
    }
    void onPackageRight()
    {
        (float distance, int beat) = distanceToClosestBeat();
        (_, int task) = getTaskFromLevel(beat);
        if (task == 2){
            // TODO: do something with feedback. maybe move rate user input somewhere else (input manager) and make events if score changes
            (string feedback, int score) = RatingHelper(distance); 
            lastFeedback = feedback;
        }
        else
        {
            lastFeedback = "Wrong Delivery";
            currentScore -= 1000;
        }
        scoreText.text = currentScore.ToString();
    }*/

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

        // -1 because we index beats starting from 1, array from 0
        //return levelManager.levelSequence[beat-1];

    }

    // Calculates distance between player and the closest beat
    private (float, int) distanceToClosestBeat()
    {
        float minDistance = 0;
        int closestBeat = 0;

        // -1 as at beat 1 we start in origin (0,0,0)
        float lastBeatPos = (audioManager.timelineInfo.elapsedBeats-1) * levelManager.distancePerBeat;
        float nextBeatPos = (audioManager.timelineInfo.elapsedBeats) * levelManager.distancePerBeat;

        float playerPosition = player.transform.position.z;

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
    

    private (string, int) RatingHelper(double distance) 
    {   
        //Normalize distance
        distance = distance/levelManager.distancePerBeat;
        string feedback;
        int score;
        switch (distance) 
        {
            case <= 0.1:
                //Add bonuspoints for streaks?
                feedback = "Perfect";
                score = 1000;
                //currentScore += 1000;
                break;
            case <= 0.2:
                feedback = "Great";
                score = 800;
                //currentScore += 800;
                break;
            case <= 0.3:
                feedback = "Good";
                score = 500;
                //currentScore += 500;
                break;
            case <= 0.4:
                feedback = "OK";
                score = 0;
                //currentScore += 0;
                break;
            case <= 0.5:
                feedback = "Bad";
                score = -500;
                //currentScore -= 500;
                break;
            default:
                feedback = "You big Noob";
                score = -1000;
                //currentScore -= 1000;
                break;
        }

        return (feedback, score);
    }


}
