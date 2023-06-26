using System;
using UnityEngine;
using UnityEngine.Events;

namespace LeftOut.GameJam.Clock
{
    public class Timer : SingletonBehaviour<Timer>
    {
        [SerializeField]
        bool m_IsPlaying;
        
        TimerSettings m_Settings;
        SessionType m_CurrentSessionType;
        int m_NumShortBreaksTaken;
        bool m_CurrentSessionHasStarted;
        float m_TimeInSession;
        float m_CurrentSessionLength;

        // [SerializeField]
        // bool ShouldPauseBetweenSessions = true;

        #if UNITY_EDITOR
        [SerializeField]
        #endif
        bool PauseBetweenSessions = true;
        [SerializeField]
        TimerSettings DefaultSettings;
        [field: SerializeField]
        public UnityEvent<SessionType> SessionStarted { get; private set; }
        [field: SerializeField]
        public UnityEvent<SessionType> SessionEnded { get; private set; }

        // Returns a value in range [0-1] indicating how far through the current session we are
        public static float ProgressThroughSession => Instance.GetProgressThroughSession();
        public static SessionType currentSessionType => Instance.GetCurrentSession();

        public static bool Exists => Instance != null;
        public static bool IsPlaying => Exists && Instance.m_IsPlaying;
        public static float CurrentTime => Instance.m_CurrentSessionLength - Instance.m_TimeInSession;
        public static void Play() => Instance.Play_impl();
        public static void Pause() => Instance.m_IsPlaying = false;

        public static void Toggle()
        {
            if (Instance.m_IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }
        
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
                Debug.Log($"Starting session: {m_CurrentSessionType}");
                SessionStarted?.Invoke(m_CurrentSessionType);
            }
        }


        float GetProgressThroughSession()
        {
            return Mathf.Clamp01(m_TimeInSession / m_CurrentSessionLength);
        }

        SessionType GetCurrentSession() => m_CurrentSessionType;

        void MoveToNextSession()
        {
            switch (m_CurrentSessionType)
            {
                case SessionType.Focus:
                    if (m_NumShortBreaksTaken == m_Settings.NumShortBreaks)
                    {
                        ChangeSession(SessionType.LongBreak);
                        m_NumShortBreaksTaken = 0;
                    }
                    else
                    {
                        ChangeSession(SessionType.ShortBreak);
                        m_NumShortBreaksTaken++;
                    }
                    break;
                case SessionType.ShortBreak:
                case SessionType.LongBreak:
                    ChangeSession(SessionType.Focus);
                    break;
                case SessionType.UnInitialized:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        void ChangeSession(SessionType newSessionType)
        {
            Debug.Log($"Ending session: {m_CurrentSessionType}");
            SessionEnded?.Invoke(m_CurrentSessionType);
            //Debug.Log($"Changing session from {m_CurrentSession} to {newSession}");
            m_CurrentSessionType = newSessionType;
            m_CurrentSessionLength = m_Settings.LookUpDuration(newSessionType);
            m_TimeInSession = 0f;
            m_CurrentSessionHasStarted = false;
            if (PauseBetweenSessions)
            {
                Pause();
            }
            else
            {
                m_CurrentSessionHasStarted = true;
                SessionStarted?.Invoke(m_CurrentSessionType);
            }
        }
        
        void Initialize_impl(TimerSettings settings)
        {
            Debug.Log("Initializing...");
            // TODO: We should check initialization status
            //  If status is NOT Uninitialized and we're NOT in Editor, something is wrong
            m_Settings = settings;
            ChangeSession(SessionType.Focus);
        }
        
        

    }
}
