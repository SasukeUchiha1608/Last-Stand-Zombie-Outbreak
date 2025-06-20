using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SurvivalTimer : MonoBehaviour
{
    public float timeRemaining = 300f;
    public TMP_Text timerText;

    public GameObject winPanel;
    public GameObject hudPanel;

    private bool hasWon = false;
    private bool hasGameStarted = false;

    void Start()
    {
        // Delay the timer start until the next frame to avoid early deltaTime subtraction
        StartCoroutine(StartTimerNextFrame());
        UpdateTimerDisplay(); // ensure it shows full 5:00 at start
    }

    System.Collections.IEnumerator StartTimerNextFrame()
    {
        yield return null; // wait 1 frame
        hasGameStarted = true;
    }

    void Update()
    {
        if (!hasGameStarted || hasWon) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.unscaledDeltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            WinGame();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void WinGame()
    {
        hasWon = true;
        timeRemaining = 0f;
        UpdateTimerDisplay();

        Time.timeScale = 0f;
        if (winPanel != null) winPanel.SetActive(true);
        if (hudPanel != null) hudPanel.SetActive(false);

        Debug.Log("Player survived! Victory!");
    }
}
