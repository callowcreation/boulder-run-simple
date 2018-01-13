using UnityEngine;

namespace RadialRays
{

    [System.Serializable]
    struct RayTester
    {
        [SerializeField]
        Vector3 m_Direction;
        [SerializeField]
        float m_Distance;
        [SerializeField]
        LayerMask m_LayerMask;
        [SerializeField]
        bool m_IsGrounded;

        RaycastHit m_Hit;
        
        public RaycastHit raycastHit { get { return m_Hit; } }

        public float distance
        {
            get { return m_Distance; }
        }

        public RayTester(Vector3 direction, float distance, LayerMask layerMask)
            : this()
        {
            m_Direction = direction;
            m_Distance = distance;
            m_LayerMask = layerMask;
        }

        public bool CheckGrounded(Transform trans)
        {
            var distance = trans.localScale.y * m_Distance;
            m_IsGrounded = Physics.Raycast(trans.position, m_Direction, out m_Hit, distance, m_LayerMask);
            if (m_IsGrounded)
            {
                Debug.DrawRay(trans.position, m_Direction * distance, Color.green);
            }
            else
            {
                Debug.DrawRay(trans.position, m_Direction * distance, Color.red);
            }
            return m_IsGrounded;
        }

        public void DrawDebugRays(Vector3 position, float distance)
        {
            if (m_IsGrounded)
            {
                Debug.DrawRay(position, m_Direction * distance, Color.green);
            }
            else
            {
                Debug.DrawRay(position, m_Direction * distance, Color.red);
            }
        }

        public void DrawGizmosRays(Vector3 position, float distance)
        {
            Color color = Gizmos.color;
            if (m_IsGrounded)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawRay(position, m_Direction * distance);
            Gizmos.color = color;
        }
    }
}