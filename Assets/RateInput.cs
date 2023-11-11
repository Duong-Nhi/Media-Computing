using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateInput : MonoBehaviour
{
    private float playerPosition;
    public GameObject player;
    public GameObject tiles;
    private float upcomingEvent;
    private float lastEvent;
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
        // These don't have to be updated each frame necessarily -> adjust later perhaps
        if (playerPosition > upcomingEvent) { 
            upcomingEvent = tiles.transform.GetChild(1).gameObject.transform.position.z;
            lastEvent = tiles.transform.GetChild(0).gameObject.transform.position.z;
        }
        //Debug.Log("Next:" + upcomingEvent);
        //Debug.Log("Player:" + playerPosition);
        //Debug.Log("Last:" + lastEvent);
        if (Input.GetKeyDown("space")) 
        {
            OnKeyPress();
            Debug.Log("space key was pressed");
        }
    }

    // TODO Adjust this later with input parameter package/letter and left/right
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
        string rating;
        switch (distance) 
        {
            case <= 1.0:
                rating = "Perfect";
                break;
            case <= 2.5:
                rating = "Great";
                break;
            case <= 4.0:
                rating = "Good";
                break;
            case <= 5.5:
                rating = "OK";
                break;
            case <= 7.0:
                rating = "Bad";
                break;
            default:
                rating = "You big Noob";
                break;
        }

        return rating;
    }



}
