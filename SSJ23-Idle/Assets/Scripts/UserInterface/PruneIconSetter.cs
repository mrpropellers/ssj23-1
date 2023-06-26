using System;
using LeftOut.GameJam.Clock;
using LeftOut.GameJam.PlayerInteractions;
using UnityEngine;
using UnityEngine.UI;

namespace LeftOut.GameJam.UserInterface
{

    public class PruneIconSetter : MonoBehaviour
    {
        // Indices
        // Inactive: 0
        // Active: 1
        [SerializeField]
        Sprite[] buttonSprites;
        [SerializeField]
        InteractionExecutor Interactions;
        
        int m_CurrentIndex = 0;
        Image buttonImage;

        void Awake()
        {
            buttonImage = GetComponent<Image>();
            buttonImage.sprite = buttonSprites[m_CurrentIndex];
        }

        void Start()
        {
            if (Interactions == null)
            {
                Debug.LogWarning($"No reference to {nameof(InteractionExecutor)} set - disabling self.");
                enabled = false;
            }
        }

        void Update()
        {
            if (Interactions.IsInPruneMode && m_CurrentIndex == 0)
            {
                SetSprite(1);
            }
            else if (!Interactions.IsInPruneMode && m_CurrentIndex == 1)
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