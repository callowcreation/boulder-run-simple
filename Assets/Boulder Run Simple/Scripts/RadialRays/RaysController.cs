using System;
using UnityEngine;

namespace RadialRays
{
    public class RaysController : MonoBehaviour
    {

        public static event EventHandler<GroundedArgs> OnGrounded;

        [SerializeField]
        RaysCheck m_RaysCheck = new RaysCheck(43, 143);

        [Range(0.0f, 1.0f)]
        [SerializeField]
        float m_Spread = 0.4f;

        [Range(1, 45)]
        [SerializeField]
        int m_Step = 1;

        Transform m_Trans = null;
        Rigidbody m_Rigidbody = null;

        public RaysCheck groundChecker { get { return m_RaysCheck; } }
        public Rigidbody rb { get { return m_Rigidbody; } }

        void Awake()
        {
            m_Trans = transform;
            m_Rigidbody = GetComponent<Rigidbody>();
            m_RaysCheck.InitIfNeeded();
        }

        void Update()
        {
            if (m_RaysCheck.CountChanged())
            {
                m_RaysCheck.GenerateTesters(m_Spread, m_Step);
            }

            m_RaysCheck.CheckForGround(m_Trans, GroundedAction);
        }

        void GroundedAction(RaycastHit hit)
        {
            if (OnGrounded != null)
            {
                OnGrounded.Invoke(this, new GroundedArgs(hit));
            }
        }

        void OnDrawGizmosSelected()
        {
            if(!Application.isPlaying)
            {
                m_RaysCheck.InitIfNeeded();
                if (m_RaysCheck.CountChanged())
                {
                    m_RaysCheck.GenerateTesters(m_Spread, m_Step);
                }
                m_RaysCheck.DrawGizmos(transform);
            }
        }
    }
}
