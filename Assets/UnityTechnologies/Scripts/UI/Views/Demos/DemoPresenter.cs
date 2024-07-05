
using DesignPatterns.Utilities;
using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    /// <summary>
    /// This initializes and manages UI elements for a Demo scene
    /// </summary>
    public class DemoPresenter: MonoBehaviour
    {
        // Inspector fields
        [Tooltip("Required UI Document")]
        [SerializeField] UIDocument m_UIDocument;

        [Tooltip("Required InfoText ScriptableObject")]
        [SerializeField] InfoTextSO m_InfoTextData;

        // Member fields
        InfoTextView m_InfoTextView;

        // Properties
        public InfoTextSO InfoTextData => m_InfoTextData;

        private void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Sets up necessary components and validating fields
        /// </summary>
        private void Initialize()
        {
            // For running coroutines on non-MonoBehaviours       
            if (!Coroutines.IsInitialized)
            {
                Coroutines.Initialize(this);
            }

            // Validate inspector fields for required dependencies
            NullRefChecker.Validate(this);

            // Create text pages
            VisualElement root = m_UIDocument.rootVisualElement;
            m_InfoTextView = new InfoTextView(root, m_InfoTextData);
        }

        /// <summary>
        /// Unregisters any events and cleans up.
        /// </summary>
        private void OnDisable()
        {
            m_InfoTextView.Disable();
        }
    }
}
