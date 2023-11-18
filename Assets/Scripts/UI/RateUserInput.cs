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


    void Awake()
    {
        //Subscribe events
        InputManager.onMailLeft += onMailLeft;
        InputManager.onMailRight += onMailRight;
        InputManager.onPackageLeft += onPackageLeft;
        InputManager.onPackageRight += onPackageRight;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = "0 Points";
        
    }

    // Display the current score every frame -> Maybe instead of every frame, only every user input (only time it changes)
    void Update()
    {

    }


    void onMailLeft()
    {
        (float distance, int beat) = distanceToClosestBeat();
        (int task,_) = getTaskFromLevel(beat);
        if (task == 1){
            // TODO: do something with feedback. maybe move rate user input somewhere else (input manager) and make events if score changes
            lastFeedback = RatingHelper(distance); 
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
            lastFeedback = RatingHelper(distance);
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
            lastFeedback = RatingHelper(distance);
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
            lastFeedback = RatingHelper(distance); 
        }
        else
        {
            lastFeedback = "Wrong Delivery";
            currentScore -= 1000;
        }
        scoreText.text = currentScore.ToString();
    }

    // Rename? With task i mean the elements of the level i.e. (1,0)
    private (int, int) getTaskFromLevel(int beat)
    {
        // -1 because we index beats starting from 1, array from 0
        return levelManager.levelSequence[beat-1];

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
    

    private string RatingHelper(double distance) 
    {
        string feedback;
        switch (distance) 
        {
            case <= 1.0:
                //Add bonuspoints for streaks?
                feedback = "Perfect";
                currentScore += 1000;
                break;
            case <= 2.5:
                feedback = "Great";
                currentScore += 800;
                break;
            case <= 4.0:
                feedback = "Good";
                currentScore += 500;
                break;
            case <= 5.5:
                feedback = "OK";
                currentScore += 0;
                break;
            case <= 7.0:
                feedback = "Bad";
                currentScore -= 500;
                break;
            default:
                feedback = "You big Noob";
                currentScore -= 1000;
                break;
        }

        return feedback;
    }


}
