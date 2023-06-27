using LeftOut.GameJam.Dialogue;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace LeftOut.GameJam.NonPlayerCharacters
{
public class Spirit : MonoBehaviour
{

    [field: SerializeField, Range(0, 3)]
    
    public int SpiritVoiceType { get; private set; }
    [field: SerializeField]
    public UnityEvent<int> SpiritVocalized { get; private set; }

    [Header("Is in Scene")]
    public bool isInScene;

    [Header("Visuel Cue")]
    [SerializeField] private GameObject dialogue_indicator;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    public string currentStoryKnot;

    [SerializeField] public bool spiritHasSpoken;

    // Start is called before the first frame update
    void Awake()
    {
        gameObject.SetActive(false);
        isInScene = false;
        spiritHasSpoken = false;
        dialogue_indicator.SetActive(false);
        spiritHasSpoken = true;
        currentStoryKnot = "Greet";
    }

        private void Update()
        {
            if(isInScene == true && spiritHasSpoken == true) { 
                dialogue_indicator.SetActive(false);
            } else
            {
                dialogue_indicator.SetActive(true);
            }
        }

        void OnMouseUp()
    {
        SpiritVocalized?.Invoke(SpiritVoiceType);
        if (!SpiritDialogueManager.GetInstance().dialogueIsPlaying && spiritHasSpoken == false)
        {
            //if currentStoryKnot is THE_END then don't let the player continue the story.
            //this is where we should if the "stop" variable has been reset. 
            if (currentStoryKnot != "THE_END")
            {
                Debug.Log(this.name);
                dialogue_indicator.SetActive(false);
                Debug.Log("Dialogue Triggered");
                spiritHasSpoken = true;
                SpiritDialogueManager.GetInstance().EnterDialogueMode(inkJSON, this.name, currentStoryKnot);
                
            }
        }

    }
}
}