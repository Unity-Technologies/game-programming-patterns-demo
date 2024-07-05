using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using DesignPatterns.Events;

namespace DesignPatterns.UI
{
    /// <summary>
    /// This UI fills the screen and shows a progress bar while loading assets.
    /// </summary>
    public class SplashScreen : UIView
    {

        // UI element progress bar
        ProgressBar m_ProgressBar;

        public SplashScreen(VisualElement rootElement): base(rootElement)
        {
            // Set the parent element name (used for base.Initialize)
            m_RootElement = rootElement;


            SubscribeToEvents();

            // Force this to be visible on Awake
            m_HideOnAwake = false;

            // Initialize progress bar
            m_ProgressBar = m_RootElement.Q<ProgressBar>("splash__progress-bar");
        }

        public override void Disable()
        {
            base.Disable();

            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            // 
            SceneEvents.LoadProgressUpdated += SceneEvents_LoadProgressUpdated;

            // UI hides itself once it receives this event; this uses an arbitrary
            // value from the SequenceManager but in a real application, wait for
            // large assets (e.g. models, textures, etc.) to load
            SceneEvents.PreloadCompleted += SceneEvents_PreloadCompleted;
        }

        private void UnsubscribeFromEvents()
        {
            SceneEvents.LoadProgressUpdated -= SceneEvents_LoadProgressUpdated;
            SceneEvents.PreloadCompleted -= SceneEvents_PreloadCompleted;
        }

        // Event-handling methods

        private void SceneEvents_LoadProgressUpdated(float value)
        {
            if (m_ProgressBar == null)
                return;

            m_ProgressBar.value = value;
        }

        private void SceneEvents_PreloadCompleted()
        {
            Hide();
            Disable();
        }
    }
}


