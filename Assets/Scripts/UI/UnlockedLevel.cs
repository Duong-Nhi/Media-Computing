using UnityEngine;
using UnityEngine.UI;

public class UnlockLevels : MonoBehaviour
{
    public Button[] levelButtons;
    private int highestUnlockedLevel = 1;

    void Start()
    {
        highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);
        UnlockedLevels();
    }

    void UnlockedLevels()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = (i + 1 <= highestUnlockedLevel);
        }
    }

    public void UnlockNextLevel()
    {
        highestUnlockedLevel++;

        PlayerPrefs.SetInt("HighestUnlockedLevel", highestUnlockedLevel);
        PlayerPrefs.Save();

        UnlockedLevels();
    }
}
