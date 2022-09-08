using UnityEngine;

namespace DesignPatterns.LSP
{
    public class RoadVehicle : IMovable, ITurnable
    {
        public string Name;

        private float moveSpeed = 100f;
        private float acceleration = 5f;

        public float TurnSpeed = 5f;

        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float Acceleration { get => acceleration; set => acceleration = value; }

        public virtual void GoForward()
        {
            
        }

        public virtual void Reverse()
        {
            
        }

        public virtual void TurnLeft()
        {
            
        }

        public virtual void TurnRight()
        {
            
        }
    }
}
