using System;
using System.Collections.Generic;
using System.Linq;
using SplineMesh;
using UnityEngine;
using UnityEngine.Serialization;
using Random = Unity.Mathematics.Random;
using LeftOut.Extensions;

namespace LeftOut.GameJam.Bonsai
{
    public class BonsaiGrower : MonoBehaviour
    {
        int m_NumActiveTrunks = 1;
        int m_CurrentBranchPrefabIndex = 0;

        [SerializeField, Range(1, 8)]
        int MaxPow2Branches = 4;
        [field: SerializeField, Range(0.01f, 1f)]
        internal float GrowthInterval { get; private set; } = 0.2f;
        [SerializeField]
        internal List<GrowingTreeLimb> BranchPrefabs;
        [SerializeField]
        internal List<GrowingTreeLimb> Trunks;
        [field: SerializeField, Range(0f, 2f)]
        internal float NewSproutDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0.01f, 0.5f)]
        internal float NewSproutStartingProgress { get; private set; } = 0.4f;
        // [SerializeField, Range(5f, 90f)]
        // float InPlaneStandardDeviation = 30f;
        // [SerializeField, Range(0f, 90f)]
        // float InPlaneLeafMaxDeviation = 45f;
        // [SerializeField, Range(0f, 30f)]
        // float OutPlaneStandardDeviation = 10f;
        // [SerializeField, Range(0f, 45f)]
        // float OutPlaneMaxDeviation = 15f;

        // Make this internal so we can test it from the debugger
        // TODO: We could optimize by caching the value of Mathf.Pow in OnValidate
        int MaxAllowedGrowingBranches => Mathf.RoundToInt(Mathf.Pow(2, MaxPow2Branches));
        internal Random Rand;
        //List<BonsaiNode> m_BonsaiNodes;

        int NextBranchIndex()
        {
            if (m_CurrentBranchPrefabIndex >= BranchPrefabs.Count)
            {
                m_CurrentBranchPrefabIndex = 0;
                return m_CurrentBranchPrefabIndex;
            }

            return m_CurrentBranchPrefabIndex++;
        }

        void Awake()
        {
            //m_BonsaiNodes = new List<BonsaiNode>();
            Rand = new Random();
        }

        void Start()
        {
            Rand.InitState((uint)DateTime.Now.Ticks);
            foreach (var trunk in Trunks)
            {
                trunk.Generation = 0;
            }
            //m_BonsaiNodes.Clear();
            // var rootSpline = m_SplineContainer.Spline;
            // for(var i = 0; i < m_SplineContainer.Spline.Count; ++i)
            // {
            //     m_BonsaiNodes.Add(new BonsaiNode(rootSpline, i, 0));
            // }
        }

        IEnumerable<GrowingTreeLimb> ActiveTrunks
        {
            get
            {
                for (var i = 0; i < m_NumActiveTrunks; ++i)
                {
                    yield return Trunks[i];
                }
            }
        }
        
        internal void GrowTree(float timeInterval)
        {
            Debug.Log($"Growing tree by {GrowthInterval} for {timeInterval} seconds ...");
            foreach (var trunk in ActiveTrunks)
            {
                trunk.GrowByProgress(timeInterval, GrowthInterval);
            }
        }

        internal void SproutNewBranches()
        {
            var numGrowingBranches = ActiveTrunks.Sum(trunk => trunk.CountGrowingBranches());
            var numAllowedSprouts = MaxAllowedGrowingBranches - numGrowingBranches;
            var context = new BonsaiGrowerContext(numAllowedSprouts);
            foreach (var trunk in ActiveTrunks)
            {
                SproutBranches(context, trunk);
            }
        }

        /// <summary>
        /// Find suitable points along each trunk and child branches for new sprouts to start
        /// We define this here instead of inside the GrowingTreeLimb class because the limb
        /// doesn't have enough context. The BonsaiGrower will (eventually) hold the game state
        /// that determines how many branches to grow and by how much
        /// </summary>
        void SproutBranches(BonsaiGrowerContext context, GrowingTreeLimb limb)
        {
            var childrenFirst = Rand.NextFloat() < 0.75f;

            if (childrenFirst)
            {
                SproutFromChildBranches(context, limb);
            }
            
            var numSprouts = ComputeAmountNewBranches(context, limb);
            Debug.Log($"Sprouting {numSprouts} new branches off {limb.name}");
            for (var n = 0; n < numSprouts; ++n)
            {
                var sample = limb.SampleNewBranchLocation(ref Rand);
                var position = limb.transform.TransformPoint(sample.location);
                var tangent = limb.transform.TransformDirection(sample.tangent);
                // >>> TODO: Clamp forward to reasonable world limits somehow?
                var orientation = Quaternion.LookRotation(tangent, Vector3.up);
                var prefab = BranchPrefabs[NextBranchIndex()];
                Debug.Log($"Spawning a {prefab.name}");
                var sprout = limb.SproutBranch(prefab, position, orientation);
                sprout.GrowByProgress(NewSproutDuration, NewSproutStartingProgress);
                context.AcknowledgeNewSprout();
            }

            if (!childrenFirst)
            {
                SproutFromChildBranches(context, limb);
            }
        }

        void SproutFromChildBranches(BonsaiGrowerContext context, GrowingTreeLimb limb)
        {
            if (!limb.IsInitialized)
            {
                return;
            }
            limb.Branches.Shuffle();   
            foreach (var branch in limb.Branches.Where(b => b.IsInitialized))
            {
                SproutBranches(context, branch);
            }
        }

        int ComputeAmountNewBranches(BonsaiGrowerContext context, GrowingTreeLimb limb)
        {
            // TODO: This logic needs to be better eventually
            const float branchesPerUnit = 0.4f;
            int maxAllowedBranches = 8;
            for (var i = 0; i < limb.Generation && i < 3; ++i)
            {
                maxAllowedBranches /= 2;
            }
            
            maxAllowedBranches -= limb.Branches.Count(b => !b.IsFullyGrown);
            var numBranchesPossible = (int)(limb.CurrentLength * branchesPerUnit);
            return Math.Clamp(numBranchesPossible, 0, Math.Min(context.SproutsRemaining, maxAllowedBranches));
        }

        internal void GrowEntireTrunk(int trunkIndex)
        {
            var trunk = Trunks[trunkIndex];
            Trunks[trunkIndex].GrowByProgress(0, 1f);
        }
    }
}
