using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoulderRun.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Transform m_Target = null;
        [SerializeField]
        float m_SmoothTime = 0.3f;
        [SerializeField]
        Vector3 m_Offset = new Vector3(6.0f, 0.0f, 0.0f);

        Vector3 m_Velocity;
        
        void Update()
        {
            Vector3 targetPosition = m_Target.position + m_Offset;
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref m_Velocity, m_SmoothTime);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, m_SmoothTime);
        }
    } 
}
