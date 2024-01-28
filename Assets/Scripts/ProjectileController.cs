using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public bool delivery = false;
    public AudioManager audioManager;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("We encountered a collisions:");
        Debug.Log(collision.gameObject);
        if (delivery)
        {
            //Here consider projectiles for delivery
            if (collision.gameObject.CompareTag("Delivery"))
            {
                Debug.Log("Delivery collision detected");
                //Sound Effect?

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
}
