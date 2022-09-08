using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.LSP
{
    public class RailVehicle : IMovable
    {

        public string Name;

        private float moveSpeed = 100f;
        private float acceleration = 5f;

        public float TurnSpeed = 5f;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }


        // implement these differently than RoadVehicles
        public virtual void GoForward()
        {
            
        }

        public virtual void Reverse()
        {
           
        }
    }
}
