using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeftOut.GameJam.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Visuel Cue")]
        [SerializeField] private GameObject dialogue_indicator;

        [Header("Ink JSON")]
        [SerializeField] private TextAsset inkJSON;
        public string currentStoryKnot;

        private void OnMouseUp()
        {
            if (!DialogueManager.GetInstance().dialogueIsPlaying)
            {
                //if currentStoryKnot is THE_END then don't let the player continue the story.
                //this is where we should if the "stop" variable has been reset. 
                if (currentStoryKnot != "THE_END")
                {
                    Debug.Log(this.name);
                    dialogue_indicator.SetActive(false);
                    Debug.Log("Dialogue Triggered");
                    Debug.Log(inkJSON.ToString());
                   // DialogueManager.GetInstance().EnterDialogueMode(inkJSON, this.name);
                   // this.spiritHasSpoken = false;
                }
            }
        }
        private void Awake()
        {
            dialogue_indicator.SetActive(false);
           // spiritHasSpoken = false;
            currentStoryKnot = "Greet";

        }

    }
}
