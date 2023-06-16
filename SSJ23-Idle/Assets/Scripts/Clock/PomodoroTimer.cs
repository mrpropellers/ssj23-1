using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeftOut.GameJam.Clock
{
    public class PomodoroTimer : SingletonBehaviour<PomodoroTimer>
    {
        TimerSettings m_Settings;
        PomodoroSession m_CurrentSession;
        int m_NumShortBreaksTaken;
        float m_TimeLastSessionChange;
        float m_CurrentSessionLength;

        [SerializeField]
        TimerSettings DefaultSettings;

        // Returns a value in range [0-1] indicating how far through the current session we are
        public static float ProgressThroughSession => Instance.GetProgressThroughSession();
        public static PomodoroSession CurrentSession => Instance.GetCurrentSession();
        float TimeInCurrentSession => Time.time - m_TimeLastSessionChange;
        
        void Start()
        {
            // TODO: Eventually this should be initialized by something else via a public method
            Initialize_impl(DefaultSettings);
        }

        void Update()
        {
            EnsureSessionUpToDate();
        }
        
        //public static void Initialize(TimerSettings settings) => Instance.Initialize_impl(settings);

        void EnsureSessionUpToDate()
        {
            // TODO: This may need to be handled by a user-facing class so we can stop the timer in between sessions
            while (TimeInCurrentSession >= m_CurrentSessionLength)
            {
                MoveToNextSession();
            }
        }
        
        float GetProgressThroughSession()
        {
            EnsureSessionUpToDate();
            return TimeInCurrentSession / m_CurrentSessionLength;
        }

        PomodoroSession GetCurrentSession()
        {
            EnsureSessionUpToDate();
            return m_CurrentSession;
        }

        void MoveToNextSession()
        {
            m_TimeLastSessionChange += m_CurrentSessionLength;
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
            Debug.Log($"Changing session from {m_CurrentSession} to {newSession}");
            m_CurrentSession = newSession;
            m_CurrentSessionLength = m_Settings.LookUpDuration(newSession);
        }
        
        void Initialize_impl(TimerSettings settings)
        {
            Debug.Log("Initializing...");
            // TODO: We should check initialization status
            //  If status is NOT Uninitialized and we're NOT in Editor, something is wrong
            m_Settings = settings;
            m_CurrentSession = PomodoroSession.Focus;
            m_TimeLastSessionChange = Time.time;
            m_CurrentSessionLength = m_Settings.LookUpDuration(m_CurrentSession);
        }
        
        

    }
}
