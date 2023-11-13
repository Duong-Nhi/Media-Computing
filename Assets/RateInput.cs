using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateInput : MonoBehaviour
{
    private float playerPosition;
    public GameObject player;
    public GameObject tiles;
    // TODO Save the whole tile here later to check for events
    private float upcomingEvent;
    private float lastEvent;
    public int rating;
    // Start is called before the first frame update
    void Start()
    {
        upcomingEvent = tiles.transform.GetChild(2).gameObject.transform.position.z;
        lastEvent = tiles.transform.GetChild(1).gameObject.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position.z;
        if (playerPosition > upcomingEvent) 
        {
            lastEvent = tiles.transform.GetChild(0).gameObject.transform.position.z;
            upcomingEvent = tiles.transform.GetChild(1).gameObject.transform.position.z;
        }
        // TODO Replace this with our 4 input keys
        if (Input.GetKeyDown("space")) 
        {
            OnKeyPress();
            //Debug.Log("space key was pressed");
        }
    }

    // TODO Adjust this later with input parameter package/letter and left/right and which key was pressed
    public void OnKeyPress() 
    {
        string rating;
        if (upcomingEvent - playerPosition > playerPosition - lastEvent)
        {
            double distance = (playerPosition - lastEvent);
            rating = RatingHelper(distance);
            Debug.Log(distance);
        }
        else if (upcomingEvent - playerPosition < playerPosition - lastEvent)
        {
            double distance = (upcomingEvent - playerPosition);
            rating = RatingHelper(distance);
            Debug.Log(distance);
        }
        else 
        {
            rating = "How?";
        }
        Debug.Log(rating);

    }

    private string RatingHelper(double distance) 
    {
        string feedback;
        switch (distance) 
        {
            case <= 1.0:
                //Add bonuspoints for streaks?
                feedback = "Perfect";
                rating += 1000;
                break;
            case <= 2.5:
                feedback = "Great";
                rating += 800;
                break;
            case <= 4.0:
                feedback = "Good";
                rating += 500;
                break;
            case <= 5.5:
                feedback = "OK";
                rating += 0;
                break;
            case <= 7.0:
                feedback = "Bad";
                rating -= 500;
                break;
            default:
                feedback = "You big Noob";
                rating -= 1000;
                break;
        }

        return feedback;
    }



}
