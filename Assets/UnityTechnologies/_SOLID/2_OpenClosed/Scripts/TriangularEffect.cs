using UnityEngine;

namespace DesignPatterns.OCP
{
    /// <summary>
    /// Class to show the effect for an equilateral triangle.
    ///
    /// Each area of effect can implement its own unique formula for calculating area. 
    /// Creating a new AreaOfEffect does not impact the existing ones, following the
    /// Open Closed Principle.
    /// </summary>
    public class TriangularEffect : AreaOfEffect
    {
        [Header("Shape")]
        [Tooltip("The side length of the triangle")]
        [SerializeField] private float m_SideLength;

        public override float CalculateArea()
        {
            return (Mathf.Sqrt(3) / 4) * m_SideLength * m_SideLength;
        }
    }
}