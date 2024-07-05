using UnityEngine;

namespace DesignPatterns.Utilities
{
    /// <summary>
    /// Utility to make an item bob up and down.
    /// </summary>
    public class Oscillator : MonoBehaviour
    {
        [SerializeField] [Tooltip("Maximum height the item will move upwards.")]
        private float m_MaxDisplacement = 0.5f;

        [SerializeField] [Tooltip("Speed of the bobbing motion.")]
        private float m_Speed = 2f;

        [SerializeField] bool m_RandomizePhase;
        
        private float m_OriginalY;
        private float m_Offset;

        private void Start()
        {
            // Store the original height of the item.
            m_OriginalY = transform.position.y;
            m_Offset = m_RandomizePhase ? Random.Range(0f, 2f) : 0;
        }

        private void Update()
        {

            // Calculate the new y position based on a sine wave.
            float newY = m_OriginalY + m_MaxDisplacement * ( Mathf.Sin(m_Offset+ Time.time * m_Speed));

            // Apply the new position to the item, only modifying the y coordinate.
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}