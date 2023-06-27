
using UnityEngine;
using Cinemachine;
using UnityEngine.Serialization;

public class PanAndZoom : MonoBehaviour
    {
        Vector3 m_InitialCameraPosition;
        
        [SerializeField]
        float m_MaxPanDistance = 0.5f;
        [SerializeField]
        private float panSpeed = 2f;
        [SerializeField]
        private float zoomSpeed = 3f;
        [SerializeField]
        private float zoomInMax = 40f;
        [SerializeField]
        private float zoomOutMax = 90f;
        
        private CinemachineInputProvider inputProvider;
        private CinemachineVirtualCamera virtualCamera;
        [SerializeField]
        private Transform FollowTransform;

        private void Awake()
        {
            inputProvider = GetComponent<CinemachineInputProvider>();
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        void Start()
        {
            m_InitialCameraPosition = FollowTransform.position;
        }


        void Update()
        {
            float x = inputProvider.GetAxisValue(0);
            float y = inputProvider.GetAxisValue(1);
            float z = inputProvider.GetAxisValue(2);
            if (x != 0 || y != 0)
            {
                PanScreen(x, y);
            }
            if (z != 0)
            {
                ZoomScreen(-z);
            }
        }

        public void ZoomScreen(float increment)
        {
            float fov = virtualCamera.m_Lens.FieldOfView;
            float target = Mathf.Clamp(fov + increment, zoomInMax, zoomOutMax);
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(fov, target, zoomSpeed * Time.deltaTime);
        }

        //sets direction of pan as mouse travels to edge of screen
        public Vector2 PanDirection(float x, float y) {
            Vector2 direction = Vector2.zero;
            if (y >= Screen.height * .95f)
            {
                direction.y += 1;
            }
            else if (y <= Screen.height * .05f)
            {
                direction.y -= 1;
            }
            if (x >= Screen.width * .95f)
            {
                direction.x += 1;
            }
            else if (x <= Screen.width * .05f)
            {
                direction.x -= 1;
            }
            return direction;
        }

        public void PanScreen(float x, float y)
        {
            var direction = (Vector3)PanDirection(x, y);
            var newPosition = FollowTransform.position + panSpeed * Time.deltaTime * direction;
            var d = newPosition - m_InitialCameraPosition;
            var displacementClamped = new Vector3(
                Mathf.Clamp(d.x, -m_MaxPanDistance, m_MaxPanDistance),
                Mathf.Clamp(d.y, -m_MaxPanDistance, m_MaxPanDistance),
                0);
            FollowTransform.position = m_InitialCameraPosition + displacementClamped;
        }

    }
