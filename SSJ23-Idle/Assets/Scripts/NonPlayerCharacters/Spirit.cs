using System;
using UnityEngine;
using UnityEngine.Events;

public class Spirit : MonoBehaviour
{
    [field: SerializeField, Range(0, 3)]
    public int SpiritVoiceType { get; private set; }
    [field: SerializeField]
    public UnityEvent<int> SpiritVocalized { get; private set; }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseUp()
    {
        // TODO: Replace with interaction handling
        SpiritVocalized?.Invoke(SpiritVoiceType);
    }
}
