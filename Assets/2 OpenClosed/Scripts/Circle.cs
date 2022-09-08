using UnityEngine;

namespace DesignPatterns.OCP
{
    public class Circle : Shape
    {
        public float Radius { get; set; }

        public override float CalculateArea()
        {
            return Radius * Radius * Mathf.PI;
        }
    }
}

