using System;
using System.Collections.Generic;
using System.Linq;
using LeftOut.GameJam.Clock;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace LeftOut.GameJam.Bonsai
{
    public class BonsaiGrower : MonoBehaviour
    {
        int m_NumActiveTrunks = 1;
        int m_CurrentBranchPrefabIndex = 0;
        int m_ActiveGrowthIndex;

        List<GrowingTreeLimb> m_ActiveGrowth;

        [SerializeField, Range(1, 8)]
        int MaxPow2Branches = 4;
        [field: SerializeField, Range(0.01f, 1f)]
        internal float GrowthInterval { get; private set; } = 0.2f;

        [SerializeField, Range(1, 8)]
        int MaxGrowStepsPerFrame = 2;
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
            m_ActiveGrowth = new List<GrowingTreeLimb>();

            if (PomodoroTimer.Exists)
            {
                PomodoroTimer.Instance.SessionStarted.AddListener(OnSessionStart);
                PomodoroTimer.Instance.SessionEnded.AddListener(OnSessionEnd);
            }
            //m_BonsaiNodes.Clear();
            // var rootSpline = m_SplineContainer.Spline;
            // for(var i = 0; i < m_SplineContainer.Spline.Count; ++i)
            // {
            //     m_BonsaiNodes.Add(new BonsaiNode(rootSpline, i, 0));
            // }
        }

        void Update()
        {
            if (m_ActiveGrowth.Count == 0)
            {
                return;
            }

            var limbIndex = m_ActiveGrowthIndex;
            var numLimbsStepped = 0;
            for (var numLimbsInspected = 0; 
                 numLimbsInspected < m_ActiveGrowth.Count && numLimbsStepped < MaxGrowStepsPerFrame; 
                 ++numLimbsInspected)
            {
                if (limbIndex >= m_ActiveGrowth.Count)
                {
                    limbIndex = 0;
                }
                var limb = m_ActiveGrowth[limbIndex];
                if (limb.IsActivelyGrowing)
                {
                    limb.DoGrowthUpdate();
                    ++numLimbsStepped;
                }
                
                // Check whether limb is still growing after the growth update (that may have been it's final one)
                if (limb.IsActivelyGrowing)
                {
                    ++limbIndex;
                }
                else
                {
                    m_ActiveGrowth.RemoveAt(limbIndex);
                }
            }

            m_ActiveGrowthIndex = limbIndex;
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
            m_ActiveGrowth.RemoveAll(limb => !limb.IsActivelyGrowing);
            if (m_ActiveGrowth.Count != 0)
            {
                Debug.LogWarning($"Still have {m_ActiveGrowth.Count} limbs growing. They may get clobbered.");
            }
            foreach (var trunk in ActiveTrunks)
            {
                BeginLimbGrowth(trunk, GrowthInterval, timeInterval);
                BeginAllBranchGrowth(trunk.Branches, GrowthInterval, timeInterval);
            }
        }

        internal void SproutNewBranches()
        {
            var numGrowingBranches = ActiveTrunks.Sum(trunk => trunk.CountGrowingBranches());
            var numAllowedSprouts = MaxAllowedGrowingBranches - numGrowingBranches;
            var context = new BranchSproutingContext(numAllowedSprouts);
            foreach (var trunk in ActiveTrunks)
            {
                SproutBranches(context, trunk);
            }
        }

        void BeginAllBranchGrowth(IEnumerable<GrowingTreeLimb> branches, float growthInterval, float timeInterval)
        {
            foreach (var branch in branches)
            {
                if (branch.HasAnyBranches)
                {
                    BeginAllBranchGrowth(branch.Branches, growthInterval, timeInterval);
                }
                BeginLimbGrowth(branch, growthInterval, timeInterval);
            }
        }

        void BeginLimbGrowth(GrowingTreeLimb limb, float growthInterval, float timeInterval)
        {
            if (m_ActiveGrowth.Contains(limb))
            {
                Debug.LogWarning($"{limb} was already growing - old targets will be overwritten.");
            }
            else
            {
                m_ActiveGrowth.Add(limb);
            }
            
            limb.SetGrowthTarget(growthInterval, timeInterval);   
        }

        void OnSessionStart(PomodoroSession session)
        {
            
        }

        void OnSessionEnd(PomodoroSession session)
        {
            
        }

        /// <summary>
        /// Find suitable points along each trunk and child branches for new sprouts to start
        /// We define this here instead of inside the GrowingTreeLimb class because the limb
        /// doesn't have enough context. The BonsaiGrower will (eventually) hold the game state
        /// that determines how many branches to grow and by how much
        /// </summary>
        void SproutBranches(BranchSproutingContext context, GrowingTreeLimb limb)
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
                BeginLimbGrowth(sprout, NewSproutStartingProgress, NewSproutDuration);
                context.AcknowledgeNewSprout();
            }

            if (!childrenFirst)
            {
                SproutFromChildBranches(context, limb);
            }
        }

        void SproutFromChildBranches(BranchSproutingContext context, GrowingTreeLimb limb)
        {
            if (!limb.IsInitialized)
            {
                return;
            }
            limb.ShuffleBranchOrder();   
            foreach (var branch in limb.Branches.Where(b => b.IsInitialized))
            {
                SproutBranches(context, branch);
            }
        }

        int ComputeAmountNewBranches(BranchSproutingContext context, GrowingTreeLimb limb)
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
            BeginLimbGrowth(trunk, 1f, 0f);
        }
    }
}
