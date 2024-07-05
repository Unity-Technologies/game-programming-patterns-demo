using UnityEngine;
using DesignPatterns.Utilities;

namespace DesignPatterns.OCP
{
    /// <summary>
    /// Each area of effect can implement its own unique formula for calculating area. 
    /// Creating a new AreaOfEffect does not impact the existing ones, following the
    /// Open Closed Principle.
    /// </summary>
    public class CircleEffect : AreaOfEffect
    {
        [Header("Shape")]
        [Tooltip("The radius of the circle")]
        [SerializeField] float m_Radius;

        public float Radius { get => m_Radius; set => m_Radius = value; }

        public override float CalculateArea()
        {
            return Radius * Radius * Mathf.PI;
        }
    }
}

