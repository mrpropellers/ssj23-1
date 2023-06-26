using System;
using LeftOut.GameJam.Bonsai;
using LeftOut.JamAids;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace LeftOut.GameJam.PlayerInteractions
{
    public class InteractionExecutor : MonoBehaviour
    {
        internal InteractionMode Mode;

        [SerializeField]
        Texture2D CursorDefault;
        [FormerlySerializedAs("m_PruneSprite")]
        [SerializeField]
        Texture2D CursorPrune;

        [SerializeField]
        CursorRaycaster m_Raycaster;

        public bool IsInPruneMode => Mode == InteractionMode.Prune;

        public void TogglePruneMode()
        {
            ChangeMode(Mode == InteractionMode.Prune ? InteractionMode.Select : InteractionMode.Prune);
        }
        
        void OnEnable()
        {
            m_Raycaster.RaycastHitEvent.AddListener(OnRaycastHit);
        }
        
        void OnDisable()
        {
            m_Raycaster.RaycastHitEvent.RemoveListener(OnRaycastHit);
        }

        void ChangeMode(InteractionMode newMode)
        {
            Mode = newMode;
            if (Mode == InteractionMode.Prune)
            {
                Cursor.SetCursor(CursorPrune, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(CursorDefault, Vector2.zero, CursorMode.Auto);
            }
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
                // >>>TODO: Add limb highlighting
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
