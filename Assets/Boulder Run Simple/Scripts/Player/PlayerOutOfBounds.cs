using BoulderRun.Map;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoulderRun.Player
{
    public delegate void PlayerInHole(PartInfo partInfo);

    public class PlayerOutOfBounds : MonoBehaviour
    {
        public static event PlayerInHole OnPlayerInHole;

        void OnTriggerStay(Collider other)
        {
            PartInfo partInfo = other.GetComponentInParent<PartInfo>();
            if(partInfo)
            {
                if(partInfo.partType == PartType.Hole)
                {
                    if(OnPlayerInHole != null)
                    {
                        OnPlayerInHole.Invoke(partInfo);
                    }
                }
            }
        }
    }
} 

