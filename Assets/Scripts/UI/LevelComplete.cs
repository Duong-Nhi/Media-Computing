using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    int unlockedlvl = 0;

    void Awake()
    {
        PlayerPrefs.GetInt("UnlockedLevels", 1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LevelCompleted()
    {
        if (PlayerPrefs.GetInt("lvlNum", 1) == PlayerPrefs.GetInt("UnlockedLevels", 1))
        {
            unlockedlvl++;
            PlayerPrefs.SetInt("UnlockedLevels", unlockedlvl);
        }


        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
