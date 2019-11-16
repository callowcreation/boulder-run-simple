using RadialRays;
using UnityEngine;

namespace BoulderRun.Parts
{
    [RequireComponent(typeof(PartInfo))]
    public class PartBehaviour : MonoBehaviour
    {

        public PartInfo partInfo { get; private set; }

        void Awake()
        {
            partInfo = GetComponent<PartInfo>();
        }

        void OnEnable()
        {
            RaysController.OnGrounded -= RaysController_OnGrounded;
            RaysController.OnGrounded += RaysController_OnGrounded;
        }

        void OnDisable()
        {
            RaysController.OnGrounded -= RaysController_OnGrounded;
        }

        protected virtual void RaysController_OnGrounded(object sender, GroundedArgs groundedArgs)
        {
            RaysController raysController = sender as RaysController;
            PartInfo partInfo = groundedArgs.hit.collider.GetComponentInParent<PartInfo>();
            if (this.partInfo == partInfo)
            {
                raysController.rb.AddForce(partInfo.influenceForces);
            }
        }
    }
} 

