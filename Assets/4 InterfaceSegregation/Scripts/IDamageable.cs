using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ISP
{
    // Here we have segregated part of the original interface into IDamageable.
    public interface IDamageable 
    {

        public float Health { get; set; }
        public int Defense { get; set; }

        public void Die();
        public void TakeDamage();
        public void RestoreHealth();
    }
}
