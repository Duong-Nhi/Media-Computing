using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=0;
    public AudioManager audioManager;
    public LevelManager levelManager;

    private float syncError = 0;
    private float currentPosition = 0;
    private float correctPosition = 0;

    private float currBeat = -1;

    void Awake()
    {
        speed = 0;
        //Subscribe events
        AudioManager.beatUpdated += onBeat;
    }
    void Start()
    {
        Debug.Log("My speed is " + speed.ToString());
        speed = 0;
        
        Invoke("setBeatZero", 0.5f);
    }

    void setBeatZero()
    {
        currBeat = 0;
    }
    
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        AudioManager.beatUpdated -= onBeat;
        speed = 0;
    }

     void OnGUI()
    {   
        //GUILayout.BeginArea(new Rect(500, 0, 475, 900));
        //GUILayout.Box(String.Format("SyncError = {0}, current = {1}, correct = {2}, beats = {3}", syncError, currentPosition, correctPosition, audioManager.timelineInfo.elapsedBeats));
        //GUILayout.EndArea();
    }

    private void onBeat()
    {
        currBeat += 1;
        Debug.Log("Beat: " + currBeat.ToString());
        //checkSync()  Not needed anymore. We are very accurate
        if (currBeat >= 1 && currBeat< audioManager.nBeats-1) {
            startMoving();
        }
        if (currBeat >= audioManager.nBeats-1)
        {
            speed = 0;
        }

    }

    private void startMoving()
    {
        Debug.Log("Start moving was called");
        speed = ((audioManager.bpm * levelManager.distancePerBeat)/60);
    }

    // For debug purposes
    private void checkSync()
    {
        correctPosition = (audioManager.timelineInfo.elapsedBeats-1) * levelManager.distancePerBeat;

        currentPosition = transform.position.z;
        syncError = currentPosition - correctPosition;

        /* Should not be used -> does not yield better results and the current sync error is likely only due to the fact that there is a delta between the beat and the next frame
        //Adjust translation so it matches expected position -> Test if actually reduces sync error
        if(autoAdjustDesync == true && AudioManager.lastBar % adjustBarDelta == 0)
        {
            transform.position = new Vector3 (0,1, correctPosition);
        }
        */
    }
}
