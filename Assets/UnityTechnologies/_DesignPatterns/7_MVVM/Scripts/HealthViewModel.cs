using UnityEngine;
using System;
using DesignPatterns.Utilities;
using Unity.Properties;
using UnityEngine.UIElements;

namespace DesignPatterns.MVVM
{
    /// <summary>
    /// This acts a mediator between the Model (data) and the View (UI). This listens
    /// for changes to the HealthModel and then notifies any listeners. This component handles
    /// user interactions from the UI to affect the HealthModel.
    /// </summary>
    public class HealthViewModel : MonoBehaviour
    {
        // Inspector fields
        [Tooltip("Reference to the View (UI)")]
        [SerializeField] private UIDocument m_Document;

        [Tooltip("Reference to the Model data (ScriptableObject asset)")]
        [SerializeField] private HealthModel m_HealthModelAsset;

        // Root element of the UI
        private VisualElement m_Root;
        
        private void OnEnable()
        {
            // Validate required fields
            NullRefChecker.Validate(this);
            
            // Cache the root element
            m_Root= m_Document.rootVisualElement;
            
            // Set up UI interactions, buttons
            RegisterElements();
            
            // Bind HealthModel to the UI
            SetDataBindings();
        }

        
        // Methods to interact with View
        private void RegisterElements()
        {
            // Find the button in the UXML
            var resetButton = m_Root .Q<Button>("reset-button");

            // Subscribe the ResetHealth method to the Clickable.clicked event of the reset button.
            if (resetButton != null)
            {
                resetButton.clicked += RestoreHealth;
            }
        }

        // Sets up data bindings between the HealthModel and UI elements, for automatic updates
        private void SetDataBindings()
        {
            // Locate the UI element 
            var healthBar = m_Root.Q<ProgressBar>("health-bar");
            var healthBarProgress = healthBar?.Q<VisualElement>(className: "unity-progress-bar__progress");

            if (healthBarProgress != null)
            {
                // Define an object as a data source on the element (the ScriptableObject in this case)
                healthBarProgress.dataSource = m_HealthModelAsset;

                // Create a new data binding for the health bar's background color
                var binding = new DataBinding
                {
                    dataSourcePath = new PropertyPath(nameof(HealthModel.CurrentHealth)),

                    // Bind one-way from the data source to the UI
                    bindingMode = BindingMode.ToTarget,
                };

                // Add a formula to map the int value to a color
                binding.sourceToUiConverters.AddConverter((ref int value) =>
                    new StyleColor(Color.Lerp(Color.red, Color.green,
                        (float)value / (float)m_HealthModelAsset.MaxHealth)));

                // Register the binding to the health bar progress element
                healthBarProgress.SetBinding("style.backgroundColor", binding);
            }
        }

        // Methods to interact with the Model
        public void RestoreHealth()
        {
            m_HealthModelAsset.Restore();
        }

        public void ApplyDamage(int damage)
        {
            m_HealthModelAsset.Decrement(damage);
        }
    }
}
