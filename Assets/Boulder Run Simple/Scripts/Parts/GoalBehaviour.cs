using UnityEngine;

namespace BoulderRun.Parts
{
    public class GoalBehaviour : Part
    {
        protected override void OnGrounded(Rigidbody rb)
        {
            rb.velocity = partInfo.influenceForces;
        }
    }
} 

