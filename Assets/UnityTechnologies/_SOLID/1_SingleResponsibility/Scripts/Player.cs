using UnityEngine;
using DesignPatterns.Utilities;

namespace DesignPatterns.SRP
{
    /// <summary>
    /// This class adheres to the Single Responsibility Principle (SRP). Instead of using a monolithic class,
    /// this implementation divides responsibilities among specialized components. Each component focuses on
    /// a specific aspect of the player's behavior (input handling, movement, audio, and visual effects).
    /// </summary>
    [RequireComponent(typeof(PlayerInput), typeof(PlayerAudio), typeof(PlayerMovement))]

    public class Player : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("LayerMask to identify obstacles in the game environment.")]
        LayerMask m_ObstacleLayer;

        // Components for handling different aspects of player functionality.
        PlayerInput m_PlayerInput;
        PlayerMovement m_PlayerMovement;
        PlayerAudio m_PlayerAudio;
        PlayerFX m_PlayerFX;

        private void Awake()
        {
            Initialize();
        }

        // Sets up component references.
        private void Initialize()
        {
            m_PlayerInput = GetComponent<PlayerInput>();
            m_PlayerMovement = GetComponent<PlayerMovement>();
            m_PlayerAudio = GetComponent<PlayerAudio>();
            m_PlayerFX = GetComponent<PlayerFX>();
        }

        // This method is called when the controller collides with another collider.
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Check if the collided object is in the obstacle layer.
            if (m_ObstacleLayer.ContainsLayer(hit.gameObject))
            {
                // Play a random audio clip on collision.
                m_PlayerAudio.PlayRandomClip();

                // Trigger visual effect, if defined.
                if (m_PlayerFX != null)
                    m_PlayerFX.PlayEffect();

            }
        }

        private void LateUpdate()
        {
            // Get the input vector from the PlayerInput component.
            Vector3 inputVector = m_PlayerInput.InputVector;

            // Move the player based on the input vector.
            m_PlayerMovement.Move(inputVector);
        }
    }
}
