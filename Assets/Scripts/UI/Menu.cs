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
        buttonContainer.SetActive(true);
        Menu.SetActive(true);
        buttonContainerSetting.SetActive(false);
        LevelSelector.SetActive(false);
    }

    public void Play()
    {
        Menu.SetActive(!Menu.activeSelf);
        LevelSelector.SetActive(!LevelSelector.activeSelf);

        // Corrected variable name
        text.text = "Level Selector";
    }

    public void Setting()
    {
        buttonContainer.SetActive(!buttonContainer.activeSelf);
        buttonContainerSetting.SetActive(!buttonContainerSetting.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void Back()
    {
        buttonContainer.SetActive(!buttonContainer.activeSelf);
        buttonContainerSetting.SetActive(!buttonContainerSetting.activeSelf);
        LevelSelector.SetActive(false);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
