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
        // --- Load and apply saved volume ---
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        // --- Filter and load unique resolution options (width x height) ---
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

        resolutions = uniqueResList.ToArray(); // Replace original with unique list

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(resolutionOptions);

        // Load saved resolution index (or use current if not set)
        int savedResIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResIndex);
        resolutionDropdown.value = savedResIndex;
        resolutionDropdown.RefreshShownValue();
        SetResolution(savedResIndex);

        // --- Fullscreen ---
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", Screen.fullScreen ? 1 : 0) == 1;
        fullscreenToggle.isOn = isFullscreen;
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
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}
