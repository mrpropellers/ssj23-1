using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using Ink.Runtime;
using UnityEngine.InputSystem;

namespace LeftOut.GameJam
{
    public class DialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private GameObject continueStoryButton;

        [Header("Choices UI")]
        [SerializeField] private GameObject[] choices;
        [SerializeField] private GameObject choiceManager;
        private TextMeshProUGUI[] choicesText;
        private int choiceIndex;

        private Story currentStory;

        private static DialogueManager instance;

        public bool dialogueIsPlaying {  get; private set; }
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
            
            //get all the choices text
            choicesText = new TextMeshProUGUI[choices.Length];
            int index = 0;
            foreach (GameObject choice in choices)
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }
        }

        public void EnterDialogueMode(TextAsset inkJSON) 
        {
            currentStory = new Story(inkJSON.text);
            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);

            ContinueStory();

        }

        private void Update()
        {
            //return immediately if no dialogue is playing
            if (!dialogueIsPlaying) { return; }
            if(continueStoryButton.activeSelf == true)
            {
                Debug.Log("Registered continue button as true");
                continueStoryButton.SetActive(false);
                ContinueStory();
            }

        }

        private void ContinueStory()
        {
            if (currentStory.canContinue)
            {
                Debug.Log("current story can continue");
                dialogueText.text = currentStory.Continue();
                //display choices if they exist
                DisplayChoices();
            }
            else
            {
                ExitDialogueMode();
            }
        }

        private void ExitDialogueMode()
        {
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";
        }

        public static DialogueManager GetInstance() { return instance; }

        private void DisplayChoices()
        {
            List<Choice> currentChoices = currentStory.currentChoices;

            if(currentChoices.Count > choices.Length)
            {
                Debug.LogError("More choices than UI Can support.");
            }
            int index = 0;
            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choicesText[index].text = choice.text;
                index++;
            }
            for (int i = index; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);
            }
        }

        public void MakeChoice(int buttonChoiceIndex)
        {
            currentStory.ChooseChoiceIndex(buttonChoiceIndex);
            ContinueStory();
        }
    }
}
