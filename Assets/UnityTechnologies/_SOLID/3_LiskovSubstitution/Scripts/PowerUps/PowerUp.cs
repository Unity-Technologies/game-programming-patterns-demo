using DesignPatterns.SRP;
using UnityEngine;

namespace DesignPatterns.LSP
{
    /// <summary>
    /// A base class for the other PowerUps.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class PowerUp : MonoBehaviour
    {
        [Tooltip("How long the PowerUp lasts, if temporary")]
        [SerializeField] protected float m_Duration;
        
        protected const string k_PlayerTag = "Player";
        
        // Override logic in each subclass
        public abstract void ApplyEffect(GameObject player);
        
        // Common functionality for all PowerUps could be added here
        protected void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag(k_PlayerTag)) 
                return;

            // Play a random beep sound
            PlaySound(other.gameObject);
            
            // Apply logic from subclass
            ApplyEffect(other.gameObject);
                
            // Handle PowerUp collection or destruction
            CollectPowerUp();
        }

        protected void PlaySound(GameObject player)
        {
            PlayerAudio m_PlayerAudio = player.GetComponent<PlayerAudio>();
            
            if (m_PlayerAudio != null)
            {
                m_PlayerAudio.PlayRandomClip();
            }
        }
        // Remove/consume PowerUp
        protected void CollectPowerUp()
        {
            // Handle the PowerUp collection or destruction here
            Destroy(gameObject);
        }
    }
}
