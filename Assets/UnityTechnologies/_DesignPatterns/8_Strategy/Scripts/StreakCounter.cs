using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;

namespace DesignPatterns.Strategy
{
    [System.Serializable]
    public struct AbilityThreshold
    {
        public Ability ability;
        public int minimumStreak;
    }
    
    public class StreakCounter : MonoBehaviour
    {

        [SerializeField] private List<AbilityThreshold> m_AbilityThresholds;
        [SerializeField] private AbilityRunner m_AbilityRunner;
        [Tooltip("UI Text element to show streak counter")]
        [SerializeField] private TextMeshProUGUI m_StreakText;
    
        private int m_CurrentStreak = 0;

        // Properties
        public int CurrentStreak
        {
            get => m_CurrentStreak;
            set
            {
                m_CurrentStreak = value;
                UpdateCurrentAbility();
                UpdateStreakText();
            }
        }

        private void OnEnable()
        {
            GameEvents.OnCollectibleCollected += IncrementStreak;
        }

        private void OnDisable()
        {
            GameEvents.OnCollectibleCollected -= IncrementStreak;
        }
        private void Start()
        {
            UpdateCurrentAbility();
            UpdateStreakText();
        }

        // Update the text to show the current streak
        private void UpdateStreakText()
        {
            if (m_StreakText != null)
            {
                m_StreakText.text = m_CurrentStreak.ToString(); 
            }
        }
        
        // Update the AbilityRunner to use the highest available ability
        private void UpdateCurrentAbility()
        {
            if (m_AbilityRunner == null)
                return;
            
            // Find the highest streak ability that does not exceed the current streak count
            
            var highestAbility = m_AbilityThresholds
                .Where(x => x.minimumStreak <= m_CurrentStreak)
                .OrderByDescending(x => x.minimumStreak)
                .FirstOrDefault().ability;

            if (highestAbility != null)
            {
                m_AbilityRunner.CurrentAbility = highestAbility;
            }
        }
        
        // Increase the streak count
        public void IncrementStreak()
        {
            CurrentStreak++;
        }
       

    }
}

