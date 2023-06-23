using System;
using UnityEngine;

namespace LeftOut.GameJam.PlayerInteractions
{
    [RequireComponent(typeof(InteractionExecutor))]
    public class InteractionModeSetter : MonoBehaviour
    {
        #if UNITY_EDITOR
        [SerializeField]
        InteractionMode m_ModeOverride = InteractionMode.Undefined;
        #endif
        
        InteractionExecutor m_InteractionExecutor;

        void Start()
        {
            m_InteractionExecutor = GetComponent<InteractionExecutor>();
        }

        void Update()
        {
            #if UNITY_EDITOR
            if (m_ModeOverride != InteractionMode.Undefined)
            {
                m_InteractionExecutor.Mode = m_ModeOverride;
            }
            #endif
        }
    }
}
