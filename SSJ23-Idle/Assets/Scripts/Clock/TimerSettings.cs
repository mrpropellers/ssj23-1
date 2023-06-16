using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LeftOut.Runtime;

namespace LeftOut.GameJam.Clock
{
    [CreateAssetMenu]
    public class TimerSettings : ScriptableObject
    {
        [field: SerializeField]
        [field: Min(0)]
        public int NumShortBreaks { get; private set; } = 3;

        [field: SerializeField]
        [field: Min(0f)]
        public float DurationFocusMinutes { get; private set; } = 25f;

        [field: SerializeField]
        [field: Min(0f)]
        public float DurationShortBreakMinutes { get; private set; } = 5f;

        [field: SerializeField]
        [field: Min(0f)]
        public float DurationLongBreakMinutes { get; private set; } = 25f;

        public float LookUpDuration(PomodoroSession session)
        {
            switch (session)
            {
                case PomodoroSession.Focus:
                    return DurationFocusMinutes * 60f;
                case PomodoroSession.ShortBreak:
                    return DurationShortBreakMinutes * 60f;
                case PomodoroSession.LongBreak:
                    return DurationLongBreakMinutes * 60f;
                case PomodoroSession.UnInitialized:
                default:
                    Debug.LogError($"No session time defined for {session}");
                    return 0f;
            }
            
        }
    }
}
