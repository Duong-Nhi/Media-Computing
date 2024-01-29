using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public LevelObject[] lvlObj;
    public int UnlockedLvl;
    public int currentLevel;

    public void OnClickLevel(int lvlNum)
    {
        currentLevel = lvlNum;
        PlayerPrefs.SetInt("lvlNum", lvlNum);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene");
    }

    public void UnlockNextLevel()
    {
        if (UnlockedLvl < lvlObj.Length - 1)
        {
            UnlockedLvl++;
            UpdateLevelButtons();
        }
    }

    void UpdateLevelButtons()
    {
        for (int i = 0; i < lvlObj.Length; i++)
        {
            if (UnlockedLvl >= i)
            {
                lvlObj[i].lvlButton.interactable = true;
            }
            else
            {
                lvlObj[i].lvlButton.interactable = false;
            }
        }
    }

    void Start()
    {
        // Set UnlockedLvl to 0 so only the first level is initially unlocked
        UnlockedLvl = 0;

        // Update level buttons based on UnlockedLvl
        UpdateLevelButtons();
    }
}
