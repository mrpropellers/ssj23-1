using System;
using LeftOut.GameJam.Clock;
using UnityEngine;
using UnityEngine.UI;

namespace LeftOut.GameJam.UserInterface
{

    public class PlayPauseIconSetter : MonoBehaviour
    {
        // Indices
        // Play: 0
        // Pause: 1
        [SerializeField]
        Sprite[] buttonSprites;
        int m_CurrentIndex = 0;
        Image buttonImage;

        void Awake()
        {
            buttonImage = GetComponent<Image>();
            buttonImage.sprite = buttonSprites[m_CurrentIndex];
        }

        void Start()
        {
            if (!PomoTimer.Exists)
            {
                Debug.LogWarning("Couldn't find a Timer to check - disabling self.");
                enabled = false;
            }
        }

        void Update()
        {

            if (PomoTimer.IsPlaying && m_CurrentIndex == 0) 
            {
                SetSprite(1);
            }
            else if (!PomoTimer.IsPlaying && m_CurrentIndex == 1)
            {
                SetSprite(0);
            }

        }

        void SetSprite(int index)
        {
            buttonImage.sprite = buttonSprites[index];
            m_CurrentIndex = index;
        }
    }
}