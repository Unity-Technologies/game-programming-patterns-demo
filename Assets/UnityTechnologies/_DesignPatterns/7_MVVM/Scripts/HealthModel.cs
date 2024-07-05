using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace DesignPatterns.MVVM
{
    /// <summary>
    /// Data container for a health object that follows MVVM design pattern.
    /// </summary>
    [CreateAssetMenu(fileName = "TargetHealth", menuName = "DesignPatterns/MVVM/TargetHealth")]
    public class HealthModel : ScriptableObject
    {
        // Event to communicate with ViewModel
        // public event Action HealthChanged;

        private const int k_MinHealth = 0;
        private const int k_MaxHealth = 100;

        [Tooltip("ID for the health object")]
        public string LabelName;

        [Space] [Tooltip("Current health value, public for data binding")]
        public int CurrentHealth;
        
        public int MinHealth => k_MinHealth;
        public int MaxHealth => k_MaxHealth;

        // Return a runtime instance (so each object can work on its own data)
        public static HealthModel CreateInstance(HealthModel original)
        {
            var newInstance = CreateInstance<HealthModel>();

            // Copy necessary fields
            newInstance.CurrentHealth = original.CurrentHealth;
            newInstance.LabelName = original.LabelName;
            return newInstance;
        }

        /// <summary>
        /// Initializes converters for data binding. This can convert the original health values
        /// (integer) into a format more compatible with the UI property (e.g. colors or strings
        /// for the background style or text properties). Add additional converters as needed.
        /// </summary>
        [InitializeOnLoadMethod]
        public static void RegisterConverters()
        {
            // Lambda to convert integer value to a float between 0 and 1
            float HealthRatio(int health) => health / (float)k_MaxHealth;
            
            // Name the ConverterGroup for easier access 
            var converter = new ConverterGroup("Int to HealthBar");

            // Convert health value to color 
            converter.AddConverter((ref int value) =>
                new StyleColor(Color.Lerp(Color.red, Color.green, HealthRatio(value))));

            // Convert health value into text
            converter.AddConverter((ref int value) =>
            {
                float healthRatio = HealthRatio(value);
                return healthRatio switch
                {
                    >= 0 and < 1.0f / 3.0f => "Danger",
                    >= 1.0f / 3.0f and < 2.0f / 3.0f => "Neutral",
                    _ => "Good"
                };
            });

            // Register the converter group in InitializeOnLoadMethod to make it accessible from the UI Builder.
            ConverterGroups.RegisterConverterGroup(converter);
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
        }

        public void Decrement(int amount)
        {
            CurrentHealth -= amount;
            
            // Clamp to valid range
            CurrentHealth = Mathf.Clamp(CurrentHealth, k_MinHealth, k_MaxHealth);
        }

        // Restores the health to the maximum value.
        public void Restore()
        {
            CurrentHealth = k_MaxHealth;
        }
    }
}