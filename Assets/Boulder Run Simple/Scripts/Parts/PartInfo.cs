using UnityEngine;

namespace BoulderRun.Parts
{
    public class PartInfo : MonoBehaviour
    {
        [SerializeField]
        Color m_PartColor = Color.black;

        [SerializeField]
        Vector3 m_InfluenceForces = Vector3.zero;

        public Color partColor
        {
            get
            {
                return m_PartColor;
            }
        }

        public Vector3 influenceForces
        {
            get
            {
                return m_InfluenceForces;
            }
        }
    }
} 

