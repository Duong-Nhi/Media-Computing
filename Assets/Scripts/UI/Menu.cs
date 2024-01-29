using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public GameObject buttonContainer;
    public GameObject buttonContainerSetting;
    public GameObject Menu;
    public GameObject LevelSelector;
    public TextMeshProUGUI text;

 private void Start()
    {
        SetInitialUIState();
    }

 public void SetInitialUIState()
    {
        buttonContainer.SetActive(true);
        Menu.SetActive(true);
        buttonContainerSetting.SetActive(false);
        LevelSelector.SetActive(false);
        text.text = "Special Delivery";
    }
    
public void Play()
{
    Menu.SetActive(!Menu.activeSelf);
    LevelSelector.SetActive(!LevelSelector.activeSelf);
    text.text = "Level Selector";
}


    public void Setting()
    {
        buttonContainer.SetActive(!buttonContainer.activeSelf);
        buttonContainerSetting.SetActive(!buttonContainerSetting.activeSelf);
        text.text = "Settings";
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
