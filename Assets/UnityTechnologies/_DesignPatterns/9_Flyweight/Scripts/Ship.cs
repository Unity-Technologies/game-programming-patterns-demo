using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.Flyweight
{

    /// <summary>
    /// 
    /// </summary>
    public class Ship : MonoBehaviour
    {
        [Header("Shared Data")]
        [Tooltip("Reference to shared ShipData ScriptableObject")]
        [SerializeField] private ShipData m_SharedData;

        [Header("Unique Data")]
        [Tooltip("This extrinsic health data is not shared with other instances of the ships")]
        [SerializeField] private float m_Health;

        // Initialize or update the ship with shared data
        public void Initialize(ShipData data, float health)
        {
            m_SharedData = data;
            m_Health = health;
        }

        // Example method to demonstrate usage
        public void DisplayShipInfo()
        {
            Debug.Log($"Name: {m_SharedData.UnitName}, Health: {m_Health}");
        }
    }
}
