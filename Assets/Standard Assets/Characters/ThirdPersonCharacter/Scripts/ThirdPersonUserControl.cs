using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        public float rotateSpeed = 7.5f;

        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        private bool m_turn;
        private Quaternion m_goal;

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            if (!m_turn && h != 0)
            {
                m_turn = true;
                m_goal = m_Cam.rotation * Quaternion.Euler(0, 0, 120 * Mathf.Sign(h));
            }
        }


        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            bool crouch = Input.GetKey(KeyCode.C);

            float v = Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical"));
            m_Move = v * Vector3.forward;

            if (m_turn)
            {
                if (Quaternion.Angle(m_Cam.rotation, m_goal) <= 0.1f)
                {
                    m_Cam.rotation = m_goal;
                    m_turn = false;
                } else
                {
                    m_Cam.rotation = Quaternion.Slerp(m_Cam.rotation, m_goal, Time.deltaTime * rotateSpeed);
                }
            }
            

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
