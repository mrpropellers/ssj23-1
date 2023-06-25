// Based on SplineMesh.ExampleGrowingRoot

using System;
using System.Collections.Generic;
using System.Linq;
using LeftOut.Extensions;
using UnityEngine;
using SplineMesh;
using Random = Unity.Mathematics.Random;
using LeftOut.Toolkit;

namespace LeftOut.GameJam.Bonsai
{
    [RequireComponent(typeof(Spline))]
    //[ExecuteAlways]
    public class GrowingTreeLimb : MonoBehaviour
    {
        // Scale can't start at zero for some reason, so need to set an initial size
        const float k_InitialScale = 0.01f;

        internal GrowthTracker GrowthState { get; private set; }

        GameObject m_Generated;
        Spline m_Spline;
        MeshBender m_MeshBender;
        MeshRenderer m_Renderer;
        float m_CurrentGrowthProgress;
        List<GrowingTreeLimb> m_Branches;

        [SerializeField]
        Leaves m_Leaves;
        [SerializeField]
        Mesh m_Mesh;
        [SerializeField]
        Material m_Material;
        [SerializeField]
        Vector3 m_MeshRotation;
        [SerializeField]
        Vector3 m_MeshScale;
        [SerializeField]
        float m_StartScale = 1f;

        internal int Generation = -1;

        // NOTE: This does not check whether those branches have been destroyed since last access
        internal bool HasAnyBranches => m_Branches.Any();
        internal bool IsActivelyGrowing => 
            Math.Abs(m_CurrentGrowthProgress - GrowthState.FinalProgress) < float.Epsilon;
        bool HasLeaves => m_Leaves != null;
        bool ShouldGrowLeaves => !IsActivelyGrowing && HasLeaves &&
            (float)m_Leaves.TargetGrowthStage / m_Leaves.NumStages < GrowthState.FinalProgress;
        
        internal bool IsInitialized { get; private set; }
        internal bool IsFullyGrown => !IsActivelyGrowing 
            && Math.Abs(m_CurrentGrowthProgress - 1f) < float.Epsilon;

        // value in units of distance indicating how far along the path the limb has grown
        internal float CurrentLength => m_Spline.Length * m_CurrentGrowthProgress;
        
        //internal GrowingTreeLimb Parent { get; private set; }
        internal IEnumerable<GrowingTreeLimb> Branches
        {
            get
            {
                for (var i = m_Branches.Count - 1; i >= 0; --i)
                {
                    var branch = m_Branches[i];
                    if (branch == null)
                    {
                        m_Branches.RemoveAt(i);
                    }
                    else
                    {
                        yield return branch;
                    }
                }
            }
        }

        public void Prune()
        {
            // Because limbs in Branches are also children of this branch, we probably don't need to recursively
            // Prune all of them... Anything we do to the Parent will carry through...
            // foreach (var limb in Branches)
            // {
            //     limb.Prune();
            // }
            Destroy(gameObject);
        }

        void Awake()
        {
            GrowthState = new GrowthTracker(0, 0, 0);
        }

        void Start()
        {
            if (IsInitialized)
            {
                Debug.LogWarning($"{name} was initialized before Start was called - something might be wrong.");
            }

            Init();
        }

        void Init()
        {
            DebugExtras.LogWhenPaused("Initializing.", this);
            if (m_Branches is { Count: > 0 })
            {
                Debug.LogWarning($"{name} has active Branches - you'll need to manually delete them.");
            }
            m_Branches = new List<GrowingTreeLimb>();
            var generatedName = "generated by " + GetType().Name;
            var generatedTransform = transform.Find(generatedName);
            m_Generated = generatedTransform != null
                ? generatedTransform.gameObject
                : UOUtility.Create(generatedName, gameObject,
                    typeof(MeshFilter),
                    typeof(MeshRenderer),
                    typeof(MeshBender));

            m_Renderer = m_Generated.GetComponent<MeshRenderer>();
            m_Renderer.material = m_Material;

            m_MeshBender = m_Generated.GetComponent<MeshBender>();
            m_Spline = GetComponent<Spline>();

            m_MeshBender.Source = SourceMesh.Build(m_Mesh).Rotate(Quaternion.Euler(m_MeshRotation)).Scale(m_MeshScale);
            m_MeshBender.Mode = MeshBender.FillingMode.StretchToInterval;
            //m_MeshBender.SetInterval(m_Spline, 0, k_InitialScale);
            GrowthState = new GrowthTracker(0, k_InitialScale, 0);
            IsInitialized = true;
            //DoGrowthUpdate();
        }

        void Update()
        {
            if (!IsInitialized)
            {
                return;
            }
            if (IsFullyGrown)
            {
                Debug.Log($"This branch is fully grown. Disabling self.");
                enabled = false;
                return;
            }
        }

        internal void Reset()
        {
            Init();
        }

        internal void ShuffleBranchOrder() => m_Branches.Shuffle();

        internal int CountGrowingBranches()
        {
            // Add self to count if branch is not fully grown
            var count = IsFullyGrown ? 0 : 1;
            
            foreach (var branch in Branches)
            {
                count += branch.CountGrowingBranches();
            }

            return count;
        }

        internal void SetGrowthTarget(float progressInterval, float timeInterval)
        {
            if (IsActivelyGrowing)
            {
                Debug.LogWarning("Called grow again before previous finished. Overwriting.");
            }

            GrowthState = new GrowthTracker(
                m_CurrentGrowthProgress,
                m_CurrentGrowthProgress + progressInterval,
                timeInterval);
        }

        internal CurveSample SampleNewBranchLocation(ref Random random)
        {
            var distance = random.NextFloat((float)CurrentLength);
            return m_Spline.GetSampleAtDistance(distance);
        }

        internal GrowingTreeLimb SproutBranch(GrowingTreeLimb prefab, Vector3 position, Quaternion orientation)
        {
            var sprout =
                Instantiate(prefab, position, orientation, transform);
            sprout.Generation = Generation + 1;
            m_Branches.Add(sprout);
            return sprout;
        }

        internal void DoGrowthUpdate()
        {
            if (!IsInitialized)
            {
                Debug.LogError("Tried to grow before initializing. Initializing now (this may break).");
                Init();
                return;
            }
            
            var progress = GrowthState.CurrentProgressTarget;
            var nodeDistance = 0f;
            for (var i = 0; i < m_Spline.nodes.Count; ++i)
            {
                var node = m_Spline.nodes[i];
                var nodeDistanceRate = nodeDistance / m_Spline.Length;
                var nodeScale = m_StartScale * (progress - nodeDistanceRate);
                node.Scale = new Vector2(nodeScale, nodeScale);
                if (i < m_Spline.curves.Count)
                { 
                    nodeDistance += m_Spline.curves[i].Length;
                }
            }

            if (m_Generated != null)
            {
                m_MeshBender.SetInterval(m_Spline, 0, m_Spline.Length * progress);
                m_MeshBender.ComputeIfNeeded();
            }

            m_CurrentGrowthProgress = progress;
            if (HasLeaves)
            {
                m_Leaves.transform.localPosition = m_Spline.GetSampleAtDistance(CurrentLength).location;
            }
            if (ShouldGrowLeaves)
            {
                m_Leaves.GrowNextStage();
            }
        }
    }
}
