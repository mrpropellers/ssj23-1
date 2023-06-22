using UnityEngine;
using UnityEngine.Splines;

namespace LeftOut.GameJam.Bonsai
{
    [System.Serializable]
    class BonsaiNode
    {
        public Spline ParentSpline { get; private set; }
        public int KnotIndex { get; private set; }
        public int Generation { get; private set; }

        public bool IsLeaf => ParentSpline.Count - 1 == KnotIndex;

        internal BonsaiNode(Spline parentSpline, int knotIndex, int generation)
        {
            ParentSpline = parentSpline;
            KnotIndex = knotIndex;
            Generation = generation;
        }
        
    }
}
