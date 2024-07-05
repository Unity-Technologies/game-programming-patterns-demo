using System;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.Strategy
{
    public class AbilityRunner : MonoBehaviour
    {
        [Tooltip("The current special ability to execute")] [SerializeField]
        private Ability m_CurrentAbility;

        [Tooltip("UI button to trigger the current ability")] [SerializeField]
        private Button m_Button;

        // Properties
        public Ability CurrentAbility
        {
            get => m_CurrentAbility;
            set
            {
                m_CurrentAbility = value;
                UpdateButtonState();
            }
        }

        private void OnEnable()
        {
            if (m_Button == null)
            {
                m_Button.onClick.AddListener(OnAbilityButtonClicked);
            }
        }

        private void OnDisable()
        {
            if (m_Button != null)
            {
                m_Button.onClick.RemoveAllListeners();
            }
        }

        private void Start()
        {
            UpdateButtonState();
        }

        public void OnAbilityButtonClicked()
        {
            if (m_CurrentAbility != null)
            {
                m_CurrentAbility.Use(this.gameObject);
            }
        }

        // Toggle the button and update its corresponding icon/sprite
        private void UpdateButtonState()
        {
            bool hasAbility = m_CurrentAbility != null;
            
            // Disable the button if the CurrentAbility is unset
            m_Button.gameObject.SetActive(hasAbility);
    
            // Update the button's icon if an ability is set, otherwise clear it
            m_Button.image.sprite = hasAbility ? m_CurrentAbility.ButtonIcon : null;

        }
    }
}