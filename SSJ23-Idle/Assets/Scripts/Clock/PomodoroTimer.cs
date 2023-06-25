using System;
using UnityEngine;
using UnityEngine.Events;

namespace LeftOut.GameJam.Clock
{
    public class PomodoroTimer : SingletonBehaviour<PomodoroTimer>
    {
        [SerializeField]
        bool m_IsPlaying;
        
        TimerSettings m_Settings;
        PomodoroSession m_CurrentSession;
        int m_NumShortBreaksTaken;
        bool m_CurrentSessionHasStarted;
        float m_TimeInSession;
        float m_CurrentSessionLength;

        // [SerializeField]
        // bool ShouldPauseBetweenSessions = true;

        [SerializeField]
        TimerSettings DefaultSettings;
        [field: SerializeField]
        public UnityEvent<PomodoroSession> SessionStarted { get; private set; }
        [field: SerializeField]
        public UnityEvent<PomodoroSession> SessionEnded { get; private set; }

        // Returns a value in range [0-1] indicating how far through the current session we are
        public static float ProgressThroughSession => Instance.GetProgressThroughSession();
        public static PomodoroSession CurrentSession => Instance.GetCurrentSession();

        public static bool Exists => Instance != null;
        public static void Play() => Instance.Play_impl();
        public static void Pause() => Instance.m_IsPlaying = false;
        
        void Start()
        {
            // TODO: Eventually this should be initialized by something else via a public method
            Initialize_impl(DefaultSettings);
        }

        void Update()
        {
            if (m_IsPlaying)
            {
                m_TimeInSession += Time.deltaTime;
                if (m_TimeInSession >= m_CurrentSessionLength)
                {
                    SessionEnded?.Invoke(m_CurrentSession);
                    MoveToNextSession();
                }
            }
        }

        void Play_impl()
        {
            m_IsPlaying = true;
            if (!m_CurrentSessionHasStarted)
            {
                m_CurrentSessionHasStarted = true;
                SessionStarted?.Invoke(m_CurrentSession);
            }
        }


        float GetProgressThroughSession()
        {
            return Mathf.Clamp01(m_TimeInSession / m_CurrentSessionLength);
        }

        PomodoroSession GetCurrentSession() => m_CurrentSession;

        void MoveToNextSession()
        {
            switch (m_CurrentSession)
            {
                case PomodoroSession.Focus:
                    if (m_NumShortBreaksTaken == m_Settings.NumShortBreaks)
                    {
                        ChangeSession(PomodoroSession.LongBreak);
                        m_NumShortBreaksTaken = 0;
                    }
                    else
                    {
                        ChangeSession(PomodoroSession.ShortBreak);
                        m_NumShortBreaksTaken++;
                    }
                    break;
                case PomodoroSession.ShortBreak:
                case PomodoroSession.LongBreak:
                    ChangeSession(PomodoroSession.Focus);
                    break;
                case PomodoroSession.UnInitialized:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void ChangeSession(PomodoroSession newSession)
        {
            Pause();
            Debug.Log($"Changing session from {m_CurrentSession} to {newSession}");
            m_CurrentSession = newSession;
            m_CurrentSessionLength = m_Settings.LookUpDuration(newSession);
            m_TimeInSession = 0f;
            m_CurrentSessionHasStarted = false;
        }
        
        void Initialize_impl(TimerSettings settings)
        {
            Debug.Log("Initializing...");
            // TODO: We should check initialization status
            //  If status is NOT Uninitialized and we're NOT in Editor, something is wrong
            m_Settings = settings;
            ChangeSession(PomodoroSession.Focus);
        }
        
        

    }
}
