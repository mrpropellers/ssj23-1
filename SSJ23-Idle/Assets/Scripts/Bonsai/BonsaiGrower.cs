using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;

namespace LeftOut.GameJam.Bonsai
{
    public class BonsaiGrower : MonoBehaviour
    {
        int m_NumActiveTrunks = 1;
        
        [SerializeField, Range(0.01f, 1f)]
        float GrowthInterval;
        [SerializeField]
        internal List<GrowingTreeLimb> BranchPrefabs;
        [SerializeField]
        internal List<GrowingTreeLimb> Trunks;
        // [SerializeField, Range(5f, 90f)]
        // float InPlaneStandardDeviation = 30f;
        // [SerializeField, Range(0f, 90f)]
        // float InPlaneLeafMaxDeviation = 45f;
        // [SerializeField, Range(0f, 30f)]
        // float OutPlaneStandardDeviation = 10f;
        // [SerializeField, Range(0f, 45f)]
        // float OutPlaneMaxDeviation = 15f;

        // Make this internal so we can test it from the debugger
        internal Random Rand;
        //List<BonsaiNode> m_BonsaiNodes;

        void Awake()
        {
            //m_BonsaiNodes = new List<BonsaiNode>();
            Rand = new Random();
        }

        void Start()
        {
            Rand.InitState((uint)DateTime.Now.Ticks);
            //m_BonsaiNodes.Clear();
            // var rootSpline = m_SplineContainer.Spline;
            // for(var i = 0; i < m_SplineContainer.Spline.Count; ++i)
            // {
            //     m_BonsaiNodes.Add(new BonsaiNode(rootSpline, i, 0));
            // }
        }
        
        internal void GrowTrunks(float timeInterval)
        {
            Debug.Log($"Growing trunks by {GrowthInterval} for {timeInterval} seconds ...");
            for (var i = 0; i < m_NumActiveTrunks; ++i)
            {
                Trunks[i].GrowByProgress(timeInterval, GrowthInterval);
            }
        }
        
        internal void GrowEntireTrunk(int trunkIndex)
        {
            var trunk = Trunks[trunkIndex];
            if (!trunk.IsInitialized)
            {
                trunk.Init();
            }
            Trunks[trunkIndex].GrowByProgress(0, 1f);
        }
    }
}
