using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

namespace LeftOut.GameJam
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;

        private Story currentStory;

        private static DialogueManager instance;

        private bool dialogueIsPlaying;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Found more than one dialogue manager in the scene.");
            }
            instance = this;
        }

        private void Start()
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
        }

        public void EnterDialogueMode(TextAsset inkJSON) 
        {
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);

            if (currentStory.canContinue)
            {
                dialogueText.text = currentStory.Continue();
            }
            else
            {
                ExitDialogueMode();
            }

        }

        private void Update()
        {
            if (!dialogueIsPlaying) { return; }
            if (true)
            {
                
            }
        }

        private void ExitDialogueMode()
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
        }


        public static DialogueManager GetInstance() { return instance; }
    }
}
