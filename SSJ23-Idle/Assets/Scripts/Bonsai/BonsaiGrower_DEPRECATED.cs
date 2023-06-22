using System;
using System.Collections.Generic;
using LeftOut.Extensions.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace LeftOut.GameJam.Bonsai
{
    [Obsolete]
    [RequireComponent(typeof(SplineContainer))]
    class BonsaiGrower_DEPRECATED : MonoBehaviour
    {
        [SerializeField]
        float GrowthLength = 0.5f;
        [SerializeField, Range(5f, 90f)]
        float InPlaneStandardDeviation = 30f;
        [SerializeField, Range(0f, 90f)]
        float InPlaneLeafMaxDeviation = 45f;
        [SerializeField, Range(0f, 30f)]
        float OutPlaneStandardDeviation = 10f;
        [SerializeField, Range(0f, 45f)]
        float OutPlaneMaxDeviation = 15f;

        // Make this internal so we can test it from the debugger
        internal Random Rand;
        SplineContainer m_SplineContainer;
        List<BonsaiNode> m_BonsaiNodes;

        void Awake()
        {
            m_BonsaiNodes = new List<BonsaiNode>();
            m_SplineContainer = GetComponent<SplineContainer>();
            Rand = new Random();
        }

        void Start()
        {
            Rand.InitState((uint)DateTime.Now.Ticks);
            m_BonsaiNodes.Clear();
            var rootSpline = m_SplineContainer.Spline;
            for(var i = 0; i < m_SplineContainer.Spline.Count; ++i)
            {
                m_BonsaiNodes.Add(new BonsaiNode(rootSpline, i, 0));
            }
        }

        internal void GrowGeneration()
        {
            Debug.Log("Growing...");
            var splineTf = m_SplineContainer.transform;
            // Because we're adding new Nodes to the collection as we iterate, need to check the current count
            // and only iterate over the ones that existed before we started adding more
            var numCurrentNodes = m_BonsaiNodes.Count;
            for(var i = 0; i < numCurrentNodes; ++i)
            {
                var node = m_BonsaiNodes[i];
                if (node.IsLeaf)
                {
                    var spline = node.ParentSpline;
                    var knot = spline[node.KnotIndex];
                    var forwardWorld = splineTf.FromLocalToWorld(math.forward(knot.Rotation));
                    var inPlaneRotation = Rand.SampleGaussianClamped(
                        0f, InPlaneStandardDeviation, InPlaneLeafMaxDeviation);
                    var outPlaneRotation = Rand.SampleGaussianClamped(
                        0f, OutPlaneStandardDeviation, OutPlaneMaxDeviation);
                    var eulerDegrees = new float3(0f, outPlaneRotation, inPlaneRotation);
                    var eulerRadians = math.radians(eulerDegrees);
                    var rotation = quaternion.Euler(eulerRadians);
                    var growthDirection = splineTf.FromWorldToLocal(math.rotate(rotation, forwardWorld));
                    
                    var newKnot = new BezierKnot(knot.Position + growthDirection * GrowthLength)
                    {
                        Rotation = math.mul(rotation, knot.Rotation)
                    };
                    spline.Add(newKnot);
                    m_BonsaiNodes.Add(new BonsaiNode(spline, spline.Count - 1, node.Generation + 1));
                }
            }
        }
    }
}
