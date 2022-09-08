using UnityEngine;

namespace DesignPatterns.OCP
{ 
    public class AreaCalculator 
    {
        // old implemenation: not using open-closed principle

        //public float GetRectangleArea(Rectangle rectangle)
        //{
        //    return rectangle.Width * rectangle.Height;
        //}

        //public float GetCircleArea(Circle circle)
        //{
        //    return circle.Radius * circle.Radius * Mathf.PI;
        //}


        // new implementation: use open-closed principle
        // let each Shape contain the logic for calculating the area

        public float GetArea(Shape shape)
        {
            return shape.CalculateArea();
        }
    }
}  
