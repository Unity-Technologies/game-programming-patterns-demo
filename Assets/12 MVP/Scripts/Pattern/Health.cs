using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DesignPatterns.MVP
{
    // The Model. This contains the data for our MVP pattern.
    public class Health : MonoBehaviour
    {
        // This event notifies the Presenter that the health has changed.
        // This is useful if setting the value (e.g. saving to disk or
        // storing in a database) takes some time.
        public event Action HealthChanged;

        private const int minHealth = 0;
        private const int maxHealth = 100;
        private int currentHealth;

        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int MinHealth => minHealth;
        public int MaxHealth => maxHealth;

        public void Increment(int amount)
        {
            currentHealth += amount;
            currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
            UpdateHealth();
        }

        public void Decrement(int amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, minHealth, maxHealth);
            UpdateHealth();
        }

        // max the health value
        public void Restore()
        {
            currentHealth = maxHealth;
            UpdateHealth();
        }

        // invokes the event
        public void UpdateHealth()
        {
            HealthChanged.Invoke();
        }
    }
}
