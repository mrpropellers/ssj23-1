using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LeftOut.GameJam.Clock;
using LeftOut.Toolkit;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace LeftOut.GameJam.Bonsai
{
    public class BonsaiGrower : MonoBehaviour
    {
        enum GrowthTargets
        {
            // Any active growths should be finished ASAP
            None,
            PreGameSprouting,
            // Growing existing limbs (based on PomodoroTimer)
            ExistingLimbs,
            // Grow new sprouts (based on elapsed realtime)
            NewSprouts
        }
        
        int m_NumActiveTrunks = 1;
        int m_CurrentBranchPrefabIndex = 0;
        int m_ActiveGrowthIndex;
        bool m_HasSproutedInitialBranches;

        GrowthTargets m_GrowthTargets;
        double m_SproutStartTime;
        double m_SproutGrowthRate;
        List<GrowingTreeLimb> m_ActiveGrowth;

        #region Inspector Fields
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
        [field: SerializeField, Range(0.01f, 2f)]
        internal float NewSproutDuration { get; private set; } = 0.5f;
        [field: SerializeField, Range(0.01f, 0.5f)]
        internal float NewSproutStartingProgress { get; private set; } = 0.4f;

        [SerializeField, Range(0f, 1f)]
        float m_StartingTrunkProgress = 0.6f;
        // [SerializeField, Range(5f, 90f)]
        // float InPlaneStandardDeviation = 30f;
        // [SerializeField, Range(0f, 90f)]
        // float InPlaneLeafMaxDeviation = 45f;
        // [SerializeField, Range(0f, 30f)]
        // float OutPlaneStandardDeviation = 10f;
        // [SerializeField, Range(0f, 45f)]
        // float OutPlaneMaxDeviation = 15f;
        #endregion

        #region Properties
        // TODO: We could optimize by caching the value of Mathf.Pow in OnValidate
        int MaxAllowedGrowingBranches => Mathf.RoundToInt(Mathf.Pow(2, MaxPow2Branches));
        internal Random Rand;
        double TimeElapsedSinceSprout => Time.timeAsDouble - m_SproutStartTime;
        float ProgressThroughSproutInterval => Mathf.Clamp01((float)(m_SproutGrowthRate * TimeElapsedSinceSprout));
        
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

        #endregion
        
        #region Unity Events
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
            m_SproutGrowthRate = 1.0 / NewSproutDuration;
            m_HasSproutedInitialBranches = false;

            if (Timer.Exists)
            {
                m_GrowthTargets = GrowthTargets.PreGameSprouting;
                Timer.Pause();
                Timer.Instance.SessionStarted.AddListener(OnSessionStart);
                Timer.Instance.SessionEnded.AddListener(OnSessionEnd);
                Trunks[0].SetGrowthTarget(m_StartingTrunkProgress);
                m_ActiveGrowth.Add(Trunks[0]);
            }
            //m_BonsaiNodes.Clear();
            // var rootSpline = m_SplineContainer.Spline;
            // for(var i = 0; i < m_SplineContainer.Spline.Count; ++i)
            // {
            //     m_BonsaiNodes.Add(new BonsaiNode(rootSpline, i, 0));
            // }
        }

        void OnValidate()
        {
            m_SproutGrowthRate = 1.0 / NewSproutDuration;
        }

        void Update()
        {
            if (m_ActiveGrowth.Count == 0)
            {
                DebugExtras.LogWhenPaused("No growth to grow. Returning.", this);
                return;
            }

            DoRoundRobinGrowthUpdates();

            if (!m_HasSproutedInitialBranches && !HasActiveGrowth())
            {
                SproutNewBranches();
                SproutNewBranches();
                m_HasSproutedInitialBranches = true;
                m_GrowthTargets = GrowthTargets.PreGameSprouting;
            }
            
        }

        void OnSessionStart(SessionType sessionTypeStarted)
        {
            if (sessionTypeStarted == SessionType.Focus)
            {
                StartCoroutine(EnsureGrowthCompleteAndThen(GrowTree));
            }
        }

        void OnSessionEnd(SessionType sessionTypeEnded)
        {
            if (sessionTypeEnded == SessionType.Focus)
            {
                StartCoroutine(EnsureGrowthCompleteAndThen(SproutNewBranches));
            }
        }
        #endregion

        #region API
        public bool HasActiveGrowth()
        {
            m_ActiveGrowth.RemoveAll(limb => !limb.IsActivelyGrowing);
            return m_ActiveGrowth.Any();
        }

        float ComputeGrowthAmount()
        {
            return m_GrowthTargets switch
            {
                GrowthTargets.None => 1f,
                GrowthTargets.ExistingLimbs => Timer.Exists ? Timer.ProgressThroughSession : 1f,
                GrowthTargets.PreGameSprouting => ProgressThroughSproutInterval,
                GrowthTargets.NewSprouts => ProgressThroughSproutInterval,
                _ => throw new ArgumentOutOfRangeException($"No handling for {m_GrowthTargets}")
            };
        }

        internal void GrowTree()
        {
            m_GrowthTargets = GrowthTargets.ExistingLimbs;
            Debug.Log($"Growing tree by {GrowthInterval}...");
            m_ActiveGrowth.RemoveAll(limb => !limb.IsActivelyGrowing);
            if (m_ActiveGrowth.Count != 0)
            {
                Debug.LogWarning($"Still have {m_ActiveGrowth.Count} limbs growing. They may get clobbered.");
            }
            foreach (var trunk in ActiveTrunks)
            {
                BeginLimbGrowth(trunk, GrowthInterval);
                BeginAllBranchGrowth(trunk.Branches, GrowthInterval);
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Because we often need to move to the next Pomodoro session before all growth targets from the previous
        /// session are 100% grown, we need to wait a few frames for them to finish up so we can clear out the
        /// m_ActiveGrowths List.
        /// </summary>
        IEnumerator EnsureGrowthCompleteAndThen(System.Action doWhenFinished)
        {
            // This coroutine should in theory always finish in 1-2 frames. If it takes longer something is wrong.
            const int maxLoopsAllowed = 100;
            var numLoopsDone = 0;
            m_GrowthTargets = GrowthTargets.None;
            while (m_ActiveGrowth.Any() && numLoopsDone++ < maxLoopsAllowed)
            {
                DoRoundRobinGrowthUpdates();
                yield return null;
            }

            if (m_ActiveGrowth.Any())
            {
                Debug.LogError("Failed to finish active growth - something is wrong.");
            }
            doWhenFinished.Invoke();
        }
        
        int NextBranchIndex()
        {
            if (m_CurrentBranchPrefabIndex >= BranchPrefabs.Count)
            {
                m_CurrentBranchPrefabIndex = 0;
                return m_CurrentBranchPrefabIndex;
            }

            return m_CurrentBranchPrefabIndex++;
        }

        internal void SproutNewBranches()
        {
            m_GrowthTargets = GrowthTargets.NewSprouts;
            var numGrowingBranches = ActiveTrunks.Sum(trunk => trunk.CountUnfinishedBranches());
            var numAllowedSprouts = MaxAllowedGrowingBranches - numGrowingBranches;
            if (numAllowedSprouts == 0)
            {
                Debug.Log("Too many active branches - not sprouting any new ones.");
                return;
            }

            m_SproutStartTime = Time.timeAsDouble;
            var context = new BranchSproutingContext(numAllowedSprouts);
            foreach (var trunk in ActiveTrunks)
            {
                SproutBranchesOnLimb(context, trunk);
            }
        }

        void BeginAllBranchGrowth(IEnumerable<GrowingTreeLimb> branches, float growthInterval)
        {
            foreach (var branch in branches)
            {
                if (branch.HasAnyBranches)
                {
                    BeginAllBranchGrowth(branch.Branches, growthInterval);
                }
                BeginLimbGrowth(branch, growthInterval);
            }
        }

        void BeginLimbGrowth(GrowingTreeLimb limb, float growthInterval)
        {
            limb.SetGrowthTarget(growthInterval);   
            if (m_ActiveGrowth.Contains(limb))
            {
                Debug.LogWarning($"{limb} was already growing - old targets will be overwritten.");
            }
            else
            {
                m_ActiveGrowth.Add(limb);
            }
        }

        /// <summary>
        /// Find suitable points along each trunk and child branches for new sprouts to start
        /// We define this here instead of inside the GrowingTreeLimb class because the limb
        /// doesn't have enough context. The BonsaiGrower will (eventually) hold the game state
        /// that determines how many branches to grow and by how much
        /// </summary>
        void SproutBranchesOnLimb(BranchSproutingContext context, GrowingTreeLimb limb)
        {
            var childrenFirst = Rand.NextFloat() < 0.75f;

            if (childrenFirst)
            {
                SproutFromChildBranches(context, limb);
            }
            
            var numSprouts = ComputeAmountNewBranches(context, limb);
            if (numSprouts > 0)
            {
                Debug.Log($"Sprouting {numSprouts} new branches off {limb.name}");
            }
            else
            {
                DebugExtras.LogWhenPaused($"Not sprouting anything off {limb.name}", this);
            }
            for (var n = 0; n < numSprouts; ++n)
            {
                var sample = limb.SampleNewBranchLocation(ref Rand);
                var position = limb.transform.TransformPoint(sample.location);
                var tangent = limb.transform.TransformDirection(sample.tangent);
                var orientation = Quaternion.LookRotation(tangent, Vector3.up);
                var prefab = BranchPrefabs[NextBranchIndex()];
                DebugExtras.LogWhenPaused($"Spawning a {prefab.name}", this);
                var sprout = limb.SproutBranch(prefab, position, orientation);
                //sprout.GrowLeaves();
                BeginLimbGrowth(sprout, NewSproutStartingProgress);
                context.AcknowledgeNewSprout();
            }

            if (!childrenFirst)
            {
                SproutFromChildBranches(context, limb);
            }
        }

        /// <summary>
        /// Because the SplineMesh package's MeshBender class does a lot of heavy computations, extruding an arbitrary
        /// number of SplineMeshes along their path every frame gets chuggy very quickly. Instead we opt to only
        /// update a few each frame, amortizing the cost over several frames. This is basically a zero-cost optimization
        /// since the larger growth intervals only become visually noticeable at very low values of MaxGrowStepsPerFrame
        /// </summary>
        void DoRoundRobinGrowthUpdates()
        {
            var limbIndex = m_ActiveGrowthIndex;
            var numLimbsStepped = 0;
            DebugExtras.LogWhenPaused($"Checking list of {m_ActiveGrowth.Count} branches for new growth.", this);
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
                    DebugExtras.LogWhenPaused($"Growing {limb.name}...", this);
                    limb.DoGrowthUpdate(ComputeGrowthAmount());
                    ++numLimbsStepped;
                }
                
                // Check whether limb is still growing after the growth update (that may have been its final one)
                if (limb.IsActivelyGrowing)
                {
                    ++limbIndex;
                }
                else
                {
                    DebugExtras.LogWhenPaused($"{limb.name} is no longer growing. Removing from list.", this);
                    m_ActiveGrowth.RemoveAt(limbIndex);
                }
            }
            DebugExtras.LogWhenPaused($"Grew {numLimbsStepped} limbs this frame.", this);
            m_ActiveGrowthIndex = limbIndex;
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
                SproutBranchesOnLimb(context, branch);
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
        #endregion
        
        #region Debug Helpers
        internal void GrowEntireTrunk(int trunkIndex)
        {
            var trunk = Trunks[trunkIndex];
            m_GrowthTargets = GrowthTargets.None;
            BeginLimbGrowth(trunk, 1f);
        }
        #endregion
    }
}
