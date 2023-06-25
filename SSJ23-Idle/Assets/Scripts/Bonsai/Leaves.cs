using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LeftOut.GameJam.Bonsai
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Leaves : MonoBehaviour
    {
        SpriteRenderer m_Renderer;
        int m_CurrentGrowthStage = 0;
        int m_NextGrowthStage = 0;

        [SerializeField]
        [Range(0f, 90f)]
        float MaxRollAllowed = 0f;
        [SerializeField]
        List<Sprite> GrowthStages;

        internal int TargetGrowthStage => m_NextGrowthStage;
        internal int NumStages => GrowthStages.Count;

        void Update()
        {
            if (m_CurrentGrowthStage != m_NextGrowthStage)
            {
                // TODO: Animate this transition
                m_Renderer.sprite = GrowthStages[m_NextGrowthStage - 1];
                m_CurrentGrowthStage = m_NextGrowthStage;
            }
        }

        internal void GrowNextStage()
        {
            if (m_CurrentGrowthStage == 0)
            {
                m_Renderer = GetComponent<SpriteRenderer>();
                m_Renderer.flipX = Random.value > 0.5f;
                if (transform.rotation.eulerAngles.z > MaxRollAllowed)
                {
                    var angles = transform.rotation.eulerAngles;
                    var anglesClamped = new Vector3(angles.x, angles.y, 
                        Mathf.Clamp(angles.z, -MaxRollAllowed, MaxRollAllowed));
                    transform.rotation = Quaternion.Euler(anglesClamped);
                }
            }

            m_NextGrowthStage = m_CurrentGrowthStage + 1;
        }
    }
}
