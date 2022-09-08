using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ISP
{
    // Interface Segregation tells us to use smaller interfaces. Rather than
    // incorporate all of these Methods/Properties in one interface, use several.

    public interface IUnitStats 
    {
        //public float Health { get; set; }
        //public int Defense { get; set; }

        //public void Die();
        //public void TakeDamage();
        //public void RestoreHealth();

        //public float MoveSpeed { get; set; }
        //public float Acceleration { get; set; }

        //public void MoveForward();
        //public void Reverse();
        //public void TurnLeft();
        //public void TurnRight();

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Endurance { get; set; }

        //public float Mass { get; set; }
        //public float ExplosiveForce { get; set; }
        //public float FuseDelay { get; set; }
        //public float Timeout { get; set; }

        //public void Explode();

    }
}
