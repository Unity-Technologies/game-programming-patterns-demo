using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.Flyweight
{
    /// <summary>
    /// Simple component to add some wave-like motion with controls for the amplitude and frequency. This
    /// adds a phase offset based on the transform's initial position.
    /// </summary>
    public class SineWaveMover : MonoBehaviour
    {
        [Tooltip("The maximum height of the waves")]
        [SerializeField] private float m_Amplitude = 1.0f; // The height of the sine wave
        [Tooltip("The speed of the wave's oscillation")]
        [SerializeField] private float m_Frequency = 1.0f; // How fast the wave oscillates

        private Vector3 m_StartPosition;
        private float m_PhaseOffset;

        // Properties
        public float Amplitude { get => m_Amplitude; set => m_Amplitude = value; }
        public float Frequency { get => m_Frequency; set => m_Frequency = value; }

        void Start()
        {
            // Store the initial position of the transform
            m_StartPosition = transform.position;

            // Generate a phase offset based on the initial position to vary the wave effect
            m_PhaseOffset = m_StartPosition.x + m_StartPosition.z;
        }

        void Update()
        {
            // Calculate the new position based on the sine wave
            float sineValue = Mathf.Sin((Time.time + m_PhaseOffset) * m_Frequency) * m_Amplitude;

            // Apply the sine wave effect on the y-axis
            transform.position = m_StartPosition + Vector3.up * sineValue;
        }
    }
}