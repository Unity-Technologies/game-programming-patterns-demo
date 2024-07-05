using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Singleton
{
    // This example shows how you access the static singleton Instance.
    public class ClickToPlaySound : MonoBehaviour
    {
        [SerializeField] private AudioClip m_Clip;
        [SerializeField] private LayerMask m_LayerToClick;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // Raycast check to see if we click on the collider
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, Mathf.Infinity, m_LayerToClick))
                {
                    PlaySoundFromAudioManager();
                }
            }
        }

        // Plays an AudioClip from the global singleton instance
        private void PlaySoundFromAudioManager()
        {
            if (m_Clip != null)
            {
                AudioManager.Instance.PlaySoundEffect(m_Clip);
            }

        }
    }
}
