using UnityEngine;

namespace DesignPatterns.Flyweight
{
    /// <summary>
    /// 
    /// </summary>
    public class ShipFactory : MonoBehaviour
    {
        [SerializeField] private Ship m_ShipPrefab;
        [SerializeField] private ShipData m_SharedData;
        
        [Header("Layout")]
        [Tooltip("Space between ships")]
        [SerializeField] private float m_Spacing = 1.0f;
        [Tooltip("Maximum height of wave motion")]
        [SerializeField] private float m_Amplitude = 0.075f;
        [Tooltip("Oscillation speed of wave motion")]
        [SerializeField] private float m_Frequency = 0.3f;

        void Start()
        {
            GenerateShips(10, 10);
        }

        public void GenerateShips(int rows, int columns)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Calculate position
                    Vector3 position = new Vector3(i * m_Spacing, 0, j * m_Spacing);

                    // Instantiate and initialize the ship
                    Ship newShip = Instantiate(m_ShipPrefab, position, Quaternion.identity, transform);
                    
                    // Set starting health of 100
                    newShip.Initialize(m_SharedData, 100);

                    // Optional oscillation movement
                    SineWaveMover oscillation = newShip.gameObject.AddComponent<SineWaveMover>();
                    oscillation.Amplitude = m_Amplitude;
                    oscillation.Frequency = m_Frequency;
                    
                    // Optional name
                    newShip.name = $"Ship_{i * columns + j}";
                    
                }
            }
        }
    }
}
