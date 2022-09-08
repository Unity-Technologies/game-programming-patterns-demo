using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ISP
{
    // Using Interface Segregation, we only implement what interfaces apply.

    public class EnemyUnit : MonoBehaviour, IDamageable, IMovable, IUnitStats
    {
        public float Health { get; set; }
        public int Defense { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Endurance { get; set; }
        public float MoveSpeed { get; set; }
        public float Acceleration { get; set; }

        // implement logic here
        public void TakeDamage()
        {

        }

        public void Die()
        {
            
        }

        public void RestoreHealth()
        {

        }

        public void MoveForward()
        {
            
        }


        public void Reverse()
        {
            
        }


        public void TurnLeft()
        {
            
        }

        public void TurnRight()
        {
            
        }
    }
}
