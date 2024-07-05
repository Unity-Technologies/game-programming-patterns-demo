using UnityEngine;
using System;

namespace DesignPatterns.Events
{
    /// <summary>
    /// Base class for ScriptableObject-based event messages. This can wrap around a static delegate for
    /// easier serialization. Use the public Action to add external listeners.
    /// </summary>
	public abstract class BaseEventSO : DescriptionSO
    {
        /// <summary>
        /// Listeners can subscribe to this Action
        /// </summary>
        public event Action EventRaised;

        [Space]
        [Space]
        [SerializeField] protected bool m_DebugLog;

        // Constructor
        public BaseEventSO()
        {
            // Initialize the EventRaised with an empty delegate
            EventRaised += () => { };
        }

        // Event-raising method
        public virtual void OnEventRaised()
        {
            EventRaised?.Invoke();
        }
    }
}