using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    public class InfoTextView : UIView
    {
        // Event for text updates
        public event Action<int> InfoTextUpdated;

        // Member fields
        Label m_InfoTextTitle;  // Text label for the title/header
        Label m_InfoTextBody;  // Text label for body text
        Label m_PageCounter;  // Shows current page out of max pages
        Label m_AdditionalText;
        Label m_FooterText;

        InfoTextSO m_InfoTextData;  // ScriptableObject container for the text data

        Button m_NextButton;  // Button to advance to the next text page
        Button m_LastButton;  // Button to go back to the previous text page
        private float m_ButtonCooldown = 0.5f; // Cooldown duration in seconds
        private float m_LastPressTime = -1; // Timestamp of the last button press


        // Properties
        public InfoTextSO InfoTextData => m_InfoTextData;
        
        // Constructor
        public InfoTextView(VisualElement rootElement, InfoTextSO infoTextData) : base(rootElement)
        {

            if (infoTextData == null)
            {
                Debug.LogWarning("[InfoTextView]: Invalid InfoText ScriptableObject");
            }
            m_InfoTextData = infoTextData;
            m_InfoTextData.ResetIndex();

            SetVisualElements();
            UpdateTextView();
            RegisterCallbacks();
            Show();
        }

        private void SetVisualElements()
        {
            m_InfoTextTitle = m_RootElement.Q<Label>("info-text__title");
            m_InfoTextBody = m_RootElement.Q<Label>("info-text__text");
            m_NextButton = m_RootElement.Q<Button>("info-text__next-button");
            m_LastButton = m_RootElement.Q<Button>("info-text__last-button");
            m_PageCounter = m_RootElement.Q<Label>("info-text__page-counter");
            m_AdditionalText = m_RootElement.Q<Label>("info-text__instructions");
            m_FooterText = m_RootElement.Q<Label>("info-text__footer");

            m_InfoTextTitle.text = m_InfoTextData.Title;
            m_AdditionalText.text = m_InfoTextData.AddditionalText;
            m_FooterText.text = m_InfoTextData.FooterText;
        }

        private void RegisterCallbacks()
        {
            // Register button callbacks
            m_EventRegistry.RegisterCallback<ClickEvent>(m_NextButton, IncrementIndex);
            m_EventRegistry.RegisterCallback<ClickEvent>(m_LastButton, DecrementIndex);
        }

        private void IncrementIndex()
        {
            if (Time.time - m_LastPressTime < m_ButtonCooldown)
                return;

            m_LastPressTime = Time.time;
            m_InfoTextData.IncrementIndex();
            UpdateTextView();
        }

        private void DecrementIndex()
        {
            if (Time.time - m_LastPressTime < m_ButtonCooldown)
                return;

            m_LastPressTime = Time.time;
            m_InfoTextData.DecrementIndex();
            UpdateTextView();
        }


        private void UpdateTextView()
        {
            m_InfoTextBody.text = m_InfoTextData.CurrentTextPage;
            // m_PageCounter.text = m_InfoTextData.GetPageCounter();

            m_PageCounter.text = m_InfoTextData.GetPageCounterIcons();
            
            // Hide/show buttons based on the current page index
            m_LastButton.SetEnabled(m_InfoTextData.CurrentIndex > 0);
            m_NextButton.SetEnabled(m_InfoTextData.CurrentIndex < m_InfoTextData.PageCount - 1);
            

            // Notify any listeners
            InfoTextUpdated?.Invoke(m_InfoTextData.CurrentIndex);
        }

        public override void Disable()
        {
            base.Disable();
            m_EventRegistry.Dispose();
        }

    }
}
