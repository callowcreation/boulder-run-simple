using RadialRays;
using System.Collections;
using UnityEngine;

namespace BoulderRun.Player
{
    [RequireComponent(typeof(Rigidbody), typeof(RaysController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float m_SmoothTime = 0.3f;
        [SerializeField]
        int m_OffsetZ = 0;

        Vector3 m_Velocity;

        public Vector3 startPosition { get; private set; }
        public Quaternion startRotation { get; private set; }

        bool m_CanControl = false;

        Rigidbody m_Rigidbody = null;
        RaysController m_RaysController = null;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
            m_RaysController = GetComponent<RaysController>();
            startPosition = m_Rigidbody.position;
            startRotation = m_Rigidbody.rotation;
            m_CanControl = true;
        }

        void Update()
        {
            if(m_CanControl)
            {
                if (m_RaysController.groundChecker.grounded)
                {
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        m_OffsetZ++;
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        m_OffsetZ--;
                    }
                }
                m_OffsetZ = Mathf.Clamp(m_OffsetZ, -1, 1);
                Vector3 targetPosition = m_Rigidbody.position;
                targetPosition.z = m_OffsetZ;
                m_Rigidbody.position = Vector3.SmoothDamp(m_Rigidbody.position, targetPosition, ref m_Velocity, m_SmoothTime);
            }
        }

        public void PlayerResetValues()
        {
            if(m_CanControl)
            {
                m_CanControl = false;
                StartCoroutine(ResetValues());
            }
        }

        IEnumerator ResetValues()
        {
            yield return new WaitForSeconds(1.0f);

            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;

            m_Rigidbody.position = startPosition;
            m_Rigidbody.rotation = startRotation;
            m_OffsetZ = 0;

            m_CanControl = true;
        }
    }
}
