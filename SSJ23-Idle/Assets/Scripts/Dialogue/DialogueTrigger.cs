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
            dialogue_indicator.SetActive(false);
            Debug.Log(inkJSON.text);

        }
        private void Awake()
        {
            dialogue_indicator.SetActive(true);

        }

    }
}
