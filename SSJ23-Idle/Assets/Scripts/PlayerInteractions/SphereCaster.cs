﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace LeftOut.JamAids
{
    public class SphereCaster : MonoBehaviour
    {
        Vector2 m_PointerPosition;

        [SerializeField, Range(0.001f, 0.5f)]
        float m_SphereRadius;
        public LayerMask Mask;
        [Min(0)]
        public int MaxDistance = int.MaxValue;
        
        
        [field: SerializeField]
        public UnityEvent<RaycastHit> RaycastHitEvent { get; private set; }

        public void OnPointerUpdate(InputAction.CallbackContext pointerContext)
        {
            // TODO: Add ability to specify a cast radius (and use sphere cast in those cases)
            m_PointerPosition = pointerContext.ReadValue<Vector2>();
            if (TryCast(out var hit))
            {
                RaycastHitEvent?.Invoke(hit);
            }
        }

        public bool TryCast(out RaycastHit hit)
        {
            var ray = Camera.main.ScreenPointToRay(m_PointerPosition);
            return Physics.Raycast(ray, out hit, MaxDistance, Mask) 
                || Physics.SphereCast(ray, m_SphereRadius, out hit, MaxDistance, Mask);
        }
    }
}
