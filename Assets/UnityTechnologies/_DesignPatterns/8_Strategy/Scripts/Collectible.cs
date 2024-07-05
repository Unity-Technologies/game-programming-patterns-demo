using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DesignPatterns.Strategy
{
    using UnityEngine;

    public class Collectible : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            // Tag your player object with "Player"
            if (other.CompareTag("Player")) 
            {
                // Notify any listeners through the event channel
                GameEvents.CollectibleCollected();
                
                // Destroy the object after it's collected
                Destroy(gameObject); 
            }
        }
    }

}
