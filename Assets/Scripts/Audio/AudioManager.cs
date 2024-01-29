using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using FMODUnity;

using FMOD.Studio;
public class AudioManager : MonoBehaviour
{   
    // Song information
    public float bpm;           //think if i can replace with a scriptable object to store the data... but singleton is fine for audio manager
    public float nBeats;    //Maybe removed, not used yet



    // Struct for storing timeline information (using FMOD Callbacks)
    [StructLayout(LayoutKind.Sequential)] 
    public class TimelineInfo
    {
        public int currentBeat = 0;
        public int currentBar = 0;
        public int elapsedBeats = 0;
        public FMOD.StringWrapper currentMarker = new FMOD.StringWrapper();
    }

    public TimelineInfo timelineInfo  = null;
    private GCHandle timelineHandle;
    private EVENT_CALLBACK beatCallback;

    // Track Beat and Marker -> Event on beat and marker update
    public static int lastBeat = 0;
    public static int lastBar = 0;
    public static string lastMarker = null;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdated;

    public delegate void BarEventDelegate();
    public static event BarEventDelegate barUpdated;

    public delegate void MarkerEventDelegate();
    public static event MarkerEventDelegate markerUpdated;


    // Event Instances
    private List<EventInstance> eventInstances;
    private EventInstance playingSongEventInstance;


    //Singleton pattern
    public static AudioManager instance { get; private set;}

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
        
        eventInstances = new List<EventInstance>();
        timelineInfo = new TimelineInfo();
        beatCallback = new EVENT_CALLBACK(BeatEventCallback);
        playingSongEventInstance = CreateInstance(FMODEvents.instance.playingSong);

        // Beat and Marker Callbacks
        // pin in memory to prevent it from being garbage collected (actually needed in our case? - We should have a reference to that object in unity while native code (FMOD) accesses the object)
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);

        // pass object pointer through userdata of instance
        playingSongEventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        // set callback
        playingSongEventInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        
        // get bpm and start by accessing parameters of the instance
        playingSongEventInstance.getParameterByName("bpm", out bpm);
        playingSongEventInstance.getParameterByName("nBeats", out nBeats);
    }


    private void Start()
    {   

        /*timelineInfo = new TimelineInfo();
        beatCallback = new EVENT_CALLBACK(BeatEventCallback);
        playingSongEventInstance = CreateInstance(FMODEvents.instance.playingSong);

        // Beat and Marker Callbacks
        // pin in memory to prevent it from being garbage collected (actually needed in our case? - We should have a reference to that object in unity while native code (FMOD) accesses the object)
        timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);

        // pass object pointer through userdata of instance
        playingSongEventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));

        // set callback
        playingSongEventInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

        
        // get bpm and start by accessing parameters of the instance
        playingSongEventInstance.getParameterByName("bpm", out bpm);
        playingSongEventInstance.getParameterByName("nBeats", out nBeats);*/

        // start song
        playingSongEventInstance.start();
    }


    private void Update()
    {   
        if(lastBeat != timelineInfo.currentBeat)
        {
            lastBeat = timelineInfo.currentBeat;
            if(beatUpdated != null)
            {
                beatUpdated();
            }
        }
        if(lastBar != timelineInfo.currentBar)
        {
            lastBar = timelineInfo.currentBar;
            if(barUpdated != null)
            {
                barUpdated();
            }
        }
        if(lastMarker != timelineInfo.currentMarker)
        {
            lastMarker = timelineInfo.currentMarker;
            if(markerUpdated != null)
            {
                markerUpdated();
            }
        }
    }


    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {   
        playingSongEventInstance.setUserData(IntPtr.Zero);
        foreach (EventInstance eventInstance in eventInstances)
        {
            
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
            
        }
        //Remove GC handle so memory can free memory
        timelineHandle.Free();
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    void OnGUI()
    {
        GUILayout.Box(String.Format("Current Bar = {0}, Current Beat = {1}, Last Marker = {2}", timelineInfo.currentBar, timelineInfo.currentBeat, (string)timelineInfo.currentMarker));
    }


    public void playBreakingGlass(Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.glassBreak, worldPos);
    }

    public void playTreeRattle(Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.treeRattle, worldPos);
    }

    public void playCarCrash(Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.carCrash, worldPos);
    }

    public void playHit(Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.hit, worldPos);
    }

    public void playStar(Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(FMODEvents.instance.starCollect, worldPos);
    }

    // FMOD Callbacks (Source: https://www.fmod.com/docs/2.00/unity/examples-timeline-callbacks.html)
    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {

        EventInstance instance = new EventInstance(instancePtr);
        // Retrieve the user data
        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            // Get the object to store beat and marker details
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                // Beat Callback
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                        timelineInfo.currentBar = parameter.bar;
                        timelineInfo.elapsedBeats += 1;
                        //parameter.bar also possible -> maybe becomes interesting for later features
                    }
                    break;
                // Marker Callback
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.currentMarker = parameter.name;
                    }
                    break;
            }

        }
        return FMOD.RESULT.OK;
    }

}
