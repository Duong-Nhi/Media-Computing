using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDelivery : MonoBehaviour
{
    public AudioManager audioManager;
    public TileSpawner tileSpawner;

    public GameObject test;

    public GameObject letter;
    
    public GameObject package;

    public RateUserInput rateUserInput;
    public float deliverySpeed = 15;
    private int currBeat = 0;
    void Awake()
    {
        AudioManager.beatUpdated += onBeat;
        //SubscribeEvents
        RateUserInput.deliverLetterLeft += onDeliverLetterLeft;
        RateUserInput.deliverLetterRight += onDeliverLetterRight;
        RateUserInput.deliverPackageLeft += onDeliverPackageLeft;
        RateUserInput.deliverPackageRight += onDeliverPackageRight;

        RateUserInput.deliverLetterLeftBad += onDeliverLetterLeftBad;
        RateUserInput.deliverLetterRightBad += onDeliverLetterRightBad;
        RateUserInput.deliverPackageLeftBad += onDeliverPackageLeftBad;
        RateUserInput.deliverPackageRightBad += onDeliverPackageRightBad;
    }

    void onBeat()
    {
        currBeat += 1;
    }
    void onDeliverLetterLeft()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position);
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
               
                GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }

    void onDeliverLetterRight()
    {

        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position);
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }

    void onDeliverPackageLeft()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position);
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }

    void onDeliverPackageRight()
    {
        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position);
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }

    // Same but bad events

    void onDeliverLetterLeftBad()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position);
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
               
                GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }

    void onDeliverLetterRightBad()
    {

        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position);
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }

    void onDeliverPackageLeftBad()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position);
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }

    void onDeliverPackageRightBad()
    {
        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position);
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position);
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }






    void deliverObject(GameObject projectile, Vector3 deliveryPosition)
    {
        if(projectile != null)
        {
            
            //spawns projectile at player position; slightly elevated
            GameObject projectileObj = Instantiate(projectile, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
            // Set to delivery layer -> does not collide with other objects other than delivery marked 
            projectileObj.layer = 6;
            //set the projectile to delivery state (only detect colisions with delivery collision box)
            ProjectileController projectileController = projectileObj.GetComponent<ProjectileController>();
            if(projectileController != null)
            {
                projectileController.delivery = true;
            }

            //Get Rigidbody component and set velocity towards delivery point
            Rigidbody projectileRb = projectileObj.GetComponent<Rigidbody>();
            if (projectileRb != null)
            {
                Vector3 velocity = (deliveryPosition - (transform.position + new Vector3(0f, 0.5f, 0f))).normalized;
                // Set constant speed in the x-direction and same velocity in the z-direction as the player
                projectileRb.velocity = velocity * deliverySpeed;

                projectileRb.transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
            }


        }
    }
}
