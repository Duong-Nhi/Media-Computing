using UnityEngine;

public class NextScreen : MonoBehaviour
{
    public GameObject[] screens;
    int currentIndex = 0;

    public void NextScreens()
    {
        screens[currentIndex].SetActive(false);

        currentIndex = (currentIndex + 1) % screens.Length;

        screens[currentIndex].SetActive(true);
    }

    public void PreviousScreens()
    {
        screens[currentIndex].SetActive(false);

        currentIndex = (currentIndex - 1 + screens.Length) % screens.Length;

        screens[currentIndex].SetActive(true);
    }
}
