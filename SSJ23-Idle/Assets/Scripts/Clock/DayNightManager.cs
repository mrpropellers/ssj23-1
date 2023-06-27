using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LeftOut.GameJam.Clock
{
    public class DayNightManager : MonoBehaviour
    {
        Vector3 m_InitialUp;
        Quaternion m_InitialOrientation;

        [SerializeField]
        Gradient m_SunColorGradient;

        [SerializeField]
        Light m_Sun;
        
        [FormerlySerializedAs("dayNight")]
        public GameObject Atmosphere;
        

        void Start()
        {
            m_InitialUp = Atmosphere.transform.up;
            m_InitialOrientation = Atmosphere.transform.rotation;
            m_Sun.color = m_SunColorGradient.Evaluate(0);
        }

        void Update()
        {
            if (!PomoTimer.IsPlaying)
            {
                return;
            }
            ApplyTimerUpdate();
        }

        void ApplyTimerUpdate()
        {
            // This is a 0-1 value that indicates progress through the session
            // 0 => session just started
            // 1 => session is ending
            var progress = PomoTimer.ProgressThroughSession;
            // This is an enum that tells you what type of session the user is currently doing
            // Right now we can just assume if they're focusing, it's daytime, and breaks happen at night
            // Add phase offset if it's nighttime
            var session = PomoTimer.currentSessionType;
            if (session == SessionType.Focus)
            {
                m_Sun.color = m_SunColorGradient.Evaluate(progress);
            }
            else
            {
                progress += 1f;
                // Ensure the sun gets set back to start-of-day color after it's definitely set, but before it next rises
                if (progress > 1.5f)
                {
                    m_Sun.color = m_SunColorGradient.Evaluate(0f);
                }
            }

            // If effectively no rotation is occuring, skip this update to keep the orientation from resetting to identity
            // if (Mathf.Approximately(progress, 0f))
            // {
            //     return;
            // }
            // Rotate about the local up-axis of the transform to get spherical coordinate rotation
            Atmosphere.transform.rotation = 
                Quaternion.AngleAxis(-180f * progress, m_InitialUp) * m_InitialOrientation;
        }
    }
}
