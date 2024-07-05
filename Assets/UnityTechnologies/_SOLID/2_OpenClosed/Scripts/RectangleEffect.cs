using UnityEngine;

namespace DesignPatterns.OCP
{
    /// <summary>
    /// Each area of effect can implement its own unique formula for calculating area. 
    /// Creating a new AreaOfEffect does not impact the existing ones, following the
    /// Open Closed Principle.
    /// </summary>
    public class RectangleEffect : AreaOfEffect
    {
        [Header("Shape")]
        [Tooltip("The width fo the rectangle")]
        [SerializeField] private float m_Width;
        [Tooltip("The height fo the rectangle")]
        [SerializeField] private float m_Height;

        public override float CalculateArea()
        {
            return m_Width * m_Height;
        }

    }
}
