using RadialRays;

namespace BoulderRun.Parts
{
    public class GoalBehaviour : PartBehaviour
    {
        protected override void RaysController_OnGrounded(object sender, GroundedArgs groundedArgs)
        {
            RaysController raysController = sender as RaysController;
            PartInfo partInfo = groundedArgs.hit.collider.GetComponentInParent<PartInfo>();
            if(base.partInfo == partInfo)
            {
                raysController.rb.velocity = partInfo.influenceForces;
            }
        }
    }
} 

