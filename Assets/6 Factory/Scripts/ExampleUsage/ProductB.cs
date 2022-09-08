using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class ProductB : MonoBehaviour, IProduct
    {
        [SerializeField] private string productName = "ProductB";
        public string ProductName { get => productName; set => productName = value; }

        private AudioSource audioSource;

        public void Initialize()
        {
            // do some logic here
            audioSource = GetComponent<AudioSource>();
            audioSource?.Stop();
            audioSource?.Play();

        }
    }
}

