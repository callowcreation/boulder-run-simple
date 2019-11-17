using UnityEngine;

namespace BoulderRun.Parts
{
    public class PartBehaviour : Part
    {    
        protected override void OnGrounded(Rigidbody rb)
        {
            rb.AddForce(partInfo.influenceForces);
        }
    }
} 

