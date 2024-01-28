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
                audioManager.playBreakingGlass(Camera.main.transform.position + 0.3f*(transform.position - Camera.main.transform.position));
                //Sound Effect?
                makeSurroundingNPCSad();
                // Destroy the projectile
                Destroy(gameObject);
            }
            //Here consider projectiles for delivery
            if (collision.gameObject.CompareTag("BadDeliveryTree"))
            {
                Debug.Log("BadDeliveryTree collision detected");
                //Sound Effect?
                makeSurroundingNPCSad();
                // Destroy the projectile
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("BadDeliveryCar"))
            {
                Debug.Log("BadDeliveryCar collision detected");
                //Sound Effect?
                makeSurroundingNPCSad();
                // Destroy the projectile
                Destroy(gameObject);
            }
            if (collision.gameObject.CompareTag("BadDeliveryNPC"))
            {
                Debug.Log("BadDeliveryNPC collision detected");
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

            // Cars? NPC's ...
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
                Debug.Log("Found object with tag: NPC - Name: " + foundObject.name);
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
                Debug.Log("Found object with tag: NPC - Name: " + foundObject.name);
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
                Debug.Log("Found object with tag: NPC - Name: " + foundObject.name);
            }
        }
    }

}
