using BoulderRun.Map;
using RadialRays;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoulderRun.Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class InfluenceController : MonoBehaviour
    {
        Rigidbody m_Rigidbody = null;

        void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        void OnEnable()
        {
            RaysController.OnGrounded -= RaysController_OnGrounded;
            RaysController.OnGrounded += RaysController_OnGrounded;

            PlayerOutOfBounds.OnPlayerInHole -= PlayerOutOfBounds_OnPlayerInHole;
            PlayerOutOfBounds.OnPlayerInHole += PlayerOutOfBounds_OnPlayerInHole;
        }

        void OnDisable()
        {
            RaysController.OnGrounded -= RaysController_OnGrounded;

            PlayerOutOfBounds.OnPlayerInHole -= PlayerOutOfBounds_OnPlayerInHole;
        }

        void RaysController_OnGrounded(object sender, GroundedArgs groundedArgs)
        {
            PartInfo partInfo = groundedArgs.hit.collider.GetComponentInParent<PartInfo>();
            if(partInfo)
            {
                switch (partInfo.partType)
                {
                    case PartType.Flat:
                        break;
                    case PartType.Hole:
                        m_Rigidbody.AddForce(-0.1f, 0.0f, 0.0f);
                        break;
                    case PartType.Ramp:
                        m_Rigidbody.AddForce(0.25f, 0.0f, 0.0f);
                        break;
                    case PartType.Wall:
                        break;
                    case PartType.Ledge:
                        break;
                    case PartType.Goal:
                        m_Rigidbody.velocity = Vector3.zero;
                        break;
                    default:
                        break;
                }
            }
        }

        void PlayerOutOfBounds_OnPlayerInHole(PartInfo partInfo)
        {
            StartCoroutine(ResetValues());
        }

        IEnumerator ResetValues()
        {
            yield return new WaitForSeconds(1);
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;
        }
    }
} 

