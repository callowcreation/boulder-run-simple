using UnityEngine;

namespace BoulderRun.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        Rigidbody m_Target = null;
        [SerializeField]
        float m_MaxDelta = 10.0f;
        [SerializeField]
        Vector3 m_Offset = new Vector3(6.0f, 0.0f, 0.0f);

        Vector3 m_Velocity;
        
        void Update()
        {
            Vector3 targetPosition = m_Target.position + m_Offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, m_MaxDelta);
        }
    } 
}
