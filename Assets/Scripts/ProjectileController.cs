using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public bool delivery = false;

    public int happyRadius = 20;

    public int sadRadius = 20;

    public int dizzyRadius = 2;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("We encountered a collisions:");
        //Debug.Log(collision.gameObject);
        if (delivery)
        {
            //Here consider projectiles for delivery
            if (collision.gameObject.CompareTag("Delivery"))
            {
                Debug.Log("Delivery collision detected");
                //Sound Effect?
                makeSurroundingNPCHappy();
                // Destroy the projectile
                Destroy(gameObject);
            }
            //Here consider projectiles for delivery
            if (collision.gameObject.CompareTag("BadDeliveryGlass"))
            {
                Debug.Log("BadDeliveryGlass collision detected");

                gameObject.layer = 0;
                audioManager.playBreakingGlass(Camera.main.transform.position + 0.5f*(transform.position - Camera.main.transform.position));
                //Sound Effect?
                makeSurroundingNPCSad();
                // Destroy the projectile
                Destroy(gameObject);
            }
            //Here consider projectiles for delivery
            if (collision.gameObject.CompareTag("BadDeliveryTree"))
            {
                Debug.Log("BadDeliveryTree collision detected");
                audioManager.playTreeRattle(Camera.main.transform.position + 0.85f*(transform.position - Camera.main.transform.position));
                //Sound Effect?
                makeSurroundingNPCSad();
                // Destroy the projectile
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("BadDeliveryCar"))
            {
                Debug.Log("BadDeliveryCar collision detected");
                audioManager.playCarCrash(Camera.main.transform.position + 0.75f*(transform.position - Camera.main.transform.position));
                //Sound Effect?
                makeSurroundingNPCSad();
                // Destroy the projectile
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("BadDeliveryNPC"))
            {
                Debug.Log("BadDeliveryNPC collision detected");
                audioManager.playHit(Camera.main.transform.position + 0.5f*(transform.position - Camera.main.transform.position));
                //Sound Effect?
                makeSurroundingNPCDizzy();
                // Destroy the projectile
                Destroy(gameObject);
            }

        }
        else
        {
            //Here consider the cases where the projectile is thrown by an autoattack
            if (collision.gameObject.CompareTag("Glass"))
            {
                Debug.Log("Glass collision detected");
                //Sound Effect
                audioManager.playBreakingGlass(transform.position);

                // Destroy the projectile
                Destroy(gameObject);
            }

            if (collision.gameObject.CompareTag("Car"))
            {
                Debug.Log("Car collision detected");
                //Sound Effect
                audioManager.playCarCrash(transform.position);

                // Destroy the projectile
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("Tree"))
            {
                Debug.Log("Tree collision detected");
                //Sound Effect
                audioManager.playTreeRattle(transform.position);

                // Destroy the projectile
                Destroy(gameObject);
            }

            if (collision.gameObject.CompareTag("NPC"))
            {
                Debug.Log("NPC collision detected");
                //Sound Effect
                audioManager.playHit(Camera.main.transform.position + 0.5f*(transform.position - Camera.main.transform.position));
                GameObject hitNPC = collision.gameObject.transform.GetChild(0).gameObject;
                NPCRandomizer npcRandomizer = hitNPC.GetComponent<NPCRandomizer>();
                if(npcRandomizer != null)
                {
                    npcRandomizer.makeDizzy();
                }
                // Destroy the projectile
                Destroy(gameObject);
            }

            if (collision.gameObject.CompareTag("Star"))
            {
                Debug.Log("Star collision detected");
                //Sound Effect
                GameObject hitStar = collision.gameObject;//.transform.GetChild(0).gameObject;
                StarController starController = hitStar.GetComponent<StarController>();
                if(starController != null)
                {
                    if(!starController.isCollected()){
                        starController.makeCollected();
                        audioManager.playStar(Camera.main.transform.position + 0.2f*(transform.position - Camera.main.transform.position));
                        Debug.Log("Coin Collected");
                    }
                    else
                    {
                        // Already collected: play basic hit sound but dont count as a collected star
                        audioManager.playHit(Camera.main.transform.position + 0.5f*(transform.position - Camera.main.transform.position));
                    }
                }
                // Destroy the projectile
                Destroy(gameObject);
            }
        }
        
    }

    void makeSurroundingNPCHappy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, happyRadius);
        foreach (Collider collider in colliders)
        {
            // Check if the collider's GameObject has the specified tag
            if (collider.CompareTag("NPC"))
            {
                // 
                GameObject foundObject = collider.gameObject.transform.GetChild(0).gameObject;
                NPCRandomizer npcRandomizer = foundObject.GetComponent<NPCRandomizer>();
                if(npcRandomizer != null)
                {
                    npcRandomizer.makeHappy();
                }
            }
        }
    }


    void makeSurroundingNPCSad()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sadRadius);
        foreach (Collider collider in colliders)
        {
            // Check if the collider's GameObject has the specified tag
            if (collider.CompareTag("NPC"))
            {
                // 
                GameObject foundObject = collider.gameObject.transform.GetChild(0).gameObject;
                NPCRandomizer npcRandomizer = foundObject.GetComponent<NPCRandomizer>();
                if(npcRandomizer != null)
                {
                    npcRandomizer.makeSad();
                }
            }
        }
    }

    void makeSurroundingNPCDizzy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, dizzyRadius);
        foreach (Collider collider in colliders)
        {
            // Check if the collider's GameObject has the specified tag
            if (collider.CompareTag("NPC"))
            {
                // 
                GameObject foundObject = collider.gameObject.transform.GetChild(0).gameObject;
                NPCRandomizer npcRandomizer = foundObject.GetComponent<NPCRandomizer>();
                if(npcRandomizer != null)
                {
                    npcRandomizer.makeDizzy();
                }
            }
        }
    }

}
