using UnityEngine;
using TMPro;

public class RainbowTextEffect : MonoBehaviour
{
    public TextMeshProUGUI rainbowText;

    public float hueSpeed = 50f; // Speed of color change
    public float colorLerpSpeed = 2f; // Speed of color transition

    private float currentHue = 0f;

    void Update()
    {
        // Update the current hue based on time
        currentHue = (currentHue + Time.deltaTime * hueSpeed) % 360;

        // Convert the hue to RGB color
        Color targetColor = Color.HSVToRGB(currentHue / 360f, 1f, 1f);

        // Smoothly lerp the color
        rainbowText.color = Color.Lerp(rainbowText.color, targetColor, Time.deltaTime * colorLerpSpeed);
    }
}
