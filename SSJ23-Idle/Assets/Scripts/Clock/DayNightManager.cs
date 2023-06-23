using Codice.Client.Common;
using System;
using UnityEngine;

namespace LeftOut.GameJam.Clock
{
    public class DayNightManager : MonoBehaviour
    {
        public Vector3 noon;
        public GameObject dayNight;

        void Update()
        {
            // This is a 0-1 value that indicates progress through the session
            // 0 => session just started
            // 1 => session is ending
            var progress = PomodoroTimer.ProgressThroughSession;
            // This is an enum that tells you what type of session the user is currently doing
            // Right now we can just assume if they're focusing, it's daytime, and breaks happen at night
            var session = PomodoroTimer.CurrentSession;
            if (session == PomodoroSession.Focus)
            {
                UpdateDay(progress);
            }
            else
            {
                UpdateNight(progress);
            }
        }

        //rotate attached object for night/day, add offest for night

        void UpdateDay(float progress)
        {
            dayNight.transform.eulerAngles = progress * noon;
        }

        void UpdateNight(float progress)
        {
            dayNight.transform.eulerAngles = (progress * noon) + noon;
        }
    }
}
