using UnityEngine;
using System;

namespace DesignPatterns.Events
{

    /// <summary>
    /// ScriptableObject-based event messages for opening a URL. 
    /// Inherits from BaseEventSO and includes a string field for the link.
    /// </summary>
    [CreateAssetMenu(fileName = "URLOpenedSO", menuName = "DesignPatterns/URLOpenedSO")]
    public class URLOpenedSO : BaseEventSO
    {
        // Inspector fields
        [Tooltip("Resource link associated with this event.")]
        [SerializeField]
        private string m_URL;

        // Properties
        public string URL { get => m_URL; set => m_URL = value; }


        public override void OnEventRaised()
        {
            base.OnEventRaised();

            if (string.IsNullOrEmpty(m_URL))
            {
                Debug.LogWarning($"{this.name} [URLOpenedSO] OnEventRaised: Invalid URL ");
                return;
            }

            UIEvents.URLOpened?.Invoke(m_URL);
        }
    }
}