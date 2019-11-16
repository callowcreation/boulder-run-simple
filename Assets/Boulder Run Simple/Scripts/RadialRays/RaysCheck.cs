using System.Collections.Generic;
using UnityEngine;

namespace RadialRays
{
    [System.Serializable]
    public struct RaysCheck
    {
        public static Vector3 EulerAngle(float angle)
        {
            //http://answers.unity3d.com/questions/362763/raycast-over-arc.html
            return (Quaternion.Euler(0, 0, angle) * -Vector3.right).normalized;
        }

        [SerializeField]
        LayerMask m_CheckLayer;
        [SerializeField]
        float m_Distance;
        [SerializeField]
        [Range(0, 359)]
        int m_MinAngle;
        [SerializeField]
        [Range(0, 359)]
        int m_MaxAngle;
        [SerializeField]
        bool m_Grounded;
        
        List<RayTester> m_RayTesters;

        public RaysCheck(int minAngle, int maxAngle) : this()
        {
            this.m_MinAngle = minAngle;
            this.m_MaxAngle = maxAngle;
        }

        public bool grounded { get { return m_Grounded; } }

        internal void InitIfNeeded()
        {
            if(m_RayTesters == null) m_RayTesters = new List<RayTester>();
        }

        internal bool CountChanged()
        {
            if (m_RayTesters == null) return false;
            return m_RayTesters.Count != m_MaxAngle - m_MinAngle;
        }

        internal void GenerateTesters(float spread = 0.4f, int step = 2)
        {
            step = Mathf.Clamp(step, 1, int.MaxValue);
            m_RayTesters.Clear();
            for(int angle = m_MinAngle; angle < m_MaxAngle; angle += step)
            {
                var testDirection = EulerAngle(angle);
                var rayTester = new RayTester(testDirection + Vector3.forward * spread, m_Distance, m_CheckLayer);
                m_RayTesters.Insert(0, rayTester);
            }
            for(int angle = m_MinAngle; angle < m_MaxAngle; angle += step)
            {
                var testDirection = EulerAngle(angle);
                var rayTester = new RayTester(testDirection, m_Distance, m_CheckLayer);
                m_RayTesters.Insert(0, rayTester);
            }
            for(int angle = m_MinAngle; angle < m_MaxAngle; angle += step)
            {
                var testDirection = EulerAngle(angle);
                var rayTester = new RayTester(testDirection - Vector3.forward * spread, m_Distance, m_CheckLayer);
                m_RayTesters.Insert(0, rayTester);
            }
        }


        internal void CheckForGround(Transform trans, System.Action<RaycastHit> groundedAction)
        {
            m_Grounded = false;
            foreach (var groundTester in m_RayTesters)
            {
                if (groundTester.CheckGrounded(trans))
                {
                    m_Grounded = true;
                    groundedAction.Invoke(groundTester.raycastHit);
                }
            }
        }

        internal void DrawGizmos(Transform trans)
        {
            if(m_RayTesters != null)
            {
                foreach (var groundTester in m_RayTesters)
                {
                    groundTester.DrawGizmosRays(trans.position, groundTester.distance);
                }
            }
        }
    }
}