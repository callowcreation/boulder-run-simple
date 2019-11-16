using BoulderRun.Player;
using RadialRays;
using System.Collections;
using UnityEngine;

namespace BoulderRun.Parts
{
    public class HoleBehaviour : PartBehaviour
    {
        protected override void RaysController_OnGrounded(object sender, GroundedArgs groundedArgs)
        {
            RaysController raysController = sender as RaysController;
            PartInfo partInfo = groundedArgs.hit.collider.GetComponentInParent<PartInfo>();
            if (base.partInfo == partInfo)
            {
                raysController.rb.velocity = partInfo.influenceForces;
                StartCoroutine(ResetValues(raysController));
            }
        }

        IEnumerator ResetValues(RaysController raysController)
        {
            raysController.rb.velocity = Vector3.zero;
            raysController.rb.angularVelocity = Vector3.zero;
            raysController.GetComponent<PlayerController>().PlayerResetValues();
            yield return new WaitForSeconds(1);
        }
    }
} 

