using System;
using UnityEngine;
using TMPro;


namespace DesignPatterns
{
    /// <summary>
    /// Monitors a GameObject for active state and updates a TMPro UGUI label.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ToggleLabel : MonoBehaviour
    {
        [Tooltip("Object to monitor for active state")]
        [SerializeField] private GameObject m_EnabledObject;
        [Header("Labels")]
        [Tooltip("The prefix to show before the enabled/disabled state text.")]
        [SerializeField] private string m_LabelPrefix;
        [Tooltip("Text to display when the monitored object is active.")]
        [SerializeField] private string m_EnabledString;
        [Tooltip("Text to display when the monitored object is inactive.")]
        [SerializeField] private string m_DisabledString;
       
        // Required TextMeshPro UGUI label
        TextMeshProUGUI m_TextLabel;

        private void Awake()
        {
            // Find the TextMeshProUGUI component on the same GameObject
            m_TextLabel = GetComponent<TextMeshProUGUI>();
        }

        void Start()
        {
            UpdateLabel();
        }

        // Updates the label based on the active state of the monitored object.
        public void UpdateLabel()
        {
            // Exit if label or monitored object is missing
            if (m_TextLabel == null || m_EnabledObject == null)
                return;

            bool state = m_EnabledObject.gameObject.activeSelf;

            m_TextLabel.text = $"{m_LabelPrefix} {(state ? m_EnabledString : m_DisabledString)}";
        }
    }
}