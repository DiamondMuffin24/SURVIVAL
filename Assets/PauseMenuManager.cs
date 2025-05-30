using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuUI;
    public GameObject settingsPanelUI;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    public string volumeParameter = "MasterVolume";

    private bool isPaused = false;

    void Start()
    {
        // Initialize slider and volume
        float savedVolume = PlayerPrefs.GetFloat(volumeParameter, 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // Add listener to slider
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        settingsPanelUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_Menu"); // Use your actual scene name
    }

    public void OpenSettings()
    {
        settingsPanelUI.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanelUI.SetActive(false);
    }

    public void SetVolume(float volume)
    {
        float dB = Mathf.Log10(volume) * 20f;
        audioMixer.SetFloat(volumeParameter, dB);
        PlayerPrefs.SetFloat(volumeParameter, volume);
    }
}
