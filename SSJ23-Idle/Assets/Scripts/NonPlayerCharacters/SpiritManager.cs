using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LeftOut.GameJam.NonPlayerCharacters
{
    public class SpiritManager : SingletonBehaviour<SpiritManager>
    {
        List<Spirit> m_Spirits;
        
        [field: SerializeField]
        public UnityEvent<Spirit> NewSpiritCreated { get; private set; }
        
        public static bool Exists => Instance != null;
        public static int NumActiveSpirits => Exists && Instance.m_Spirits != null ? Instance.m_Spirits.Count : 0;

        protected override void Awake()
        {
            base.Awake();
            m_Spirits = new List<Spirit>();
        }
    }
}
