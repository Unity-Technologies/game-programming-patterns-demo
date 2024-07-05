using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.SRP
{
    public class PlayerFX : MonoBehaviour
    {
        [SerializeField]
        ParticleSystem m_ParticleSystem;

        // Cooldown time between particle system plays.
        const float k_Cooldown = 1f;

        float m_TimeToNextPlay = -1f;

        public void PlayEffect()
        {
            // Check if the cooldown time has passed.
            if (Time.time < m_TimeToNextPlay)
                return;

            // Play the particle system effect if it is not null.
            if (m_ParticleSystem != null)
            {
                // Stop the particle system before playing it again to avoid overlapping effects.
                m_ParticleSystem.Stop();
                m_ParticleSystem.Play();

                m_TimeToNextPlay = Time.time + k_Cooldown;
            }
        }

    }
}
