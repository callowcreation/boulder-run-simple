using BoulderRun.Player;
using UnityEngine;

namespace BoulderRun.Parts
{
    public class HoleBehaviour : Part
    {
        protected override void OnGrounded(Rigidbody rb)
        {
            rb.GetComponent<PlayerController>().PlayerResetValues();
        }
    }
} 

