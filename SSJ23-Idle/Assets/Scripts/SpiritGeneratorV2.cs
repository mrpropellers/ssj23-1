using Codice.CM.Common.Replication;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritGeneratorV2 : MonoBehaviour
{
    public GameObject[] spiritHeads;
    public GameObject[] spiritBodies;
    public GameObject[] spiritLeftUpperArms;
    public GameObject[] spiritRightUpperArms;
    public GameObject[] spiritLeftUpperHands;
    public GameObject[] spiritRightUpperHands;
    public GameObject[] spiritLeftLowerArms;
    public GameObject[] spiritRightLowerArms;
    public GameObject[] spiritLeftLowerHands;
    public GameObject[] spiritRightLowerHands;
    public GameObject[] spiritLeftLegs;
    public GameObject[] spiritRightLegs;
    public GameObject[] spiritLeftThinFeet;
    public GameObject[] spiritLeftWideFeet;
    public GameObject[] spiritRightThinFeet;
    public GameObject[] spiritRightWideFeet;
    public GameObject[] spiritTail;

    int oneAndOneHundred = 100;
    int symmetryValue;

    private void Awake()
    {
        SpiritGenerator(spiritHeads, spiritBodies, spiritLeftUpperArms, spiritRightUpperArms, spiritLeftUpperHands, spiritRightUpperHands, spiritLeftLowerArms, spiritRightLowerArms, spiritLeftLowerHands, spiritRightLowerHands, spiritLeftLegs, spiritRightLegs, spiritLeftThinFeet, spiritRightThinFeet, spiritLeftWideFeet, spiritRightWideFeet, spiritTail);
    }

    void SpiritGenerator(GameObject[] heads, GameObject[] bodies, GameObject[] leftUpperArms, GameObject[] rightUpperArms, GameObject[] leftUpperHands, GameObject[] rightUpperHands, GameObject[] leftLowerArms, GameObject[] rightLowerArms, GameObject[] leftLowerHands, GameObject[] rightLowerHands, GameObject[] leftLegs, GameObject[] rightLegs, GameObject[] leftThinFeet, GameObject[] rightThinFeet, GameObject[] leftWideFeet, GameObject[] rightWideFeet, GameObject[] tails)
    {
        SpiritGeneratePart(bodies);
        if(Randomizer(oneAndOneHundred) >= 25)
            SpiritGeneratePart(heads);
        if (Randomizer(oneAndOneHundred) >= 25)
            SpiritGenerateExtremities(leftUpperArms, rightUpperArms, leftUpperHands, rightUpperHands);
        if (Randomizer(oneAndOneHundred) >= 25)
            SpiritGenerateExtremities(leftLowerArms, rightLowerArms, leftLowerHands, rightLowerHands);
        if (Randomizer(oneAndOneHundred) >= 25)
            SpiritGenerateLegs(leftLegs, rightLegs, leftThinFeet, rightThinFeet, leftWideFeet, rightWideFeet);
        if (Randomizer(oneAndOneHundred) >= 25)
            SpiritGeneratePart(tails);
    }

    void SpiritGeneratePart(GameObject[] part)
    {
        part[Randomizer(part.Length)].SetActive(true); 
    }

    void SpiritGenerateExtremities(GameObject[] leftLimb, GameObject[] rightLimb, GameObject[] leftDigits, GameObject[] rightDigits)
    {
        symmetryValue = Randomizer(leftLimb.Length);
        leftLimb[symmetryValue].SetActive(true);
        rightLimb[symmetryValue].SetActive(true);
        symmetryValue = Randomizer(leftDigits.Length);
        leftDigits[symmetryValue].SetActive(true);
        rightDigits[symmetryValue].SetActive(true);
    }

    void SpiritGenerateLegs(GameObject[] leftLegs, GameObject[] rightLegs, GameObject[] leftThinFeet, GameObject[] rightThinFeet, GameObject[] leftWideFeet, GameObject[] rightWideFeet) 
    {
        symmetryValue = Randomizer(leftLegs.Length);
        leftLegs[symmetryValue].SetActive(true);
        rightLegs[symmetryValue].SetActive(true);
        switch (symmetryValue)
        {
            case 0:
                symmetryValue = Randomizer(leftThinFeet.Length);
                leftThinFeet[symmetryValue].SetActive(true);
                rightThinFeet[symmetryValue].SetActive(true);
                break;
            case 1:
                symmetryValue = Randomizer(leftWideFeet.Length);
                leftWideFeet[symmetryValue].SetActive(true);
                rightWideFeet[symmetryValue].SetActive(true);
                break;
        }
    }

    int Randomizer(int length)
    {
        return Random.Range(0, length);
    }
}
