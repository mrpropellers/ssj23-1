using System;
using System.Collections;
using UnityEngine;
using LeftOut.Extensions.Mathematics;

namespace LeftOut.GameJam.Bonsai
{
    /// <summary>
    /// A class that enables us to debug the Bonsai mechanics while in the Editor, but that will not compile
    /// into our built game so we can ensure these functions aren't accessible to the end user
    /// </summary>
    [RequireComponent(typeof(BonsaiGrower))]
    public class BonsaiDebugger : MonoBehaviour
    {
#if UNITY_EDITOR
        // 0x6E624EB7u
        public uint RandSeed = 0x6E624EB7u;
        public float GrowthTime = 10f;
        
        BonsaiGrower m_Grower;

        BonsaiGrower GrowerNoNull
        {
            get
            {
                if (m_Grower == null)
                {
                    m_Grower = GetComponent<BonsaiGrower>();
                }

                return m_Grower;
            }
        }

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
            m_Grower.GrowTree(GrowthTime);
            StartCoroutine(SproutBranchesAfterGrowth(GrowthTime));
        }

        IEnumerator SproutBranchesAfterGrowth(float timeToWait)
        {
            yield return new WaitForSeconds(timeToWait);
            m_Grower.SproutNewBranches();
        }

        internal void InitRandom()
        {
            m_Grower.Rand.InitState(RandSeed);
        }

        internal void ShowTrunks()
        {
            var grower = GrowerNoNull;
            for (var i = 0; i < grower.Trunks.Count; i++)
            {
                grower.GrowEntireTrunk(i);
            }
        }

        internal void HideTrunks()
        {
            foreach (var trunk in GrowerNoNull.Trunks)
            {
                trunk.Reset();
            }
        }

        internal void VerifyNormalSampling()
        {
            m_Grower.Rand.VerifyNormalDistribution(1000000);
        }
#endif
    }
}
