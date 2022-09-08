using UnityEngine;

namespace DesignPatterns.LSP
{
    public class Vehicle
    {
        public float speed = 100;
        public string name;
        public Vector3 direction;

        // Don't include these in the base class; use composition instead of inheritance.
        // We put the functionality instead into the ITurnable and IMovable interfaces.

        // The RailVehicle and RoadVehicle classes can then implement only what they need.

        //public void GoForward()
        //{

        //}

        //public void Reverse()
        //{

        //}

        //public void TurnRight()
        //{

        //}

        //public void TurnLeft()
        //{

        //}
    }
}
