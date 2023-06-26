using System;
using LeftOut.GameJam.Clock;
using UnityEngine;

namespace LeftOut.GameJam.UserInterface
{
    public class UIRotateDots : MonoBehaviour
    {
        [SerializeField] float rotationSpeed = 5.0f;
        bool spin => Timer.IsPlaying;

        // Update is called once per frame
        void Update()
        {
            if (spin)
            {
                gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            }
        }

        [Obsolete("Spin is controlled by the Timer state")]
        public void StartStopSpin()
        {
            Debug.LogWarning("this doesn't do anything now", this);
        }
    }
}
