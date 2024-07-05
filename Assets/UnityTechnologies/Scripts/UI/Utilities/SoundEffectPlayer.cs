using System;
using DesignPatterns.Utilities;
using UnityEngine;

namespace DesignPatterns
{

    /// <summary>
    /// Simple utility for playing a provided AudioClip using the given AudioSource.
    /// </summary>
    /// <param name="audioSource">The AudioSource to play the AudioClip on.</param>
    /// <param name="clip">The AudioClip to play.</param>
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip m_Clip;
        [SerializeField] AudioSource m_AudioSource;

        private void Awake()
        {
            if (m_AudioSource == null)
                m_AudioSource = GetComponent<AudioSource>();

            NullRefChecker.Validate(this);
        }

        public void PlaySoundEffect()
        {
            if (m_AudioSource == null || m_Clip == null)
                return;

            m_AudioSource.clip = m_Clip;
            m_AudioSource.Stop();
            m_AudioSource.Play();
        }
    }
}