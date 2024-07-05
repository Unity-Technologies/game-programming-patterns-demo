
using System;
using UnityEngine;
using DesignPatterns.Utilities;
using UnityEngine.UI;

namespace DesignPatterns.OCP
{
    /// <summary>
    /// Plays a ParticleSystem and AudioClip.
    /// 
    /// Each area of effect can implement its own unique formula for calculating area. 
    /// Creating a new AreaOfEffect does not impact the existing ones, following the
    /// Open Closed Principle.
    /// </summary>
    public abstract class AreaOfEffect: MonoBehaviour
    {
        [Header("Particle Effect")]
        [SerializeField]
        [Optional]
        ParticleSystem m_EffectParticleSystem;
        [Header("Audio Effect")]
        [Optional]
        [SerializeField]
        AudioSource m_EffectAudioSource;
        [Optional]
        [SerializeField]
        AudioClip m_EffectSoundFX;
        [Space]
        [SerializeField] float m_CooldownTime = 1.0f;
        [SerializeField] string m_LabelString;
        [SerializeField] Text m_LabelText;
        
        /// <summary> Particle System for this AreaOfEffect.</summary>
        public ParticleSystem EffectParticleSystem => m_EffectParticleSystem;
        /// <summary> AudioSource for this AreaOfEffect.</summary>
        public AudioSource EffectAudioSource => m_EffectAudioSource;
        /// <summary> AudioClip for this AreaOfEffect.</summary>
        public AudioClip EffectSoundFX => m_EffectSoundFX;
        public float TotalArea => CalculateArea();
        private float cooldownTimer;
        
        /// <summary>
        /// Each AreaOfEffect subclass implements its own definition of CalculateArea
        /// </summary>
        /// <returns></returns>
        public abstract float CalculateArea();

        /// <summary>
        /// Play back the sounds and effects.
        /// </summary>
        private void Start()
        {
            if (m_LabelText != null)
                m_LabelText.text = string.Empty;
        }

        public void PlayEffect()
        {
            // Checks if the cooldown time has passed.
            if (Time.time >= cooldownTimer)
            {
                cooldownTimer = Time.time + m_CooldownTime;
                PlayParticleEffect();
                PlaySoundEffect();
                
                ShowAreaText();
            }
        }

        private void PlayParticleEffect()
        {
            if (m_EffectParticleSystem != null)
            {
                m_EffectParticleSystem.Play();
            }
        }

        private void PlaySoundEffect()
        {
            if (m_EffectAudioSource != null && m_EffectSoundFX != null)
            {
                m_EffectAudioSource.PlayOneShot(m_EffectSoundFX);
            }
        }

        public void ShowLabelText(string textToShow)
        {
            if (m_LabelText != null)
            {
                m_LabelText.text = textToShow;
            }
        }

        public void ShowAreaText()
        {
            ShowLabelText(m_LabelString + " " + CalculateArea());
        }
    }
}
