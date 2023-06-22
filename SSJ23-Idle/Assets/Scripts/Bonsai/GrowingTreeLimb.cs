// Based on SplineMesh.ExampleGrowingRoot

using System;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace LeftOut.GameJam.Bonsai
{
    [RequireComponent(typeof(Spline))]
    [ExecuteAlways]
    public class GrowingTreeLimb : MonoBehaviour
    {
        // Scale can't start at zero for some reason, so need to set an initial size
        const float k_InitialScale = 0.01f;

        GameObject m_Generated;
        Spline m_Spline;
        MeshBender m_MeshBender;
        MeshRenderer m_Renderer;
        float m_GrowthProgress;
        float m_GrowthTarget;
        double m_LastGrowStartTime;
        double m_GrowthPerSecond;

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
        //internal GrowingTreeLimb Parent { get; private set; }
        internal List<GrowingTreeLimb> Branches { get; private set; }
        internal bool IsInitialized { get; private set; }
        // Returns true IFF the branch is currently in a growth coroutine - if you want to know whether a branch
        // CAN grow, check IsFullyGrown
        internal bool IsCurrentlyGrowing => Math.Abs(m_GrowthProgress - m_GrowthTarget) > float.Epsilon;
        internal bool IsFullyGrown => Math.Abs(m_GrowthProgress - 1f) < float.Epsilon;

        // 0-1 value indicating how far along the path the limb has grown
        internal double CurrentGrowthProgress => m_GrowthProgress;
        // value in units of distance indicating how far along the path the limb has grown
        internal double CurrentLength => m_Spline.Length * m_GrowthProgress;

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
            Debug.Log("Initializing trunk.");
            if (Branches is { Count: > 0 })
            {
                Debug.LogWarning($"{name} has active Branches - you'll need to manually delete them.");
            }
            Branches = new List<GrowingTreeLimb>();
            m_GrowthProgress = k_InitialScale;
            m_GrowthTarget = m_GrowthProgress;
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
            Contort(k_InitialScale);
            IsInitialized = true;
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

            if (IsCurrentlyGrowing)
            {
                DoGrowUpdate();
            }
        }

        internal void Reset()
        {
            Init();
        }

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
            Branches.Add(sprout);
            return sprout;
        }

        internal void GrowByProgress(double timeInterval, float progressDelta)
        {
            if (IsCurrentlyGrowing)
            {
                Debug.LogError("You are calling Grow too fast. Wait until previous has finished.");
                return;
            }

            if (!IsFullyGrown)
            {
                m_LastGrowStartTime = Time.timeAsDouble;
                m_GrowthPerSecond = 1.0 / timeInterval;

                if (progressDelta + m_GrowthTarget > 1f)
                {
                    Debug.Log("Growth target is flowing over; clamping.");
                }

                m_GrowthTarget = math.clamp(m_GrowthTarget + progressDelta, m_GrowthProgress, 1.0f);
            }
            
            // No reason to check for branches if this instance is brand new
            if (!IsInitialized)
            {
                return;
            }
            
            foreach (var branch in Branches)
            {
                branch.GrowByProgress(timeInterval, progressDelta);
            }
        }

        void DoGrowUpdate()
        {
            if (!IsInitialized)
            {
                Debug.LogWarning("Tried to grow before initializing. Initializing now (this may break).");
                Init();
            }

            var timeElapsed = Time.timeAsDouble - m_LastGrowStartTime;
            var t = math.clamp(timeElapsed * m_GrowthPerSecond, 0.0, 1.0);
            var currentProgress = math.lerp(m_GrowthProgress, m_GrowthTarget, t);
            Contort((float)currentProgress);
            if (Math.Abs(t - 1.0) < float.Epsilon)
            {
                m_GrowthProgress = m_GrowthTarget;
            }
        }

        void Contort(float t)
        {
            var nodeDistance = 0f;

            for (var i = 0; i < m_Spline.nodes.Count; ++i)
            {
                var node = m_Spline.nodes[i];
                var nodeDistanceRate = nodeDistance / m_Spline.Length;
                var nodeScale = m_StartScale * (t - nodeDistanceRate);
                node.Scale = new Vector2(nodeScale, nodeScale);
                if (i < m_Spline.curves.Count)
                { 
                    nodeDistance += m_Spline.curves[i].Length;
                }
            }

            if (m_Generated != null)
            {
                m_MeshBender.SetInterval(m_Spline, 0, m_Spline.Length * t);
                m_MeshBender.ComputeIfNeeded();
            }

            // if (m_IsHidingMesh)
            // {
            //     m_Renderer.enabled = true;
            //     m_IsHidingMesh = false;
            // }
        }
    }
}
