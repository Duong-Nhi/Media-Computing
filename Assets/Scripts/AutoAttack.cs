using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    public RateUserInput rateUserInput;

    public GameObject letter;
    public GameObject package;

    public PlayerMovement playerMovement;

    public float velocity=8;

    void Awake()
    {
        RateUserInput.autoAttackLeftLetter += onAutoAttackLeftLetter;
        RateUserInput.autoAttackLeftPackage += onAutoAttackLeftPackage;
        RateUserInput.autoAttackRightLetter += onAutoAttackRightLetter;
        RateUserInput.autoAttackRightPackage += onAutoAttackRightPackage;
        
    }

    void onAutoAttackLeftLetter()
    {
        spawnProjectile(letter, velocity);
    }

    void onAutoAttackLeftPackage()
    {
        spawnProjectile(package, velocity);
    }

    void onAutoAttackRightLetter()
    {
        spawnProjectile(letter, -velocity);
    }

    void onAutoAttackRightPackage()
    {
        spawnProjectile(package, -velocity);
    }

    void spawnProjectile(GameObject projectile, float velocity)
    {
        GameObject projectileObj = Instantiate(projectile, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        Rigidbody projectileRb = projectileObj.GetComponent<Rigidbody>();
        if (projectileRb != null)
        {
            // Set constant speed in the x-direction and same velocity in the z-direction as the player
            projectileRb.velocity = new Vector3(-velocity, 0f, playerMovement.speed*1.75f);
            projectileRb.transform.rotation = Quaternion.Euler(new Vector3(45f, 0f, 0f));
        }
    }
}


