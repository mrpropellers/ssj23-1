using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRotateDots : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5.0f;
    [SerializeField] bool spin = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spin)
        {
            gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        else
        {
            gameObject.transform.Rotate(0, 0, 0);
        }
    }

    public void StartStopSpin()
    {
        if (spin)
        {
            spin = false;
        }
        else
        {
            spin = true;
        }
    }
}
