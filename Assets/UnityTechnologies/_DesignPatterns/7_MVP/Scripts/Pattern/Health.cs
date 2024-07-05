using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DesignPatterns.MVP
{
    // The Model. This contains the data for our MVP pattern. This could also be a 
    // System.Object or ScriptableObject.
    public class Health : MonoBehaviour
    {
        // This event notifies the Presenter that the health has changed.
        // This is useful if setting the value (e.g. saving to disk or
        // storing in a database) takes some time.
        public event Action HealthChanged;

        private const int k_MinHealth = 0;
        private const int k_MaxHealth = 100;
        private int m_CurrentHealth;

        // Properties
        public int CurrentHealth
        {
            get => m_CurrentHealth;
            set
            {
                m_CurrentHealth = Mathf.Clamp(value, k_MinHealth, k_MaxHealth);
                HealthChanged?.Invoke();
            }
        }
        public int MinHealth => k_MinHealth;
        public int MaxHealth => k_MaxHealth;



        public void Increment(int amount)
        {
            CurrentHealth += amount;
        }

        public void Decrement(int amount)
        {
            CurrentHealth -= amount;
        }

        // max the health value
        public void Restore()
        {
            CurrentHealth = k_MaxHealth;
        }
    }
}
