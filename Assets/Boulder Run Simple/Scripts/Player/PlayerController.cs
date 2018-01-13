using BoulderRun.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoulderRun.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        float m_SmoothTime = 0.3f;
        [SerializeField]
        int m_OffsetZ = 0;

        Vector3 m_Velocity;

        RadialRays.RaysController m_RaysController = null;

        public Vector3 startPosition { get; private set; }
        public Quaternion startRotation { get; private set; }

        bool m_CanControl = false;

        void Awake()
        {
            m_RaysController = GetComponent<RadialRays.RaysController>();
            startPosition = transform.position;
            startRotation = transform.rotation;
            m_CanControl = true;
        }

        void OnEnable()
        {
            PlayerOutOfBounds.OnPlayerInHole -= PlayerOutOfBounds_OnPlayerInHole;
            PlayerOutOfBounds.OnPlayerInHole += PlayerOutOfBounds_OnPlayerInHole;
        }

        void OnDisable()
        {
            PlayerOutOfBounds.OnPlayerInHole -= PlayerOutOfBounds_OnPlayerInHole;
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
                Vector3 targetPosition = transform.position;
                targetPosition.z = m_OffsetZ;
                transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, m_SmoothTime);
            }
        }

        void PlayerOutOfBounds_OnPlayerInHole(PartInfo partInfo)
        {
            if(m_CanControl)
            {
                m_CanControl = false;
                StartCoroutine(ResetValues());
            }
        }

        IEnumerator ResetValues()
        {
            yield return new WaitForSeconds(1);

            transform.position = startPosition;
            transform.rotation = startRotation;
            m_OffsetZ = 0;

            m_CanControl = true;
        }
    }
}
