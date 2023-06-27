using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace LeftOut.GameJam
{
    public class MainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            SceneManager.LoadScene("MainScene");
            //Debug.Log("Pressed new scene");
        }

        public void EndGame()
        {
            Application.Quit();
            //Debug.Log("pressed exit");
        }
    }
}
