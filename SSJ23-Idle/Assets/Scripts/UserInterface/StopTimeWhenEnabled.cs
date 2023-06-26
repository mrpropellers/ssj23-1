using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeftOut.GameJam.UserInterface
{
    public class StopTimeWhenEnabled : MonoBehaviour
    {
        void OnEnable()
        {
            Debug.Log("Freezing time.", this);
            Time.timeScale = 0f;
        }

        void OnDisable()
        {
            Debug.Log("Unfreezing time", this);
            Time.timeScale = 1f;
        }
    }
}
