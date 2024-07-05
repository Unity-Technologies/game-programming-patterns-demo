using UnityEngine;
using System;

namespace DesignPatterns.MVP_UIToolkit
{
    /// <summary>
    /// Data container for a health object that follows MVP design pattern using UI Toolkit.
    /// </summary>
    [CreateAssetMenu(fileName = "HealthData", menuName = "DesignPatterns/MVP_UIToolkit/HealthModel")]
    public class HealthModel : ScriptableObject
    {
        // Event to communicate with ViewModel
        public event Action HealthChanged;

        // Our min and max are constants as we use them for configuration only
        private const int k_MinHealth = 0;
        private const int k_MaxHealth = 100;

        [Tooltip("ID for the health object")] [SerializeField]
        private string m_LabelName;

        [Space] 
        [Tooltip("Current health value")]
        public int CurrentHealth;
        
        public int MinHealth => k_MinHealth;
        public int MaxHealth => k_MaxHealth;
        public string LabelName => m_LabelName;

        // Return a runtime instance (so each object can work on its own data)
        public static HealthModel CreateInstance(HealthModel original)
        {
            var newInstance = CreateInstance<HealthModel>();

            // Copy necessary fields
            newInstance.CurrentHealth = original.CurrentHealth;
            newInstance.m_LabelName = original.m_LabelName;
            return newInstance;
        }

        // Clamps to a valid range when entering in the Inspector
        private void OnValidate()
        {
            CurrentHealth = Mathf.Clamp(CurrentHealth, k_MinHealth, k_MaxHealth);
        }

        private void OnEnable()
        {
            // We reset the health to the maximum value when the object is enabled restarting the scene
            Restore();
        }

        public void Increment(int amount)
        {
            CurrentHealth += amount;
            
            // Clamp to valid range
            CurrentHealth = Mathf.Clamp(CurrentHealth, k_MinHealth, k_MaxHealth);
            
            HealthChanged?.Invoke();
        }

        public void Decrement(int amount)
        {
            CurrentHealth -= amount;
            
            // Clamp to valid range
            CurrentHealth = Mathf.Clamp(CurrentHealth, k_MinHealth, k_MaxHealth);
            HealthChanged?.Invoke();
        }

        // Restores the health to the maximum value.
        public void Restore()
        {
            CurrentHealth = k_MaxHealth;
            HealthChanged?.Invoke();
        }
    }
}