using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace LeftOut.GameJam.Clock
{
    public class DayNightManager : MonoBehaviour
    {
        Vector3 m_InitialUp;
        Quaternion m_InitialOrientation;
        float m_LastProgressValue;
        bool m_SunShadowsOn;
        
        [SerializeField]
        LightShadows AtmosphereShadows = LightShadows.Hard;

        [SerializeField]
        Gradient m_SunColorGradient;

        [SerializeField]
        Light m_Sun;
        [SerializeField]
        Light m_Moon;
        
        [FormerlySerializedAs("dayNight")]
        public GameObject Atmosphere;
        

        void Start()
        {
            m_InitialUp = Atmosphere.transform.up;
            m_InitialOrientation = Atmosphere.transform.rotation;
            m_Sun.color = m_SunColorGradient.Evaluate(0);
            m_Moon.shadows = LightShadows.None;
            m_Sun.shadows = AtmosphereShadows;
            m_SunShadowsOn = true;
        }

        void Update()
        {
            ApplyTimerUpdate();
        }

        void ApplyTimerUpdate()
        {
            // This is a 0-1 value that indicates progress through the session
            // 0 => session just started
            // 1 => session is ending
            var progress = PomoTimer.ProgressThroughSession;
            if (Mathf.Approximately(progress, m_LastProgressValue))
            {
                return;
            }

            m_LastProgressValue = progress;
            // This is an enum that tells you what type of session the user is currently doing
            // Right now we can just assume if they're focusing, it's daytime, and breaks happen at night
            // Add phase offset if it's nighttime
            var session = PomoTimer.currentSessionType;
            if (session == SessionType.Focus)
            {
                // This is roughly when the horizon switches...
                if (m_SunShadowsOn && progress > 0.95f)
                {
                    m_Sun.shadows = LightShadows.None;
                    m_Moon.shadows = AtmosphereShadows;
                    m_SunShadowsOn = false;
                }
                m_Sun.color = m_SunColorGradient.Evaluate(progress);
            }
            else
            {

                if (!m_SunShadowsOn && progress > 0.95f)
                {
                    m_Moon.shadows = LightShadows.None;
                    m_Sun.shadows = AtmosphereShadows;
                    m_SunShadowsOn = true;
                }

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
