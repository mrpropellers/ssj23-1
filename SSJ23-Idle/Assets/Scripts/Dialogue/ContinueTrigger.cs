using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LeftOut.GameJam
{
    public class ContinueTrigger : MonoBehaviour
    {
        //public bool continueButton = false;
        
        //private static ContinueTrigger instance;
        // Start is called before the first frame update
        void Start()
        {
            this.gameObject.SetActive(false);
        }

        // Update is called once per frame
       // private void OnMouseDown()
        //{
         //   Debug.Log("Continue Button Pushed");
          //  continueButton = true;
        //}

        //public static ContinueTrigger GetInstance()
        //{
        //    return instance;
       // }
    }
}
