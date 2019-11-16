using System;
using UnityEngine;

namespace RadialRays
{
    public class GroundedArgs : EventArgs
    {
        public readonly RaycastHit hit;
        public GroundedArgs(RaycastHit hit)
        {
            this.hit = hit;
        }
    }
}
