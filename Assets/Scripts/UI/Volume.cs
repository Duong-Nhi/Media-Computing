using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider effectsVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;

    void Start()
    {
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 1f);
        soundVolumeSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1f);
    }

    public void OnEffectsVolumeChanged()
    {
        AudioListener.volume = effectsVolumeSlider.value;
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
    }

    public void OnSoundVolumeChanged()
    {
        AudioListener.volume = soundVolumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", soundVolumeSlider.value);
    }
}
