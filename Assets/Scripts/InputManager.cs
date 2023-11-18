using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Input Events
    public delegate void MailLeftEventDelegate();
    public static event MailLeftEventDelegate onMailLeft;

    public delegate void MailRightEventDelegate();
    public static event MailRightEventDelegate onMailRight;

    public delegate void PackageLeftEventDelegate();
    public static event PackageLeftEventDelegate onPackageLeft;

    public delegate void PackageRightEventDelegate();
    public static event PackageRightEventDelegate onPackageRight;

    //Singleton pattern
    public static InputManager instance { get; private set;}
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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q")) 
        {
            onMailLeft();
        }
        if (Input.GetKeyDown("e")) 
        {
            onMailRight();
        }
        if (Input.GetKeyDown("left")) 
        {
            onPackageLeft();
        }
        if (Input.GetKeyDown("right")) 
        {
            onPackageRight();
        }
    }


}
