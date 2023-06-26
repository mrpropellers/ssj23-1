using System;
using UnityEngine;
using TMPro;
using LeftOut.GameJam.Clock;

namespace LeftOut.GameJam.UserInterface
{
    public class UITimerCountdown : MonoBehaviour
    {
        public TextMeshProUGUI timerText; // Reference to the UI text component

        float currentTime => Timer.Exists ? Timer.CurrentTime : 0f;

        private void Start()
        {
            UpdateTimerText();
        }

        private void Update()
        {
            UpdateTimerText();
        }

        private void UpdateTimerText()
        {
            // Format the time into minutes and seconds
            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            // Update the UI text component with the current time
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        [Obsolete("Countdown starting/pausing is controlled by the Timer Object")]
        public void StartPauseCountdown()
        {
            Debug.LogWarning("this doesn't do anything now.", this);
        }
    }
}