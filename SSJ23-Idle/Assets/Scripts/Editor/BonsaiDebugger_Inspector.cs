using Codice.CM.Common.Replication;
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

        VisualElement RowElement()
        {
            var row = new VisualElement();
            row.style.flexDirection = new StyleEnum<FlexDirection>(FlexDirection.Row);
            row.style.justifyContent = new StyleEnum<Justify>(Justify.Center);
            return row;
        }

        VisualElement HalfButton(System.Action clickEvent, string text)
        {
            var button = new Button(clickEvent)
            {
                text = text
            };
            button.style.width = new Length(50, LengthUnit.Percent);
            return button;
        }

        public override VisualElement CreateInspectorGUI()
        {
            var bonsaiDebugger = (BonsaiDebugger)target;

            var showTrunks = HalfButton(bonsaiDebugger.ShowTrunks, "Show Trunks");
            var hideTrunks = HalfButton(bonsaiDebugger.HideTrunks, "Hide Trunks");
            var showHideRow = RowElement();
            showHideRow.Add(showTrunks);
            showHideRow.Add(hideTrunks);
            
            var grow = new Button(bonsaiDebugger.GrowOneGeneration)
            {
                text = "Grow One Generation",
            };

            var initRand = HalfButton(bonsaiDebugger.InitRandom, "Initialize Rand");
            var testRand = HalfButton(bonsaiDebugger.VerifyNormalSampling, "Test Rand");
            var randRow = RowElement();
            randRow.Add(initRand);
            randRow.Add(testRand);

            var gui = new VisualElement();
            gui.Add(new IMGUIContainer(OnInspectorGUI));
            var buttons = new VisualElement();
            buttons.style.marginLeft = new StyleLength(new Length(2.5f, LengthUnit.Percent));
            buttons.style.marginRight = new StyleLength(new Length(2.5f, LengthUnit.Percent));
            buttons.Add(randRow);
            buttons.Add(showHideRow);
            buttons.Add(grow);
            gui.Add(buttons);
            return gui;
        }
    }
}
