using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    [Header("Resolution")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    [Header("Display")]
    public Toggle fullscreenToggle;

    void Start()
    {
        // --- Volume ---
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // --- Resolution ---
        Resolution[] allResolutions = Screen.resolutions;
        var resolutionOptions = new System.Collections.Generic.List<string>();
        var uniqueResSet = new System.Collections.Generic.HashSet<string>();
        var uniqueResList = new System.Collections.Generic.List<Resolution>();

        int currentResIndex = 0;

        for (int i = 0; i < allResolutions.Length; i++)
        {
            string resString = allResolutions[i].width + " x " + allResolutions[i].height;

            if (!uniqueResSet.Contains(resString))
            {
                uniqueResSet.Add(resString);
                uniqueResList.Add(allResolutions[i]);
                resolutionOptions.Add(resString);

                if (allResolutions[i].width == Screen.currentResolution.width &&
                    allResolutions[i].height == Screen.currentResolution.height)
                {
                    currentResIndex = uniqueResList.Count - 1;
                }
            }
        }

        resolutions = uniqueResList.ToArray();
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);

        int savedResIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResIndex);
        resolutionDropdown.value = savedResIndex;
        resolutionDropdown.RefreshShownValue();

        // --- Fullscreen (IMPORTANT ORDER!) ---
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;

        fullscreenToggle.onValueChanged.RemoveAllListeners(); // Just in case!
        fullscreenToggle.isOn = isFullscreen; // Set value BEFORE adding listener
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Set resolution AFTER fullscreen is known
        SetResolution(savedResIndex);

        // Set actual screen mode
        Screen.fullScreen = isFullscreen;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainGame"); // Change to your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void SetVolume(float volume)
    {
        // Apply and save volume
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20f);
        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    public void SetResolution(int index)
    {
        Resolution res = resolutions[index];

        // Read the most recent fullscreen setting from PlayerPrefs
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        Screen.SetResolution(res.width, res.height, isFullscreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    public void SetFullscreen(bool isFullscreen)
    {

        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();

        int index = resolutionDropdown.value;
        SetResolution(index);
    }

}
