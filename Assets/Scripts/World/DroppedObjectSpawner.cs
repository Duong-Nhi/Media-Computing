using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedObjectSpawner : MonoBehaviour
{
    public RateUserInput rateUserInput;
    public PlayerMovement player;

    public GameObject package;

    public GameObject letter;
    void Awake()
    {
        RateUserInput.dropLetterLeft += onDropLetterLeft;
        RateUserInput.dropPackageLeft += onDropPackageLeft;
        RateUserInput.dropLetterRight += onDropLetterRight;
        RateUserInput.dropPackageRight += onDropPackageRight;
    }

    void OnDestroy()
    {
        RateUserInput.dropLetterLeft -= onDropLetterLeft;
        RateUserInput.dropPackageLeft -= onDropPackageLeft;
        RateUserInput.dropLetterRight -= onDropLetterRight;
        RateUserInput.dropPackageRight -= onDropPackageRight;
    }
    private void onDropLetterLeft()
    {
        Instantiate(letter, player.transform.position - new Vector3(2f,1f,-1f), Quaternion.Euler(0f, 0f, 0f));
    }

    private void onDropPackageLeft()
    {
        Instantiate(package, player.transform.position - new Vector3(2f,1f,-1f), Quaternion.Euler(0f, 0f, 0f));
    }

    private void onDropPackageRight()
    {
        Instantiate(package, player.transform.position - new Vector3(-2f,1f,-1f), Quaternion.Euler(0f, 0f, 0f));
    }

    private void onDropLetterRight()
    {
        Instantiate(letter, player.transform.position - new Vector3(-2f,1f,-1f), Quaternion.Euler(0f, 0f, 0f));
    }
}
