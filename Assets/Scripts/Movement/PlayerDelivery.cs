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
        //SubscribeEvents
        AudioManager.beatUpdated += onBeat;
        RateUserInput.deliverLetterLeft += onDeliverLetterLeft;
        RateUserInput.deliverLetterRight += onDeliverLetterRight;
        RateUserInput.deliverPackageLeft += onDeliverPackageLeft;
        RateUserInput.deliverPackageRight += onDeliverPackageRight;

        RateUserInput.deliverLetterLeftBad += onDeliverLetterLeftBad;
        RateUserInput.deliverLetterRightBad += onDeliverLetterRightBad;
        RateUserInput.deliverPackageLeftBad += onDeliverPackageLeftBad;
        RateUserInput.deliverPackageRightBad += onDeliverPackageRightBad;
    }
    void OnDestroy()
    {
        //UnsubscribeEvents
        AudioManager.beatUpdated -= onBeat;
        RateUserInput.deliverLetterLeft -= onDeliverLetterLeft;
        RateUserInput.deliverLetterRight -= onDeliverLetterRight;
        RateUserInput.deliverPackageLeft -= onDeliverPackageLeft;
        RateUserInput.deliverPackageRight -= onDeliverPackageRight;

        RateUserInput.deliverLetterLeftBad -= onDeliverLetterLeftBad;
        RateUserInput.deliverLetterRightBad -= onDeliverLetterRightBad;
        RateUserInput.deliverPackageLeftBad -= onDeliverPackageLeftBad;
        RateUserInput.deliverPackageRightBad -= onDeliverPackageRightBad;
    }

    // keep track of the beats
    void onBeat()
    {
        currBeat += 1;
    }

    // Find the target tile and find the gameobject delivery marking the delivery destination; currently doors, possibly mailboxes in the future
    void onDeliverLetterLeft()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
               
                GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }

    // Find the target tile and find the gameobject delivery marking the delivery destination; currently doors, possibly mailboxes in the future
    void onDeliverLetterRight()
    {

        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }
    // Find the target tile and find the gameobject delivery marking the delivery destination; currently doors, possibly mailboxes in the future
    void onDeliverPackageLeft()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjLeft.transform.Find("Delivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }

    // Find the target tile and find the gameobject delivery marking the delivery destination; currently doors, possibly mailboxes in the future
    void onDeliverPackageRight()
    {
        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("Delivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }

    // Same but bad events (wrong inputs/ low scoring)
    // Find the target tile and find the gameobject Baddelivery marking the badDelivery destination; currently windows trees, cars and NPCs
    void onDeliverLetterLeftBad()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
               
                GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }
    // Find the target tile and find the gameobject Baddelivery marking the badDelivery destination; currently windows trees, cars and NPCs
    void onDeliverLetterRightBad()
    {

        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
            deliverObject(letter, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
                deliverObject(letter, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }

    }
    // Find the target tile and find the gameobject Baddelivery marking the badDelivery destination; currently windows trees, cars and NPCs
    void onDeliverPackageLeftBad()
    {
        GameObject currObjLeft = tileSpawner.getObjectLeft(currBeat+1);
        if(currObjLeft.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
        }
        else{
            currObjLeft = tileSpawner.getObjectLeft(currBeat);
            if(currObjLeft.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjLeft.transform.Find("BadDelivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position, new Vector3(-1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }
    // Find the target tile and find the gameobject Baddelivery marking the badDelivery destination; currently windows trees, cars and NPCs
    void onDeliverPackageRightBad()
    {
        GameObject currObjRight = tileSpawner.getObjectRight(currBeat+1);
        if(currObjRight.CompareTag("House"))
        {
            GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
            deliverObject(package, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
        }
        else{
            currObjRight = tileSpawner.getObjectRight(currBeat);
            if(currObjRight.CompareTag("House"))
            {
                GameObject deliveryLoc = currObjRight.transform.Find("BadDelivery").gameObject;
                deliverObject(package, deliveryLoc.transform.position, new Vector3(1f, 0f, 0f));
            }
            else{
                Debug.Log("Incorrect Delivery");
            }
        }
    }





    // Spawn a projectile and set it on its way to the delivery position
    void deliverObject(GameObject projectile, Vector3 deliveryPosition, Vector3 offset)
    {
        if(projectile != null)
        {
            
            //spawns projectile at player position; slightly elevated
            GameObject projectileObj = Instantiate(projectile, transform.position + new Vector3(0f, 0.5f, 0f) + offset, Quaternion.identity);
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
                Vector3 velocity = (deliveryPosition - (transform.position + new Vector3(0f, 0.5f, 0f) + offset)).normalized;
                // Set constant speed in the x-direction and same velocity in the z-direction as the player
                projectileRb.velocity = velocity * deliverySpeed;

                projectileRb.transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
            }


        }
    }
}
