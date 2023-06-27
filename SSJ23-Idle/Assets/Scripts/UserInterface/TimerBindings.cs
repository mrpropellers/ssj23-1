using UnityEngine;
using LeftOut.GameJam.Clock;

namespace LeftOut.GameJam.UserInterface
{
    public class TimerBindings : MonoBehaviour
    {
        public void ToggleTimer()
        {
            if (CheckForTimer())
            {
                PomoTimer.Toggle();
            }
        }

        public void FastForward()
        {
            if (CheckForTimer())
            {
                PomoTimer.FastForward();
            }
        }

        bool CheckForTimer()
        {
            if (PomoTimer.Exists)
            {
                return true;
            }
            else
            {
                Debug.LogWarning($"No {nameof(PomoTimer)} in Scene - not doing anything.");
                return false;
            }
        }
    }
}
