using Codice.Client.Common;
using System;
using UnityEngine;

namespace LeftOut.GameJam.Clock
{
    public class DayNightManager : MonoBehaviour
    {
        public Vector3 noon;

        [Header("Sun")]
        public Light sun;
        public Gradient sunColor;
        public AnimationCurve sunIntensity;

        [Header("Moon")]
        public Light moon;
        public Gradient moonColor;
        public AnimationCurve moonIntensity;

        [Header("Other Lighting")]
        public AnimationCurve lightingIntensityMultiplier;
        public AnimationCurve reflectionsIntensityMultiplier;

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

        void UpdateDay(float progress)
        {
            sun.gameObject.SetActive(true);
            moon.gameObject.SetActive(false);

            //light intensity
            sun.intensity = sunIntensity.Evaluate(progress);

            //change colors
            sun.color = sunColor.Evaluate(progress);

            sun.transform.eulerAngles = ((progress - 0.14f) / 1.5f) * noon * 4.0f;

            RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(progress);
            RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(progress);
        }

        void UpdateNight(float progress)
        {
            moon.gameObject.SetActive(true);
            sun.gameObject.SetActive(false);

            //light intensity
            moon.intensity = moonIntensity.Evaluate(progress);

            //change colors
            moon.color = moonColor.Evaluate(progress);

            moon.transform.eulerAngles = ((progress - 0.14f) / 1.5f) * noon * 4.0f;

            RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(progress);
            RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(progress);
        }
    }
}
