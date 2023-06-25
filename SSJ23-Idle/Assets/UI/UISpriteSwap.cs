using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteSwap : MonoBehaviour
{
    [SerializeField] Sprite[] buttonSprites;
    Image buttonImage;
    bool enabled = false;

    // Start is called before the first frame update
    void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SpriteSwap()
    {
        if (!enabled)
        {
            enabled = true;
            buttonImage.sprite = buttonSprites[1];
        }
        else
        {
            buttonImage.sprite = buttonSprites[0];
            enabled = false;
        }
    }
}
