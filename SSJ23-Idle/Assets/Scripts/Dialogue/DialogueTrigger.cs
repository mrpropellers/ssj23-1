using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeftOut.GameJam
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Visuel Cue")]
        [SerializeField] private GameObject dialogue_indicator;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;


        private void OnMouseDown()
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying) { 
            dialogue_indicator.SetActive(false);
            Debug.Log("Dialogue Triggered");
            DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }
        }
        private void Awake()
        {
            dialogue_indicator.SetActive(true);

        }

    }
}
