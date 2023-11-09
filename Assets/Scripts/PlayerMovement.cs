using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed=0;
    public AudioManager audioManager;

    private float syncError = 0;
    private float currentPosition = 0;
    private float correctPosition = 0;

    public int beatsPerLine = 4; //Should be defined elsewhere... Other objects might also need access to this information (e.g. TileSpawner) Maybe other solution
    private bool riding = false;

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

    
    void Update()
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
        GUILayout.BeginArea(new Rect(0, 30, 375, 400));
        GUILayout.Box(String.Format("SyncError = {0}, current = {1}, correct = {2}", syncError, currentPosition, correctPosition));
        GUILayout.EndArea();
    }

    private void startMoving()
    {
        if (speed == 0)
        {
            speed = ((audioManager.bpm * 25)/60)/beatsPerLine;
        }
    }

    // For debug purposes
    private void checkSync()
    {
        float elapsed_beats = ((audioManager.timelineInfo.currentBar-1)*4+audioManager.timelineInfo.currentBeat-1);
        correctPosition = elapsed_beats * 25.0f/beatsPerLine;

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
