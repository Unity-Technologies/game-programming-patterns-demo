using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Singleton
{

    /// <summary>
    /// This example shows a simple audio/sound manager as a Singleton.
    /// Access the AudioManager.Instance to play an AudioClip from any GameObject.
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource audioSource;

        public Vector2 volume = new Vector2(0.5f, 0.9f);
        public Vector2 pitch = new Vector2(0.8f, 1.2f);

        // Play a clip from a designated AudioSource
        public void PlaySoundEffect(AudioClip clip)
        {
            if (audioSource == null)
                return;

            // Randomize volume and pitch
            audioSource.volume = Random.Range(volume.x, volume.y);
            audioSource.pitch = Random.Range(pitch.x, pitch.y);

            // We update the clip 
            audioSource.clip = clip;
            // We ensure the audio source is stopped before we play it again
            audioSource.Stop();
            audioSource.Play();
        }
    }
}  
