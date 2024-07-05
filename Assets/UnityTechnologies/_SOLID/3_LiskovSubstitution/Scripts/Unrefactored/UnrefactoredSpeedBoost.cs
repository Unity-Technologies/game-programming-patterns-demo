using DesignPatterns.SRP;
using UnityEngine;

namespace DesignPatterns.LSP
{
    /// <summary>
    /// This class shows a violation of Liskov Substitution. The subclass adds a time-based duration, not
    /// present in the base class. Though the logic is functional, "duration" is not a concept from the base class.
    /// Thus, the UnrefactoredSpeedBoost cannot be substituted for other PowerUps that do not support duration.
    /// </summary>
    public class UnrefactoredSpeedBoost : UnrefactoredPowerUp
    {
        public float m_SpeedMultiplier = 2f;
        public float m_Duration = 5f; // Duration not supported by the base class

        public override void ApplyEffect(GameObject player)
        {
            if (m_Duration > 0)
            {
                SpeedModifier playerMovement = player.GetComponent<SpeedModifier>();
                if (playerMovement != null)
                {
                    playerMovement.ModifySpeed(m_SpeedMultiplier, m_Duration);
                }
            }
            else
            {
                // This branch or logic might be confusing for someone who only expects to "ApplyEffect"
                // without a duration.  Not every PowerUp is interchangeable if we use this logic.
            }
        }
    }
}
