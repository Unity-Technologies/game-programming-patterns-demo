using UnityEngine;

namespace DesignPatterns.LSP
{
    /// <summary>
    /// A base class for the other PowerUps.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class UnrefactoredPowerUp : MonoBehaviour
    {
        const string k_PlayerTag = "Player";

        // Override logic in each subclass
        public abstract void ApplyEffect(GameObject player);

        // Common functionality for all PowerUps could be added here
        protected void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(k_PlayerTag))
                return;

            // Apply logic from subclass
            ApplyEffect(other.gameObject);

            // Handle PowerUp collection or destruction
            CollectPowerUp();
        }

        // Remove/consume PowerUp
        protected void CollectPowerUp()
        {
            // Handle the PowerUp collection or destruction here
            Destroy(gameObject);
        }
    }
}