using DesignPatterns.Utilities;
using System;
using System.Linq;
using UnityEngine;

namespace DesignPatterns.SRP
{
    /// <summary>
    /// Plays an example sound effect when colliding with a wall or obstacle.
    /// </summary>

    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] 
        float m_CooldownTime = 2f;

        [SerializeField]
        AudioClip[] m_BounceClips;

        float m_LastTimePlayed;
        AudioSource m_AudioSource;

        void Awake()
        {
            m_AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            m_LastTimePlayed = -m_CooldownTime;
        }

        public void PlayRandomClip()
        {
            // Calculate the time to play the next clip.            
            float timeToNextPlay = m_CooldownTime + m_LastTimePlayed;

            // Check if the cooldown time has passed.
            if (Time.time > timeToNextPlay)
            {
                m_LastTimePlayed = Time.time;
                m_AudioSource.clip = GetRandomClip();
                m_AudioSource.Play();
            }
        }

        private AudioClip GetRandomClip()
        {
            // Get a random clip from the array based on the number of clips in it.
            int randomIndex = UnityEngine.Random.Range(0, m_BounceClips.Length);
            return m_BounceClips[randomIndex];
        }
    }
}
