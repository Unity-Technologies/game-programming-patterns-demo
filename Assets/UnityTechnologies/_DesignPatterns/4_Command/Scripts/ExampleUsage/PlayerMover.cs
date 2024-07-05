using UnityEngine;

namespace DesignPatterns.Command
{
    // moves the player one space and checks for walls
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private LayerMask m_ObstacleLayer;
        
        private const float k_BoardSpacing = 1f;

        // Path Visualization
        private PlayerPath m_PlayerPath;
        public PlayerPath PlayerPath => m_PlayerPath;


        private void Start()
        {
            m_PlayerPath = gameObject.GetComponent<PlayerPath>();
        }

        // Simple movement along XZ-plane
        public void Move(Vector3 movement)
        {
            Vector3 destination = transform.position + movement;
            transform.position = destination;
        }

        public bool IsValidMove(Vector3 movement)
        {
            return !Physics.Raycast(transform.position, movement, k_BoardSpacing, m_ObstacleLayer);
        }
    }
}