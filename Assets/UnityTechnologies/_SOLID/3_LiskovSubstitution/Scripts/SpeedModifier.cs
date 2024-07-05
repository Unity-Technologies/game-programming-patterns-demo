using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.SRP;

namespace DesignPatterns.LSP
{
    /// <summary>
    /// Used to create a speed boost or slow down effect on the PlayerMovement.
    /// </summary>
    [RequireComponent(typeof(PlayerMovement))]
    public class SpeedModifier : MonoBehaviour
    {
        
        [SerializeField] private PlayerMovement m_PlayerMovement;
        [Header("Visuals")]
        [Tooltip("Play back particles when modifier is active")]
        [SerializeField] private ParticleSystem m_ParticleSystem;
        
        private Coroutine m_SpeedBoostCoroutine;
        private bool m_IsModifierActive = false;
        private float m_Duration = 0f;

        private void Start()
        {
            if (m_PlayerMovement == null)
            {
                m_PlayerMovement = GetComponent<PlayerMovement>();
            }
        }

        public void ModifySpeed(float speedMultiplier, float duration)
        {
            // If the modifier is already active, stop the coroutine and start a new one.
            if (m_IsModifierActive)
            {
                StopCoroutine(m_SpeedBoostCoroutine);
                m_SpeedBoostCoroutine =
                    StartCoroutine(ApplySpeedModifier(speedMultiplier, m_Duration + duration - Time.time));
            }
            else
            {
                m_Duration = Time.time + duration;
                m_SpeedBoostCoroutine = StartCoroutine(ApplySpeedModifier(speedMultiplier, duration));
            }
        }

        private IEnumerator ApplySpeedModifier(float speedMultiplier, float duration)
        {
            // Apply the speed modifier if it is not already active.
            if (!m_IsModifierActive)
            {
                m_PlayerMovement.SpeedMultiplier *= speedMultiplier;
                m_IsModifierActive = true;

                if (m_ParticleSystem != null)
                {
                    m_ParticleSystem.Play();
                }
                
            }

            yield return new WaitForSeconds(duration);

            // Remove the speed modifier after the duration has passed.
            if (Time.time >= m_Duration)
            {
                m_PlayerMovement.SpeedMultiplier /= speedMultiplier;
                m_IsModifierActive = false;
                
                if (m_ParticleSystem != null)
                {
                    m_ParticleSystem.Stop();
                }
            }
        }
    }
}