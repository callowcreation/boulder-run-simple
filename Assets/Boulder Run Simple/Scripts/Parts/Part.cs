using RadialRays;
using UnityEngine;

namespace BoulderRun.Parts
{
    [RequireComponent(typeof(PartInfo))]
    public abstract class Part : MonoBehaviour
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

        void RaysController_OnGrounded(object sender, GroundedArgs groundedArgs)
        {
            PartInfo partInfo = groundedArgs.hit.collider.GetComponentInParent<PartInfo>();
            if (this.partInfo == partInfo)
            {
                OnGrounded(((RaysController)sender).GetComponent<Rigidbody>());
            }
        }

        protected abstract void OnGrounded(Rigidbody rb);
    }
} 

