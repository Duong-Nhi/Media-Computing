using UnityEngine;
using UnityEngine.UI;

public class ToggleVisualHelp : MonoBehaviour
{
    public void OnToggleValueChanged(bool isOn)
    {
        Debug.Log("GlowEnabled:");
        if (isOn)
        {
            // Set GlowEnabled to 1 in PlayerPrefs
            Debug.Log("GlowEnabled: " + PlayerPrefs.GetInt("GlowEnabled"));
            PlayerPrefs.SetInt("GlowEnabled", 1);
        }
        else
        {

            // Set GlowEnabled to 0 in PlayerPrefs
            Debug.Log("GlowEnabled: " + PlayerPrefs.GetInt("GlowEnabled"));
            PlayerPrefs.SetInt("GlowEnabled", 0);
        }

        // Save PlayerPrefs immediately to ensure the data is stored
        PlayerPrefs.Save();
    }
}