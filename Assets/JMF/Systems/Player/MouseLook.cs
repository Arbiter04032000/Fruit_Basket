using UnityEngine;

namespace JMF.Systems.Player
{
    public class MouseLook : MonoBehaviour
    {
        public float m_sensitivity = 1f; // Look around sensitivity
        public float m_clampAngle = 90f; // Clamp angle for looking up and down
        public Transform m_playerObject; // Stores the player go

        //private InputMaster m_controls; // New unity input system
        private Vector2 m_mousePos; // Store mouse position
        private float m_xRotation = 0f; // Stores the x rotation

        // Awake happens first
        void Awake()
        {
            //m_controls = new InputMaster(); // Create a new control instance
        }

        // Start
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to center of screen
        }

        // Update is called once per frame
        void Update()
        {
            GetMousePos(); // get the mouse position
            FixXRotation(); // fix the x rotation
            LookAt(); // look at mouse position
        }

        // Get mouse position
        void GetMousePos()
        {
            m_mousePos.x = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
            m_mousePos.y = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;
        }

        // Fix X Rotation
        void FixXRotation()
        {
            m_xRotation -= m_mousePos.y;
            m_xRotation = Mathf.Clamp(m_xRotation, -m_clampAngle, m_clampAngle);
        }

        // Look at mouse pos
        void LookAt()
        {
            transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
            m_playerObject.Rotate(Vector3.up * m_mousePos.x);
        }
    }
}
