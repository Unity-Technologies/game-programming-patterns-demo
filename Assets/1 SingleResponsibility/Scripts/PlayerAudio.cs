using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.SRP
{
    public class PlayerAudio : MonoBehaviour
    {

        private AudioSource bounceSfx;

        private void Start()
        {
            bounceSfx = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            bounceSfx.Play();
        }
    }
}
