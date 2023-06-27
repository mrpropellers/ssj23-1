using System;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using LeftOut.GameJam.Clock;
using LeftOut.GameJam.NonPlayerCharacters;

namespace LeftOut.GameJam
{
    public class MusicBindings : MonoBehaviour
    {
        bool m_GameHasJustStarted;

        [SerializeField]
        SpiritActivator m_SpiritManager;
        
        [SerializeField]
        StudioEventEmitter m_FocusAmbience;
        [SerializeField]
        StudioEventEmitter m_FocusMusic;

        [SerializeField]
        StudioEventEmitter m_BreakAmbience;
        [SerializeField]
        StudioEventEmitter m_BreakMusic;

        int SpiritCount => m_SpiritManager != null ? m_SpiritManager.NumActiveSpirits : 0;

        void Start()
        {
            m_GameHasJustStarted = true;
            m_FocusAmbience.Play();
            if (PomoTimer.Exists)
            {
                PomoTimer.Instance.SessionStarted.AddListener(OnSessionStarted);
                PomoTimer.Instance.SessionEnded.AddListener(OnSessionEnded);
            }
            else
            {
                Debug.LogWarning("No Timer in Scene - can't bind music to sessions");
            }
        }

        void OnDisable()
        {
            if (PomoTimer.Exists)
            {
                PomoTimer.Instance.SessionStarted.RemoveListener(OnSessionStarted);
                PomoTimer.Instance.SessionEnded.RemoveListener(OnSessionEnded);
            }
        }

        void OnSessionStarted(SessionType sessionStarted)
        {
            Debug.Log($"Picking new music for {sessionStarted}...");
            switch (sessionStarted)
            {
                case SessionType.Focus:
                    m_FocusMusic.Play();
                    break;
                case SessionType.LongBreak:
                case SessionType.ShortBreak:
                    var spiritCount = SpiritCount;
                    Debug.Log($"Setting spirit count to {spiritCount}!");
                    RuntimeManager.StudioSystem.setParameterByName("SpiritCount", spiritCount);
                    m_BreakMusic.Play();
                    break;
                default:
                    break;
            }
        }

        void OnSessionEnded(SessionType sessionEnded)
        {
            Debug.Log($"Picking new ambience now that {sessionEnded} ended...");
            switch (sessionEnded)
            {
                case SessionType.Focus:
                    m_FocusAmbience.Stop();
                    m_FocusMusic.Stop();
                    m_BreakAmbience.Play();
                    break;
                case SessionType.LongBreak:
                case SessionType.ShortBreak:
                    m_BreakAmbience.Stop();
                    m_BreakMusic.Stop();
                    m_FocusAmbience.Play();
                    break;
                default:
                    break;
            }
            
        }
    }
}
