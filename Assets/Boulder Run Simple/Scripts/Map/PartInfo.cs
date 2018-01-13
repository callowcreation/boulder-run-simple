using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoulderRun.Map
{
    public class PartInfo : MonoBehaviour
    {
        [SerializeField]
        PartType m_PartType = PartType.Flat;

        public PartType partType
        {
            get
            {
                return m_PartType;
            }
        }
    }
} 

