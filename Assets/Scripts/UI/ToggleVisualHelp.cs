using UnityEngine;
using UnityEngine.UI;

public class ToggleVisualHelp : MonoBehaviour
{
    public void OnToggleValueChanged(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("GlowEnabled", 1);
            Debug.Log("GlowEnabled: " + PlayerPrefs.GetInt("GlowEnabled"));
        }
        else
        {
            PlayerPrefs.SetInt("GlowEnabled", 0);
            Debug.Log("GlowEnabled: " + PlayerPrefs.GetInt("GlowEnabled"));
        }

        // Save PlayerPrefs immediately to ensure the data is stored
        PlayerPrefs.Save();
    }
}
