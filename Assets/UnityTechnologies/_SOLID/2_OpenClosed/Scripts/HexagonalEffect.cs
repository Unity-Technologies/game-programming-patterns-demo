using UnityEngine;

namespace DesignPatterns.OCP
{
    /// <summary>
    /// Class to show the effect for a hexagon.
    ///
    /// Each area of effect can implement its own unique formula for calculating area. 
    /// Creating a new AreaOfEffect does not impact the existing ones, following the
    /// Open Closed Principle.
    /// </summary>
    public class HexagonalEffect : AreaOfEffect
    {
        [Header("Shape")]
        [Tooltip("The side length of the hexagon")]
        [SerializeField] private float m_SideLength;

        public override float CalculateArea()
        {
            return (3 * Mathf.Sqrt(3) / 2) * m_SideLength * m_SideLength;
        }
    }
}