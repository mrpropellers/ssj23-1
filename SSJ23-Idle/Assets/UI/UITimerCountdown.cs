using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UITimerCountdown : MonoBehaviour
{
    public float countdownTime = 60f; // Countdown time in seconds
    public TextMeshProUGUI timerText; // Reference to the UI text component

    float currentTime;

    bool playmode = false;

    private void Start()
    {
        currentTime = countdownTime;
        UpdateTimerText();
    }

    private void Update()
    {
        if (currentTime > 0 && playmode)
        {
            currentTime -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            playmode = false;
            currentTime = countdownTime;
        }
    }

    private void UpdateTimerText()
    {
        // Format the time into minutes and seconds
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);

        // Update the UI text component with the current time
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartPauseCountdown()
    {
        // Start the countdown by setting the current time
        // to the initial countdown time.
        if (!playmode)
        {
            currentTime = countdownTime;
            playmode = true;
        }
        else
        {
            playmode = false;
        }
    }
}
