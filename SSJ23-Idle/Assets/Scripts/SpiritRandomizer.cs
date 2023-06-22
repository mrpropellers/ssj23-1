using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritRandomizer : MonoBehaviour
{
    public GameObject spiritBase;
    public GameObject spiritBody;
    SpriteRenderer bodyRenderer;
    int counter;

    // Start is called before the first frame update
    void Awake()
    {
        SpawnBody();
    }

    void SpawnBody()
    {
        GameObject mainBody = Instantiate(spiritBody, spiritBase.transform);
        //bodyRenderer = mainBody.GetComponent<SpriteRenderer>();
        //bodyRenderer.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        mainBody.transform.parent = spiritBase.transform;
    }
}
