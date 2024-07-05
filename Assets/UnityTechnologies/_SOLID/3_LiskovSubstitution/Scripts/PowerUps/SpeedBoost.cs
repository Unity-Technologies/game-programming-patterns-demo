using DesignPatterns.SRP;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.LSP
{

    /// <summary>
    /// Each PowerUp subclass can have its own unique behavior.
    /// </summary>
    public class SpeedBoost : PowerUp
    {
        [Header("Speed parameters")] 
        [Tooltip("Factor used to multiply speed")] [SerializeField]
        float m_SpeedMultiplier = 2f;

        // Override this method in the subclass
        public override void ApplyEffect(GameObject player)
        {
            // Add SpeedBoost logic here
            SpeedModifier speedModifier = player.GetComponent<SpeedModifier>();

            if (speedModifier != null)
            {
                speedModifier.ModifySpeed(m_SpeedMultiplier, m_Duration);
            }
        }
    }
}