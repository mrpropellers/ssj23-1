using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace LeftOut.GameJam
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI m_StartText;
        [SerializeField]
        List<Button> m_MainButtons;

        public void StartGame()
        {
            m_StartText.text = "Loading...";
            foreach (var button in m_MainButtons)
            {
                button.interactable = false;
            }
            SceneManager.LoadSceneAsync("MainScene");
            //Debug.Log("Pressed new scene");
        }

        public void EndGame()
        {
            Application.Quit();
            //Debug.Log("pressed exit");
        }
    }
}
