using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameMenuUI : MonoBehaviour
{

    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        Button buttonResume = root.Q<Button>("ButtonResume");
        Button buttonQuit = root.Q<Button>("ButtonQuit");

        //buttonResume.clicked += () => buttonResume.style.opacity = 1f;

        buttonResume.RegisterCallback<MouseOverEvent>((type) =>
        {
            buttonResume.style.opacity = 1f;
        });

        buttonQuit.RegisterCallback<MouseOverEvent>((type) =>
        {
            buttonQuit.style.opacity = 1f;
        });

        buttonResume.RegisterCallback<MouseOutEvent>((type) =>
        {
            buttonResume.style.opacity = .7f;
        });

        buttonQuit.RegisterCallback<MouseOutEvent>((type) =>
        {
            buttonQuit.style.opacity = .7f;
        });
    }
}
