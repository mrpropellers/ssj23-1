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

        public float LookUpDuration(SessionType sessionType)
        {
            switch (sessionType)
            {
                case SessionType.Focus:
                    return DurationFocusMinutes * 60f;
                case SessionType.ShortBreak:
                    return DurationShortBreakMinutes * 60f;
                case SessionType.LongBreak:
                    return DurationLongBreakMinutes * 60f;
                case SessionType.UnInitialized:
                default:
                    Debug.LogError($"No session time defined for {sessionType}");
                    return 0f;
            }
            
        }
    }
}
