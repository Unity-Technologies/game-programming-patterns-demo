using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ISP
{
    // Here we have segregated part of the original interface into IExplodable.
    public interface IExplodable
    {
        public float Mass { get; set; }
        public float ExplosiveForce { get; set; }
        public float FuseDelay { get; set; }
        public float Timeout { get; set; }

        public void Explode();
    }
}
