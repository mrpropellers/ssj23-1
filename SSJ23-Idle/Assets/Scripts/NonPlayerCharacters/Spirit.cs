using LeftOut.GameJam.Dialogue;
using System;
using UnityEngine;
using UnityEngine.Events;

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
    void Start()
    {
        this.gameObject.SetActive(false);
        this.isInScene = false;
        this.spiritHasSpoken = false;
        dialogue_indicator.SetActive(false);
        spiritHasSpoken = false;
        currentStoryKnot = "Greet";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        // TODO: Replace with interaction handling
        SpiritVocalized?.Invoke(SpiritVoiceType);
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
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON, this.name);
                this.spiritHasSpoken = false;
            }
        }

    }
}
