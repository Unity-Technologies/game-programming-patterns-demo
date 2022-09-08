using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.MVP
{
    // The Presenter. This listens for View changes in the user interface and the manipulates the Model (Health)
    // in response. The Presenter updates the View when the Model changes.

    public class HealthPresenter : MonoBehaviour
    {
        [Header("Model")]
        [SerializeField] Health health;

        [Header("View")]
        [SerializeField] Slider healthSlider;
        [SerializeField] Text healthText;

        private void Start()
        {
            if (health != null)
            {
                health.HealthChanged += OnHealthChanged;
            }

            Reset();
        }

        private void OnDestroy()
        {
            if (health != null)
            {
                health.HealthChanged -= OnHealthChanged;
            }
        }

        // send damage to the model
        public void Damage(int amount)
        {
            health?.Decrement(amount);
        }

        public void Heal(int amount)
        {
            health?.Increment(amount);
        }

        // send reset to the model
        public void Reset()
        {
            health?.Restore();
        }

        public void UpdateView()
        {
            if (health == null)
                return;

            // format the data for view
            if (healthSlider !=null && health.MaxHealth != 0)
            {
                healthSlider.value = (float) health.CurrentHealth / (float)health.MaxHealth;
            }

            if (healthText != null)
            {
                healthText.text = health.CurrentHealth.ToString();
            }
        }

        // listen for model changes and update the view
        public void OnHealthChanged()
        {
            UpdateView();
        }
    }
}
