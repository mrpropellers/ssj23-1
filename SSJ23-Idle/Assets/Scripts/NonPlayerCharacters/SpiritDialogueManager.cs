using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

//using Ink.Parsed;

namespace LeftOut.GameJam.NonPlayerCharacters
{
    public class SpiritDialogueManager : MonoBehaviour
    {
        [Header("Dialogue UI")]
        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private GameObject continueStoryTracker;
        [SerializeField] private GameObject continueStoryButton;

        [Header("Choices UI")]
        [SerializeField] private GameObject[] choices;
        [SerializeField] private GameObject choiceManager;
        private TextMeshProUGUI[] choicesText;
        private int choiceIndex;
        private bool makingChoices;

        private Story currentStory;

        private static SpiritDialogueManager instance;

        private GameObject npcObject;

        public bool dialogueIsPlaying { get; private set; }
        private void Awake()
        {
            if (instance != null)
            {
                UnityEngine.Debug.LogWarning("Found more than one dialogue manager in the scene.");
            }
            instance = this;
        }

        private void Start()
        {
            dialogueIsPlaying = false;
            makingChoices = false;
            dialoguePanel.SetActive(false);
            continueStoryButton.SetActive(false);
            //  knotProgress = ["one", "two" ];

            //get all the choices text
            choicesText = new TextMeshProUGUI[choices.Length];
            int index = 0;
            foreach (GameObject choice in choices)
            {
                choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
                index++;
            }
        }

        public void EnterDialogueMode(TextAsset inkJSON, string npcSpeakingName, string npcCurrentKnot)
        {
            currentStory = new Story(inkJSON.text); //generate the inky story object
            npcObject = GameObject.Find(npcSpeakingName); //find the NPC object that triggered the dialogue
            //if currentStoryKnot is NOT the greeting knot, then load where the player last left off.
            if (npcCurrentKnot != "Greet")
            {
                currentStory.ChoosePathString(npcCurrentKnot);
            }

            dialogueIsPlaying = true;
            dialoguePanel.SetActive(true);
            continueStoryButton.SetActive(true);
            ContinueStory();

        }

        private void Update()
        {
            //return immediately if no dialogue is playing
            if (!dialogueIsPlaying) { return; }
            if (continueStoryTracker.activeSelf == true || makingChoices == true)
            {
                continueStoryTracker.SetActive(false);
                ContinueStory();
            }

        }

        private void ContinueStory()
        {

            if (currentStory.canContinue)
            {

                dialogueText.text = currentStory.Continue();

                //immediately after story has continued, update the currentStoryKnot by pulling the variable from the ink.
                //this allows us to direct the flow of the story within the ink while saving the future story reference point on the npc object
                npcObject.GetComponent<Spirit>().currentStoryKnot = currentStory.variablesState["currentStoryKnot"].ToString();

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
            //turn on the dialogue indicator if there's more dialogue for that NPC
            if (npcObject.GetComponent<Spirit>().currentStoryKnot != "THE_END") { reactivateDialogueIndicator(); }

            //clean up the dialogue box
            makingChoices = false;
            dialogueIsPlaying = false;
            dialoguePanel.SetActive(false);
            dialogueText.text = "";

        }

        public static SpiritDialogueManager GetInstance() { return instance; }

        private void DisplayChoices()
        {
            List<Choice> currentChoices = currentStory.currentChoices;

            if (currentChoices.Count > choices.Length)
            {
                UnityEngine.Debug.LogError("More choices than UI Can support.");
            }

            //turn off the continue button if there are choices to display

            if (currentChoices.Count > 0)
            {
                makingChoices = true;
                continueStoryButton.SetActive(false);
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
            makingChoices = false;

        }
        private void reactivateDialogueIndicator()
        {
            npcObject.transform.GetChild(0).gameObject.SetActive(true);
        }

        public void MakeChoice(int buttonChoiceIndex)
        {
            //when a choice button has been clicked, update the story, turn on the continue button and then continue
            currentStory.ChooseChoiceIndex(buttonChoiceIndex);
            continueStoryButton.SetActive(true);
            ContinueStory();

        }
    }
}

