using UnityEditor;
using UnityEngine;
using LeftOut.GameJam.Bonsai;
using UnityEngine.UIElements;

namespace LeftOut.GameJam.Editor
{
    [CustomEditor(typeof(BonsaiDebugger))]
    public class BonsaiDebugger_Inspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI() => DrawDefaultInspector();
        
        public override VisualElement CreateInspectorGUI()
        {
            var bonsaiDebugger = (BonsaiDebugger)target;
            var grow = new Button(bonsaiDebugger.GrowOneGeneration)
            {
                text = "Grow One Generation",
            };

            var bonsaiGrower = bonsaiDebugger.GetComponent<BonsaiGrower>();
            var initRand = new Button(() => bonsaiGrower.Rand.InitState(bonsaiDebugger.RandSeed))
            {
                text = "Initialize Rand"
            };
            var testRand = new Button(bonsaiDebugger.VerifyNormalSampling)
            {
                text = "Test Rand"
            };

            var gui = new VisualElement();
            gui.Add(new IMGUIContainer(OnInspectorGUI));
            gui.Add(initRand);
            gui.Add(testRand);
            gui.Add(grow);
            return gui;
        }
    }
}
