using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SpiritBody : MonoBehaviour
{
    public GameObject[] spiritLimbs;
    public GameObject spiritLeftArm;
    public GameObject spiritRightArm;
    public GameObject spiritLeftLeg;
    public GameObject spiritRightLeg;
    public GameObject spiritRound;
    public GameObject spiritReverseRound;

    SpriteRenderer spiritColor;


    void Awake()
    {
        SpawnLimbs();
    }

    void SpawnLimbs()
    {
        foreach (GameObject child in spiritLimbs)
        {
            float random = Random.Range(0.0f, 100.0f);

            if (random >= 50.0f)
            {
                if (child.tag == "LeftArm") 
                { 
                    GameObject limb = Instantiate(spiritLeftArm, transform);
                    limb.transform.position = child.transform.position;
                    /*spiritColor = limb.transform.GetChild(0).GetComponent<SpriteRenderer>();
                    spiritColor.color = GetComponentInParent<SpriteRenderer>().color;
                    Debug.Log(spiritColor.color);*/
                } else if (child.tag == "RightArm")
                {
                    GameObject limb = Instantiate(spiritRightArm, transform);
                    limb.transform.position = child.transform.position;
                } else if (child.tag == "LeftLeg")
                {
                    GameObject limb = Instantiate(spiritLeftLeg, transform);
                    limb.transform.position = child.transform.position;
                } else if (child.tag == "RightLeg")
                {
                    GameObject limb = Instantiate(spiritRightLeg, transform);
                    limb.transform.position = child.transform.position;
                }
                if (child.tag == "Head")
                {
                    GameObject limb = Instantiate(spiritRound, transform);
                    limb.transform.position = child.transform.position;
                }
                if (child.tag == "Backend")
                {
                    GameObject limb = Instantiate(spiritReverseRound, transform);
                    limb.transform.position = child.transform.position;
                }
            }
        }
    }
}
