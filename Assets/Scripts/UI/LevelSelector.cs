using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public LevelObject[] lvlObj;
    public Sprite[] starSprites;
    public Sprite[] trophySprites;
    public int UnlockedLvl;
    public int currentLevel;


    void Start()
    {   


        // Set UnlockedLvl to 0 so only the first level is initially unlocked
        UnlockedLvl = 0;



        // Unlock Levels if the previous Level you had collected at least 2 trophies

        if(PlayerPrefs.GetInt("trophy")>=2){
            UnlockedLvl = 1;
        }
        if(PlayerPrefs.GetInt("trophy1")>=2){
            UnlockedLvl = 2;
        }
        if(PlayerPrefs.GetInt("trophy2")>=2){
            UnlockedLvl = 3;
        }
        if(PlayerPrefs.GetInt("trophy3")>=2){
            UnlockedLvl = 4;
        }

        // Update level buttons based on UnlockedLvl
        UpdateLevelButtons();

        // Update Stars in Menu

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
        for (int i = 0; i < PlayerPrefs.GetInt("stars3"); i++)
        {
            lvlObj[3].stars[i].sprite = starSprites[0];
        }
        for (int i = 0; i < PlayerPrefs.GetInt("stars4"); i++)
        {
            lvlObj[4].stars[i].sprite = starSprites[0];
        }

      
        // Update Trophy in Menu (3 Golden then they turn Purple)


        for (int i = 0; i < PlayerPrefs.GetInt("trophy"); i++)
        {
            if(i < 3){
            lvlObj[0].trophy[i].sprite = trophySprites[0];
            
            }else{
            lvlObj[0].trophy[i-3].sprite = trophySprites[1];
            }

        }


        for (int i = 0; i < PlayerPrefs.GetInt("trophy1"); i++)
        {
            if(i < 3){
            lvlObj[1].trophy[i].sprite = trophySprites[0];

            }else{
            lvlObj[1].trophy[i-3].sprite = trophySprites[1];
            }

        }
        
        
        for (int i = 0; i < PlayerPrefs.GetInt("trophy2"); i++)
        {
            if(i < 3){
            lvlObj[2].trophy[i].sprite = trophySprites[0];

            }else{
            lvlObj[2].trophy[i-3].sprite = trophySprites[1];
            }
        }

        for (int i = 0; i < PlayerPrefs.GetInt("trophy3"); i++)
        {
            if(i < 3){
            lvlObj[3].trophy[i].sprite = trophySprites[0];

            }else{
            lvlObj[3].trophy[i-3].sprite = trophySprites[1];
            }
        }

        for (int i = 0; i < PlayerPrefs.GetInt("trophy4"); i++)
        {
            if(i < 3){
            lvlObj[4].trophy[i].sprite = trophySprites[0];

            }else{
            lvlObj[4].trophy[i-3].sprite = trophySprites[1];
            }
        }




    }

    //Button Click, Gives back lvlNum to choose the Level
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

    //Resets Trophy/Stars
    public void ResetValues(){
        PlayerPrefs.SetInt("stars", 0);
        PlayerPrefs.SetInt("stars1", 0);
        PlayerPrefs.SetInt("stars2", 0);
        PlayerPrefs.SetInt("stars3", 0);
        PlayerPrefs.SetInt("stars4", 0);

        PlayerPrefs.SetInt("trophy", 0);
        PlayerPrefs.SetInt("trophy1", 0);
        PlayerPrefs.SetInt("trophy2", 0);
        PlayerPrefs.SetInt("trophy3", 0);
        PlayerPrefs.SetInt("trophy4", 0);
        
        UnlockedLvl = 0;
        UpdateLevelButtons();
        lvlObj[0].stars[0].sprite = starSprites[2];
        lvlObj[0].stars[1].sprite = starSprites[2];
        lvlObj[0].stars[2].sprite = starSprites[2];
        lvlObj[1].stars[0].sprite = starSprites[2];
        lvlObj[1].stars[1].sprite = starSprites[2];
        lvlObj[1].stars[2].sprite = starSprites[2];
        lvlObj[2].stars[0].sprite = starSprites[2];
        lvlObj[2].stars[1].sprite = starSprites[2];
        lvlObj[2].stars[2].sprite = starSprites[2];
        lvlObj[3].stars[0].sprite = starSprites[2];
        lvlObj[3].stars[1].sprite = starSprites[2];
        lvlObj[3].stars[2].sprite = starSprites[2];
        lvlObj[4].stars[0].sprite = starSprites[2];
        lvlObj[4].stars[1].sprite = starSprites[2];
        lvlObj[4].stars[2].sprite = starSprites[2];


        lvlObj[0].trophy[0].sprite = trophySprites[2];
        lvlObj[0].trophy[1].sprite = trophySprites[2];
        lvlObj[0].trophy[2].sprite = trophySprites[2];
        lvlObj[1].trophy[0].sprite = trophySprites[2];
        lvlObj[1].trophy[1].sprite = trophySprites[2];
        lvlObj[1].trophy[2].sprite = trophySprites[2];
        lvlObj[2].trophy[0].sprite = trophySprites[2];
        lvlObj[2].trophy[1].sprite = trophySprites[2];
        lvlObj[2].trophy[2].sprite = trophySprites[2];
        lvlObj[3].trophy[0].sprite = trophySprites[2];
        lvlObj[3].trophy[1].sprite = trophySprites[2];
        lvlObj[3].trophy[2].sprite = trophySprites[2];
        lvlObj[4].trophy[0].sprite = trophySprites[2];
        lvlObj[4].trophy[1].sprite = trophySprites[2];
        lvlObj[4].trophy[2].sprite = trophySprites[2];

    }

    //Enables LevelButtons
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
