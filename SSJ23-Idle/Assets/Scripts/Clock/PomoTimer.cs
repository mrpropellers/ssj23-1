using System;
using UnityEngine;
using UnityEngine.Events;

namespace LeftOut.GameJam.Clock
{
    public class PomoTimer : SingletonBehaviour<PomoTimer>
    {
        [SerializeField]
        bool m_IsPlaying;
        
        TimerSettings m_Settings;
        SessionType m_CurrentSessionType;
        int m_NumShortBreaksTaken;
        bool m_CurrentSessionHasStarted;
        float m_TimeInSession;
        float m_CurrentSessionLength;
        float m_TimerTimeScale = 1f;

        // [SerializeField]
        // bool ShouldPauseBetweenSessions = true;

        #if UNITY_EDITOR
        [SerializeField]
        #endif
        bool PauseBetweenSessions = true;
        [SerializeField]
        TimerSettings DefaultSettings;
        [SerializeField, Range(0.2f, 0.8f)]
        float m_FastForwardMultiplier;
        [field: SerializeField]
        public UnityEvent<SessionType> SessionStarted { get; private set; }
        [field: SerializeField]
        public UnityEvent<SessionType> SessionEnded { get; private set; }
        // Event specifically for when a user plays/pauses when not at a session boundary
        // true => Playing, false => Paused
        [field: SerializeField]
        public UnityEvent<bool> UserPlayPause { get; private set; }

        // Returns a value in range [0-1] indicating how far through the current session we are
        public static float ProgressThroughSession => Instance.GetProgressThroughSession();
        public static SessionType currentSessionType => Instance.GetCurrentSession();

        public static float TimerTimeScale => Instance.m_TimerTimeScale;
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
            Instance.UserPlayPause?.Invoke(Instance.m_IsPlaying);
        }

        public static void FastForward() => Instance.FastForward_impl();
        
        void Start()
        {
            // TODO: Eventually this should be initialized by something else via a public method
            Initialize_impl(DefaultSettings);
            //set up listener for triggering spirits
        }

        void Update()
        {
            if (m_IsPlaying)
            {
                m_TimeInSession += Time.deltaTime * m_TimerTimeScale;
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
            m_TimerTimeScale = 1f;
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

        void FastForward_impl()
        {
            if (!m_IsPlaying)
            {
                Play();
            }

            m_TimerTimeScale = Mathf.Max(CurrentTime * m_FastForwardMultiplier, 10f);
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
