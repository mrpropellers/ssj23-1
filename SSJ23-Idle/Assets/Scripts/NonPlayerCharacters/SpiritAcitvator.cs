using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using LeftOut.GameJam.Clock;
using LeftOut.GameJam.NonPlayerCharacters;
using UnityEditor;
//using Random = Unity.Mathematics;
using System;
using LeftOut.GameJam.Dialogue;
using System.Diagnostics;

namespace LeftOut.GameJam.NonPlayerCharacters
{
    public class SpiritActivator : MonoBehaviour
    {
        [SerializeField] private List<Spirit> spirits;
        // Start is called before the first frame update
        public void ActivateSpirits()
        {
            foreach (Spirit spirit in spirits)
            {
                if (spirit.isInScene == true) { 
                    spirit.gameObject.SetActive(true);
                    spirit.transform.GetChild(0).gameObject.SetActive(true);
                    spirit.spiritHasSpoken = false;
                }
            }

            UnityEngine.Debug.Log("Spirits have been activated");
            //get the spirts and then activate them
        }

        public void ActivateNewSpirit()
        {
            //create a new empty list of spirits
            List<Spirit> m_spirits = new List<Spirit>();

            //add all spirits who aren't in the scene to that list
            foreach (Spirit spirit in spirits)
            {
                if (spirit.isInScene == false) { m_spirits.Add(spirit); }
            }
            UnityEngine.Debug.Log(m_spirits.ToString());
            //if there's at least one spirit on that list, pick one at random and then add then flag it as in scene
            if (m_spirits.Count > 0)
            {
                int spiritIndex = UnityEngine.Random.Range(0, m_spirits.Count);
                m_spirits[spiritIndex].isInScene = true;
                m_spirits[spiritIndex].spiritHasSpoken = false;
            }
        }
        
        public void DeactivateSpirits()
        {
            foreach (Spirit spirit in spirits)
            {
                UnityEngine.Debug.Log(spirit.transform.GetChild(0).name);
                UnityEngine.Debug.Log("Child spirit name above.");
                spirit.transform.GetChild(0).gameObject.SetActive(false);
            }

        }

        void Start()
        {
 
            if (PomoTimer.Exists) {
                PomoTimer.Instance.SessionStarted.AddListener(OnSessionStarted);
            }

        }

        void OnSessionStarted(SessionType sessionTypeStarted)
        {
            UnityEngine.Debug.Log("OnSessionStarted Called");
            UnityEngine.Debug.Log(sessionTypeStarted);
            switch(sessionTypeStarted)
            {
                case SessionType.ShortBreak:
                    UnityEngine.Debug.Log("Short break! Let's wake up some SPIRITS");
                    ActivateSpirits();
                    break;

                case SessionType.LongBreak:
                    UnityEngine.Debug.Log("Long break! Make a new spirit and wake up some little ones!");
                    ActivateNewSpirit();
                    ActivateSpirits();
                    break;

                case SessionType.Focus:
                    UnityEngine.Debug.Log("Daylight is here hide them spirits!");
                    DeactivateSpirits();
                    break;
                
                case SessionType.UnInitialized:
                default:
                    throw new ArgumentOutOfRangeException();
            }
       
        }



        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
