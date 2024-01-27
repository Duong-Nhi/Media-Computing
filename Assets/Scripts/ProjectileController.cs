using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public AudioManager audioManager;
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Glass"))
        {
            Debug.Log("Glass collision detected");
            // Sound Effect?
            audioManager.playBreakingGlass(transform.position);

            // Destroy the projectile
            Destroy(gameObject);
        }
        
        
    }
}
