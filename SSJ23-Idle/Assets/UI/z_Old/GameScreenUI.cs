using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameScreenUI : MonoBehaviour
{
    [SerializeField] Sprite[] pruneButtonState;
    [SerializeField] Sprite[] playButtonState;
    VisualElement root;
    Button buttonPrune;
    Button buttonMenu;
    Button buttonPlay;
    Button buttonRestart;
    bool pruneMode = false;
    bool playMode = false;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        buttonPrune = root.Q<Button>("ButtonPrune");
        buttonMenu = root.Q<Button>("ButtonMenu");
        buttonPlay = root.Q<Button>("ButtonPlay");
        buttonRestart = root.Q<Button>("ButtonRestart");

        buttonPrune.clicked += () => ChangeButtonBackground();

        buttonPlay.clicked += () => PlayPause();

        buttonPrune.RegisterCallback<MouseOverEvent>((type) =>
        {
            buttonPrune.style.opacity = 0.8f;
        });

        buttonMenu.RegisterCallback<MouseOverEvent>((type) =>
        {
            buttonMenu.style.opacity = 0.8f;
        });

        buttonPrune.RegisterCallback<MouseOutEvent>((type) =>
        {
            buttonPrune.style.opacity = .4f;
        });

        buttonMenu.RegisterCallback<MouseOutEvent>((type) =>
        {
            buttonMenu.style.opacity = .4f;
        });

        buttonPlay.RegisterCallback<MouseOverEvent>((type) =>
        {
            buttonPlay.style.opacity = 1f;
        });

        buttonRestart.RegisterCallback<MouseOverEvent>((type) =>
        {
            buttonRestart.style.opacity = 1f;
        });

        buttonPlay.RegisterCallback<MouseOutEvent>((type) =>
        {
            buttonPlay.style.opacity = .6f;
        });

        buttonRestart.RegisterCallback<MouseOutEvent>((type) =>
        {
            buttonRestart.style.opacity = .6f;
        });
    }

    private void PlayPause()
    {
        if (!playMode)
        {
            buttonPlay.style.backgroundImage = new StyleBackground(playButtonState[1]);
            playMode = true;
        }
        else
        {
            buttonPlay.style.backgroundImage = new StyleBackground(playButtonState[0]);
            playMode = false;
        }
    }

    void ChangeButtonBackground()
    {
        if (!pruneMode)
        {
            buttonPrune.style.backgroundImage = new StyleBackground(pruneButtonState[1]);
            pruneMode = true;
        }
        else
        {
            buttonPrune.style.backgroundImage = new StyleBackground(pruneButtonState[0]);
            pruneMode = false;
        }
    }
}