using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Level : MonoBehaviour
{

    public AudioManager audioManager;
    (int, int)[] levelSequence;

    private void Start()
    {
        Debug.Log(audioManager.bpm);
        Debug.Log(audioManager.nBeats);

        levelSequence = new (int,int)[] {(0,0),(1,0),(0,0),(0,2),(0,0),(1,1),(0,0),(2,2),(0,2),(1,1),(0,1),(2,2),(0,0),(0,0),(3,3)};
    }

}
