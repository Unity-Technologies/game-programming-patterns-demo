using UnityEngine;

namespace DesignPatterns.LSP
{
    public class HealthBoost : PowerUp
    {
        [SerializeField] private float m_HealValue = 50f;
        public override void ApplyEffect(GameObject player)
        {
            Health playerHealth = player.GetComponent<Health>();
            
            if (playerHealth != null)
            {
                playerHealth.Heal(m_HealValue);
            }
        }
    }
}