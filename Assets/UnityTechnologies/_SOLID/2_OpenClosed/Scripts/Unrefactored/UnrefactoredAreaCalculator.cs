using UnityEngine;

namespace DesignPatterns.OCP
{ 
    public class UnrefactoredAreaCalculator 
    {
        // Non-SOLID implementation: not using Open-Closed principle. Though
        // this approach works well with a small number of effects, it does
        // not scale and becomes unwieldy as the project grows.

        public float GetRectangleArea(Rectangle rectangle)
        {
            return rectangle.Width * rectangle.Height;
        }

        public float GetCircleArea(Circle circle)
        {
            return circle.Radius * circle.Radius * Mathf.PI;
        }
        
        // Adds additional methods with additional shapes
        // e.g GetPentagonArea, GetHexagonArea, etc.
    }

    public class Rectangle
    {
        public float Height;
        public float Width;
        
    }

    public class Circle
    {
        public float Radius;
    }
}  
