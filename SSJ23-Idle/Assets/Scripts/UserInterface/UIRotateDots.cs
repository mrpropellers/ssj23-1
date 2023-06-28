using System;
using LeftOut.GameJam.Clock;
using UnityEngine;

namespace LeftOut.GameJam.UserInterface
{
    public class UIRotateDots : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 5.0f;
        
        bool spin => PomoTimer.Exists && PomoTimer.IsPlaying;
        float rotationSpeedScaled => PomoTimer.Exists ? PomoTimer.TimerTimeScale * rotationSpeed : rotationSpeed;

        // Update is called once per frame
        void Update()
        {
            if (spin)
            {
                gameObject.transform.Rotate(0, 0, rotationSpeedScaled * Time.deltaTime);
            }
        }

        [Obsolete("Spin is controlled by the Timer state")]
        public void StartStopSpin()
        {
            Debug.LogWarning("this doesn't do anything now", this);
        }
    }
}
