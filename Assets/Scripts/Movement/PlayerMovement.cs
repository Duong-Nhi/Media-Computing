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

    void Awake()
    {
        //Subscribe events
        AudioManager.beatUpdated += startMoving;
        //AudioManager.barUpdated += checkSync;
        AudioManager.beatUpdated += checkSync;
    }
    void Start()
    {
        speed = 0;
    }

    
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        AudioManager.beatUpdated -= startMoving;
        //AudioManager.barUpdated -= checkSync;
        AudioManager.beatUpdated -= checkSync;
    }

     void OnGUI()
    {   
        GUILayout.BeginArea(new Rect(500, 0, 475, 900));
        GUILayout.Box(String.Format("SyncError = {0}, current = {1}, correct = {2}, beats = {3}", syncError, currentPosition, correctPosition, audioManager.timelineInfo.elapsedBeats));
        GUILayout.EndArea();
    }

    private void startMoving()
    {
        if (speed == 0)
        {
            speed = ((audioManager.bpm * levelManager.distancePerBeat)/60);
        }
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
