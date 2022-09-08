using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ISP
{
    // Using Interface Segregation, we only implement what interfaces apply.

    public class ExplodingBarrel : MonoBehaviour, IExplodable, IDamageable
    {
        public float Mass { get; set; }
        public float ExplosiveForce { get; set; }
        public float FuseDelay { get; set; }
        public float Timeout { get; set; }
        public float Health { get; set; }
        public int Defense { get; set; }

        // implement logic here
        public void Die()
        {
            
        }

        public void Explode()
        {

        }

        public void RestoreHealth()
        {
            
        }

        public void TakeDamage()
        {
            
        }
    }
}
