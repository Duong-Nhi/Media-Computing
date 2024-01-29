using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class LevelSelector : MonoBehaviour
{
    public static int currentLvl;

    public void onclickLevel(int lvlNum){
    currentLvl = lvlNum;
    SceneManager.LoadScene("SampleScene");
    }
}
