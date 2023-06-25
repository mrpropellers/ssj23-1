using UnityEngine;

namespace LeftOut.GameJam.Bonsai
{
    class GrowthTracker
    {
        internal readonly double StartTime;
        internal readonly double RatePerSecond;
        internal readonly float StartProgress;
        internal readonly float FinalProgress;

        internal GrowthTracker(float startProgress, float finalProgress, float growthDuration)
        {
            StartTime = Time.timeAsDouble;
            StartProgress = startProgress;
            FinalProgress = finalProgress;
            RatePerSecond = 1.0 / growthDuration;
        }

        internal double TimeElapsed => Time.timeAsDouble - StartTime;
        internal float CurrentProgressTarget
        {
            get
            {
                var t = Mathf.Clamp01((float)(TimeElapsed * RatePerSecond));
                return Mathf.Lerp(StartProgress, FinalProgress, t);
            }
        }
    }
}
