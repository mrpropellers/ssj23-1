using System.Collections.Generic;
using UnityEngine;
using LeftOut.GameJam.Clock;
using System;
using System.Linq;

namespace LeftOut.GameJam.NonPlayerCharacters
{
    public class SpiritActivator : MonoBehaviour
    {
        [SerializeField] public List<Spirit> spirits;
        [SerializeField] private bool firstBreak;
        public int NumActiveSpirits => spirits.Count(s => s.isInScene);

        void Start()
        {

            if (PomoTimer.Exists)
            {
                PomoTimer.Instance.SessionStarted.AddListener(OnSessionStarted);
                PomoTimer.Instance.SessionEnded.AddListener(OnSessionEnded);
                activateFirstSpirit();
            }

        }

        public void ActivateSpirits()
        {
            foreach (Spirit spirit in spirits)
            {
                if (spirit.isInScene) {
                    spirit.SetSpiriteActiveTo(true);
                    spirit.SetSpriteActiveTo(true);
                    spirit.SetSpiritHasSpokenTo(false);
                }
            }

            UnityEngine.Debug.Log("Spirits have been activated");

        }

        public void ActivateNewSpirit()
        {
            //create a new empty list of spirits
            List<Spirit> m_spirits = new List<Spirit>();

            //add all spirits who aren't in the scene to that list
            foreach (Spirit spirit in spirits)
            {
                if (!spirit.isInScene)
                {
                    m_spirits.Add(spirit);
                }
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
                spirit.SetSpiriteActiveTo(false);
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
                    // (Devin): Moving this to OnSessionEnded
                    //UnityEngine.Debug.Log("Daylight is here hide them spirits!");
                    //DeactivateSpirits();
                    break;
                
                case SessionType.UnInitialized:
                default:
                    throw new ArgumentOutOfRangeException();
            }
       
        }

        void OnSessionEnded(SessionType sessionEnded)
        {
            switch(sessionEnded)
            {
                case SessionType.ShortBreak:
                case SessionType.LongBreak:
                    UnityEngine.Debug.Log("Daylight is here hide them spirits!");
                    DeactivateSpirits();
                    break;

                case SessionType.Focus:
                case SessionType.UnInitialized:
                default:
                    break;
            }
            
        }
        void activateFirstSpirit()
        {

                UnityEngine.Debug.Log("Spawning tutorial spirit.");

                List<Spirit> m_spirits = new List<Spirit>();

                //add all spirits who aren't in the scene to that list
                foreach (Spirit spirit in spirits)
                {
                    if (!spirit.isInScene && spirit.name != "Spirit_QuestionMark")
                    {
                        m_spirits.Add(spirit);
                    }
                }
                UnityEngine.Debug.Log(m_spirits.ToString());
                //if there's at least one spirit on that list, pick one at random and then add then flag it as in scene
                if (m_spirits.Count > 0)
                {
                    int spiritIndex = UnityEngine.Random.Range(0, m_spirits.Count);
                    m_spirits[spiritIndex].isInScene = true;
                    m_spirits[spiritIndex].spiritHasSpoken = false;
                    m_spirits[spiritIndex].spiritIsTutorial = true;
            }
            
        }
        // Update is called once per frame
        void Update()
        {
        
        }

    }
}
