using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.LSP
{
    public interface IMovable
    {
        public float MoveSpeed { get; set; }
        public float Acceleration { get; set; }

        public void GoForward();
        public void Reverse();

    }
}
