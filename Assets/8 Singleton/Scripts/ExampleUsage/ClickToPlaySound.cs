using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Singleton
{
    // This example shows how you access the static singleton Instance.
    public class ClickToPlaySound : MonoBehaviour
    {
        [SerializeField] AudioClip clip;
        [SerializeField] LayerMask layerToClick;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // raycast check to see if we click on the collider
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, Mathf.Infinity, layerToClick))
                {
                    PlaySoundFromAudioManager();
                }
            }
        }

        // plays an AudioClip from the global singleton instance
        private void PlaySoundFromAudioManager()
        {
            if (clip)
            {
                AudioManager.Instance.PlaySoundEffect(clip);
            }

        }
    }
}
