using System;
using LeftOut.GameJam.Bonsai;
using LeftOut.JamAids;
using LeftOut.GameJam.Clock;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Events;

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
        SphereCaster m_SphereCaster;

        [field: SerializeField]
        public UnityEvent PruneBranch { get; private set; }

        public bool IsInPruneMode => Mode == InteractionMode.Prune;

        public bool IsPruningAllowed => PomoTimer.Exists
            ? PomoTimer.IsPlaying && PomoTimer.currentSessionType != SessionType.Focus
            // If no PomoTimer, just assume we're testing and allow pruning
            : true;

        public void TogglePruneMode()
        {
            ChangeMode(Mode == InteractionMode.Prune ? InteractionMode.Select : InteractionMode.Prune);
        }
        
        void Start()
        {
            m_SphereCaster.RaycastHitEvent.AddListener(OnRaycastHit);
            if (PomoTimer.Exists)
            {
                PomoTimer.Instance.SessionStarted.AddListener(OnSessionStarted);
                PomoTimer.Instance.SessionEnded.AddListener(OnSessionEnded);
                PomoTimer.Instance.UserPlayPause.AddListener(OnPlayPause);
            }
            else
            {
                Debug.LogWarning("No PomoTimer to monitor");
            }
        }
        
        void OnDisable()
        {
            m_SphereCaster.RaycastHitEvent.RemoveListener(OnRaycastHit);
            if (PomoTimer.Exists)
            {
                PomoTimer.Instance.SessionStarted.RemoveListener(OnSessionStarted);
                PomoTimer.Instance.SessionEnded.RemoveListener(OnSessionEnded);
                PomoTimer.Instance.UserPlayPause.RemoveListener(OnPlayPause);
            }
        }

        void OnPlayPause(bool isPlaying)
        {
            if (isPlaying)
            {
                OnSessionStarted(PomoTimer.currentSessionType);
            }
            else
            {
                ChangeMode(InteractionMode.Select);
            }
        }

        void OnSessionStarted(SessionType sessionStarted)
        {
            ChangeMode(InteractionMode.Select);
        }

        void OnSessionEnded(SessionType sessionEnded)
        {
            ChangeMode(InteractionMode.Select);
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
                    if (m_SphereCaster.TryCast(out var hit) &&
                        TryGetPruneTarget(hit.collider.gameObject, out var limb))
                    {
                        Debug.Log($"Pruning {limb.name}");

                        limb.Prune();
                        PruneBranch?.Invoke();
                        
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
