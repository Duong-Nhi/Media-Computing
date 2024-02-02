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

    private float currBeat = 0;

    void Awake()
    {
        //Subscribe events
        AudioManager.beatUpdated += onBeat;
        speed = 0;
    }
    
    // Move player in constant speed
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        //Unsubscribe events
        AudioManager.beatUpdated -= onBeat;
    }

    //Displays current beat, current position and correct position for debug purposes
    void OnGUI()
    {   
        //GUILayout.BeginArea(new Rect(500, 0, 475, 900));
        //GUILayout.Box(String.Format("SyncError = {0}, current = {1}, correct = {2}, beats = {3}", syncError, currentPosition, correctPosition, audioManager.timelineInfo.elapsedBeats));
        //GUILayout.EndArea();
    }

    //Start moving on first beat and stop on last beat
    private void onBeat()
    {

        currBeat += 1;
        if (currBeat >= 1 && currBeat< audioManager.nBeats-1) {
            startMoving();
        }
        if (currBeat >= audioManager.nBeats-1)
        {
            speed = 0;
            // Disable all scripts on the current and child GameObjects
            //DisableScriptsOnObject(gameObject);
            //DisableScriptsOnChildren(transform);
        }

    }

    // Sets the player to a speed such that the player is at the correct/expected position every beat
    private void startMoving()
    {
        speed = ((audioManager.bpm * levelManager.distancePerBeat)/60);
    }

    // For debug purposes; Not needed anymore, we are very accurate
    private void checkSync()
    {
        correctPosition = (audioManager.timelineInfo.elapsedBeats-1) * levelManager.distancePerBeat;

        currentPosition = transform.position.z;
        syncError = currentPosition - correctPosition;
    }

    void DisableScriptsOnObject(GameObject obj)
    {
        MonoBehaviour[] scripts = obj.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
        }
    }

    void DisableScriptsOnChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Disable scripts on the child
            DisableScriptsOnObject(child.gameObject);

            // Recursively disable scripts on grandchildren
            DisableScriptsOnChildren(child);
        }
    }
}
