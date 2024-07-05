using DesignPatterns.Utilities;
using UnityEngine;
using UnityEngine.UI;


namespace DesignPatterns.MVP
{
    // The Presenter. This listens for View changes in the user interface and the manipulates the Model (Health)
    // in response. The Presenter updates the View when the Model changes.

    public class HealthPresenter : MonoBehaviour
    {
        [Header("Model")]
        [Tooltip("An object containing the health data")]
        [SerializeField] Health m_Health;

        [Header("View")]
        [Tooltip("UI Slider representing health bar")]
        [SerializeField] Slider m_HealthSlider;
        [Optional]
        [SerializeField] Text m_HealthLabel;

        private void Awake()
        {
            NullRefChecker.Validate(this);
        }

        private void Start()
        {
            m_Health.HealthChanged += Health_HealthChanged;
            InitializeSlider();

            Reset();
            UpdateView();
        }

        private void OnDestroy()
        {
            m_Health.HealthChanged -= Health_HealthChanged;
        }

        private void InitializeSlider()
        {
            m_HealthSlider.maxValue = m_Health.MaxHealth;
        }

        // send damage to the model
        public void Damage(int amount)
        {
            m_Health.Decrement(amount);
        }

        public void Heal(int amount)
        {
            m_Health.Increment(amount);
        }

        // send reset to the model
        public void Reset()
        {
            m_Health.Restore();
        }

        public void UpdateView()
        {
            if (m_Health == null)
                return;

            // format the data for view
            if (m_Health.MaxHealth != 0)
            {
                m_HealthSlider.value = ((float)m_Health.CurrentHealth / (float)m_Health.MaxHealth) *100f;
            }

            if (m_HealthLabel != null)
            {
                m_HealthLabel.text = m_Health.CurrentHealth.ToString();
            }
        }

        // Event handling method; listen for model changes and update the view
        public void Health_HealthChanged()
        {
            UpdateView();
        }
    }
}
