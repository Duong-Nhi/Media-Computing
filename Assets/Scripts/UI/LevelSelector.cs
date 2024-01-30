using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public LevelObject[] lvlObj;
    public Sprite[] starSprites;
    public int UnlockedLvl;
    public int currentLevel;


    void Start()
    {
        // Set UnlockedLvl to 0 so only the first level is initially unlocked
        UnlockedLvl = 0;
        UnlockedLvl = PlayerPrefs.GetInt("UnlockedLevels", 0);
        // Update level buttons based on UnlockedLvl
        UpdateLevelButtons();

        for (int i = 0; i < PlayerPrefs.GetInt("stars"); i++)
        {
            lvlObj[0].stars[i].sprite = starSprites[0];
        }
        for (int i = 0; i < PlayerPrefs.GetInt("stars1"); i++)
        {
            lvlObj[1].stars[i].sprite = starSprites[0];
        }
        for (int i = 0; i < PlayerPrefs.GetInt("stars2"); i++)
        {
            lvlObj[2].stars[i].sprite = starSprites[0];
        }

      


        for (int i = 0; i < PlayerPrefs.GetInt("trophy"); i++)
        {
            if(i < 3){
            lvlObj[0].trophy[i].sprite = starSprites[0];
            
            }else{
            lvlObj[0].trophy[i-3].sprite = starSprites[1];
            }

        }


        for (int i = 0; i < PlayerPrefs.GetInt("trophy1"); i++)
        {
            if(i < 3){
            lvlObj[1].trophy[i].sprite = starSprites[0];

            }else{
            lvlObj[1].trophy[i-3].sprite = starSprites[1];
            }

        }
        
        
        for (int i = 0; i < PlayerPrefs.GetInt("trophy2"); i++)
        {
            if(i < 3){
            lvlObj[2].trophy[i].sprite = starSprites[0];

            }else{
            lvlObj[2].trophy[i-3].sprite = starSprites[1];
            }

        }




    }

    public void OnClickLevel(int lvlNum)
    {
        currentLevel = lvlNum;
        PlayerPrefs.SetInt("lvlNum", lvlNum);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
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
}
