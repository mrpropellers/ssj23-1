using System;
using UnityEngine;
using LeftOut.Extensions.Mathematics;

namespace LeftOut.GameJam.Bonsai
{
    /// <summary>
    /// A class that enables us to debug the Bonsai mechanics while in the Editor, but that will not compile
    /// into our built game so we can ensure these functions aren't accessible to the end user
    /// </summary>
    public class BonsaiDebugger : MonoBehaviour
    {
#if UNITY_EDITOR
        // 0x6E624EB7u
        public uint RandSeed = 0x6E624EB7u;
        
        BonsaiGrower m_Grower;

        void Awake()
        {
            m_Grower = GetComponent<BonsaiGrower>();
        }

        internal void GrowOneGeneration()
        {
            if (!Application.isPlaying)
            {
                Debug.LogError("Can't grow Bonsai when not in Play mode!");
                return;
            }
            m_Grower.GrowGeneration();
        }

        internal void VerifyNormalSampling()
        {
            m_Grower.Rand.VerifyNormalDistribution(1000000);
        }
#endif
    }
}
