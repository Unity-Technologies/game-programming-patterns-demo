using System;
using UnityEngine;

namespace DesignPatterns.OCP
{
    public class EffectTrigger : MonoBehaviour
    {
        [Tooltip("The AreaOfEffect triggered with colliding with this component")]
        [SerializeField] AreaOfEffect m_Effect;
        [Tooltip("The minimum time in seconds between triggers")]
        [SerializeField] float m_Cooldown = 2f;

        float m_LastEffectTime = -1;
        // Tag for the player
        const string k_PlayerTag = "Player";

        private void OnTriggerEnter(Collider other)
        {
            PlayEffect(other);

            if (other.CompareTag(k_PlayerTag) && m_Effect != null)
                m_Effect.ShowAreaText();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(k_PlayerTag))
                PlayEffect(other);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(k_PlayerTag) && m_Effect != null)
                m_Effect.ShowLabelText(string.Empty);
        }

        private void PlayEffect(Collider other)
        {
            float nextEffectTime = m_LastEffectTime + m_Cooldown;

            // Check by tag 
            if (other.CompareTag(k_PlayerTag) && Time.time > nextEffectTime)
            {
                m_LastEffectTime = Time.time;

                // Trigger effect for player
                m_Effect.PlayEffect();
            }
        }
    }
}