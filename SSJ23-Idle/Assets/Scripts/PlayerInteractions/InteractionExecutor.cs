using System;
using LeftOut.GameJam.Bonsai;
using LeftOut.JamAids;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LeftOut.GameJam.PlayerInteractions
{
    public class InteractionExecutor : MonoBehaviour
    {
        internal InteractionMode Mode;
        
        [SerializeField]
        CursorRaycaster m_Raycaster;
        
        void OnEnable()
        {
            m_Raycaster.RaycastHitEvent.AddListener(OnRaycastHit);
        }
        
        void OnDisable()
        {
            m_Raycaster.RaycastHitEvent.RemoveListener(OnRaycastHit);
        }

        void OnRaycastHit(RaycastHit hit)
        {
            switch (Mode)
            {
                case (InteractionMode.Prune):
                    TryGetPruneTarget(hit.collider.gameObject, out _);
                    break;
                case (InteractionMode.Select):
                default:
                    Debug.LogWarning($"No handling implemented for {Mode}");
                    break;
            }
        }

        bool TryGetPruneTarget(GameObject targetChild, out GrowingTreeLimb target)
        {
            var potentialTarget = targetChild.transform.parent.gameObject;
            if (potentialTarget != null && potentialTarget.TryGetComponent(out target))
            {
                Debug.Log($"Limb {target.name} detected.");
                return true;
            }

            target = null;
            return false;
        }

        public void OnClick(InputAction.CallbackContext action)
        {
            if (!action.performed)
            {
                return;
            }
            
            switch (Mode)
            {
                case (InteractionMode.Prune):
                    if (m_Raycaster.TryRaycast(out var hit) &&
                        TryGetPruneTarget(hit.collider.gameObject, out var limb))
                    {
                        Debug.Log($"Pruning {limb.name}");
                        limb.Prune();
                    }
                    break;
                case (InteractionMode.Select):
                default:
                    Debug.LogWarning($"No handling implemented for {Mode}");
                    break;
            }
        }
    }
}
