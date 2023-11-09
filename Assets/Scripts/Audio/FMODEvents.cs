using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("PlayingSong")]
    [field: SerializeField] public EventReference playingSong { get; private set; }

    // Singleton pattern
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        // Make sure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
}
