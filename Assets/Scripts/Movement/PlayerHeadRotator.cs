using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerHeadRotator : MonoBehaviour
{   
    
    public AudioManager audioManager;
    public float[] rotations = new float[] {0f, -10f, 0f, 10f};
    public float[] translations = new float[] {0f, -10f, 0f, 10f};
    private int currentBeat = 0;

    void Awake()
    {
        //Subscribe events
        AudioManager.beatUpdated += onBeat;
    }

    void OnDestroy()
    {
        //Unsubscribe events
        AudioManager.beatUpdated -= onBeat;
    }

    // Wobble head -> Change sprites and apply slight rotations every beat
    void onBeat()
    {
        currentBeat++;
        if(currentBeat >= audioManager.nBeats-1){
            return;
        }
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, rotations[currentBeat%rotations.Length]));
        transform.Translate(new Vector3(0f, translations[currentBeat%translations.Length] ,0f));
    }

}
