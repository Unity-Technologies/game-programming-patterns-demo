using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ISP
{
    // Here we have segregated part of the original interface into IMovable.
    public interface IMovable
    {
        public float MoveSpeed { get; set; }
        public float Acceleration { get; set; }

        public void MoveForward();
        public void Reverse();
        public void TurnLeft();
        public void TurnRight();
    }
}
