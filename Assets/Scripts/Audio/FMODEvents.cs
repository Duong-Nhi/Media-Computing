using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("PlayingSong")]
    [field: SerializeField] public EventReference playingSong { get; private set; }

    [field: Header("GlassBreak Sound")]
    [field: SerializeField] public EventReference glassBreak { get; private set; }

    [field: Header("TreeRattle Sound")]
    [field: SerializeField] public EventReference treeRattle { get; private set; }

    [field: Header("CarCrash Sound")]
    [field: SerializeField] public EventReference carCrash { get; private set; }

    [field: Header("Hit Sound")]
    [field: SerializeField] public EventReference hit { get; private set; }

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
