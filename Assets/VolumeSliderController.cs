using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    [Header("References")]
    public Slider volumeSlider;
    public AudioMixer audioMixer;

    [Header("Mixer Parameter Name")]
    public string volumeParameter = "MasterVolume";

    private void Start()
    {
        // Load saved volume or set default
        float savedVolume = PlayerPrefs.GetFloat(volumeParameter, 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Add listener
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Volume must be converted to logarithmic (decibel) scale
        float dB = Mathf.Log10(volume) * 20f;
        audioMixer.SetFloat(volumeParameter, dB);

        // Save preference
        PlayerPrefs.SetFloat(volumeParameter, volume);
    }
}
