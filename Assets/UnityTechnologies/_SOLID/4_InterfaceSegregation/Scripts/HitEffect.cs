using UnityEngine;

namespace DesignPatterns.ISP
{
    /// <summary>
    /// Implements an effect trigger for a projectile hitting a surface.  Interface Segregation Principle
    /// promotes smaller, client-specific interfaces.
    /// </summary>
    public class HitEffect : MonoBehaviour, IEffectTrigger
    {
        [SerializeField] private ParticleSystem m_ParticleSystem;
        
        public void TriggerEffect(Vector3 position)
        {
            // Play the particle system effect if it is not null.
            if (m_ParticleSystem != null)
            {
                m_ParticleSystem.transform.position = position;
                // Stop the particle system before playing it again to avoid overlapping effects.
                m_ParticleSystem.Stop();
                m_ParticleSystem.Play();
            }
        }
    }
}
