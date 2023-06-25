using UnityEngine;

namespace LeftOut.GameJam.Bonsai
{
    [System.Serializable]
    class GrowthTracker
    {
        internal readonly float StartProgress;
        internal readonly float FinalProgress;

        internal GrowthTracker(float startProgress, float finalProgress)
        {
            if (finalProgress > 1f)
            {
                Debug.LogWarning($"Attempting to grow past 100% of Spline length. Clamping.");
                finalProgress = 1f;
            }

            if (startProgress > finalProgress)
            {
                Debug.LogWarning($"{nameof(startProgress)} must be <= {nameof(finalProgress)}. Clamping...");
                startProgress = finalProgress;
            }

            StartProgress = startProgress;
            FinalProgress = finalProgress;
        }

        internal float ComputeProgress(float t) => 
            Mathf.Lerp(StartProgress, FinalProgress, t);
    }
}
