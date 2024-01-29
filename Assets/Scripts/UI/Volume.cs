using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] Slider effectsVolumeSlider;
    [SerializeField] Slider soundVolumeSlider;
    public AudioSource audioSource;

    void Start()
    {
        // Try to get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Check if AudioSource is present
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found. Please attach an AudioSource to the same GameObject as the Volume script.");
            return;
        }

        audioSource.volume = PlayerPrefs.GetFloat("SoundVolume", 1f);
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
        audioSource.volume = soundVolumeSlider.value;
        PlayerPrefs.SetFloat("SoundVolume", soundVolumeSlider.value);
    }
}
