using UnityEngine.Serialization;

namespace DesignPatterns.Flyweight
{
    using UnityEngine;

    public class UnrefactoredShipFactory : MonoBehaviour
    {
        [SerializeField] private UnrefactoredShip m_ShipPrefab;

        [Header("Layout")] 
        [SerializeField] private float m_Spacing = 1.0f; // Space between ships

        [Tooltip("Maximum height of wave motion")] 
        [SerializeField] private float m_Amplitude = 0.075f;

        [Tooltip("Oscillation speed of wave motion")] 
        [SerializeField] private float m_Frequency = 0.3f;

        [Header("Ship settings (copied)")] 
        [SerializeField] private string m_UnitName = "SpaceFighter";

        [TextArea(5, 10)] 
        [SerializeField] private string m_Description = ".";

        [Space] 
        [SerializeField] private float m_Speed = 5f;
        [SerializeField] private int m_AttackPower = 100;
        [SerializeField] private int m_Defense = 50;

        [Space] 
        [SerializeField] private Texture2D m_IconA;
        [SerializeField] private Texture2D m_IconB;
        [SerializeField] private Texture2D m_IconC;

        [Space] 
        [Tooltip("Starting health")] 
        [SerializeField] private float m_MaxHealth = 100f;

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

                    // Instantiate the ship
                    UnrefactoredShip newShip = Instantiate(m_ShipPrefab, position, Quaternion.identity, transform);

                    // Copy shared data
                    newShip.UnitName = m_UnitName;
                    newShip.Description = m_Description;
                    newShip.Speed = m_Speed;
                    newShip.AttackPower = m_AttackPower;
                    newShip.Defense = m_Defense;
                    newShip.IconA = m_IconA;
                    newShip.IconB = m_IconB;
                    newShip.IconC = m_IconC;
                    newShip.Health = m_MaxHealth;

                    // Optional oscillation movement
                    SineWaveMover oscillation = newShip.gameObject.AddComponent<SineWaveMover>();
                    oscillation.Amplitude = m_Amplitude;
                    oscillation.Frequency = m_Frequency;

                    // Optional name for easier identification
                    newShip.name = $"Ship_{i * columns + j}";
                }
            }
        }
    }
}