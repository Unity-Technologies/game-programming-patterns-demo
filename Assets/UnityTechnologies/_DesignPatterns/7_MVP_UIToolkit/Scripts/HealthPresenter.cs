using UnityEngine;
using DesignPatterns.Utilities;
using Unity.Properties;
using UnityEngine.UIElements;

namespace DesignPatterns.MVP_UIToolkit
{
    /// <summary>
    /// This acts a mediator between the Model (data) and the View (UI). This listens
    /// for changes to the HealthModel and then notifies any listeners. This component handles
    /// user interactions from the UI to affect the HealthModel.
    /// </summary>
    public class HealthPresenter : MonoBehaviour
    {
        // Inspector fields
        [Tooltip("Reference to the View (UI)")] [SerializeField]
        private UIDocument m_Document;

        [Tooltip("Reference to the Model data (ScriptableObject asset)")] [SerializeField]
        private HealthModel m_HealthModelAsset;

        // Root element of the UI
        private VisualElement m_Root;

        // UI elements
        private ProgressBar m_HealthBar;
        private Label m_StatusLabel;
        private Label m_ValueLabel;

        private void OnEnable()
        {
            // Validate required fields using our utility helper class
            NullRefChecker.Validate(this);

            // Cache the root element
            m_Root = m_Document.rootVisualElement;

            // Find and assign our UI elements
            m_HealthBar = m_Root.Q<ProgressBar>("health-bar");
            m_StatusLabel = m_Root.Q<Label>("health-bar__status-label");
            m_ValueLabel = m_Root.Q<Label>("health-bar__value-label");

            // Set up UI interactions, buttons
            RegisterElements();

            if (m_HealthModelAsset != null)
            {
                m_HealthBar.title = m_HealthModelAsset.LabelName;
                m_HealthModelAsset.HealthChanged += OnHealthChanged;
            }

            UpdateUI();
        }

        private void OnHealthChanged()
        {
            UpdateUI();
        }

        private void OnDisable()
        {
            if (m_HealthModelAsset != null)
            {
                m_HealthModelAsset.HealthChanged -= OnHealthChanged;
            }
        }

        // Methods to interact with View
        private void RegisterElements()
        {
            // Find the button in the UXML
            var resetButton = m_Root.Q<Button>("reset-button");

            // Subscribe the ResetHealth method to the Clickable.clicked event of the reset button.
            if (resetButton != null)
            {
                resetButton.clicked += RestoreHealth;
            }
        }

        private void UpdateUI()
        {
            // Calculate percentage health
            float healthRatio = (float)m_HealthModelAsset.CurrentHealth / m_HealthModelAsset.MaxHealth;
            
            // Interpolate color
            Color healthColor = Color.Lerp(Color.red, Color.green,healthRatio);

            // Update health bar value
            m_HealthBar.value = healthRatio * 100f;

            // Update health bar color
            var healthBarProgress = m_HealthBar?.Q<VisualElement>(className: "unity-progress-bar__progress");
            if (healthBarProgress != null)
            {
                healthBarProgress.style.backgroundColor = new StyleColor(healthColor);
            }
            
            // Update the status label based on percentage health
            m_StatusLabel.text = healthRatio switch
            {
                >= 0 and < 1.0f / 3.0f => "Danger",
                >= 1.0f / 3.0f and < 2.0f / 3.0f => "Neutral",
                _ => "Good"
            };
            
            // Change the color of the status label to interpolated color
            m_StatusLabel.style.color = new StyleColor(healthColor);

            // Update value label
            m_ValueLabel.text = m_HealthModelAsset.CurrentHealth.ToString();
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