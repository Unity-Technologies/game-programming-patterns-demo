using System;
using UnityEngine;

namespace DesignPatterns.Events
{
    /// <summary>
    /// This ScriptableObject delegate wraps around the static UIEvents.ScreenClosed event message for 
    /// easier serialization.
    /// </summary>
    [CreateAssetMenu(fileName = "ScreenClosedEventSO", menuName = "DesignPatterns/ScreenClosedEventSO")]
    public class ScreenClosedEventSO : BaseEventSO
    {
        [SerializeField]
        private float m_CooldownDuration = 1f; // Cooldown duration in seconds.
        
        private float m_LastEventTime = float.MinValue; // Track the last event time.

        private void OnEnable()
        {
            UpdateLastEventTime();
        }

        public override void OnEventRaised()
        {
            // Cooldown prevents unintentional double-clicking
            if (!CanRaiseEvent())
            {
                // Debug.Log("[ScreenClosedEventSO]: wait for cooldown");
                return;
            }
            
            base.OnEventRaised();
            UpdateLastEventTime();
            UIEvents.ScreenClosed?.Invoke();
      
            if (m_DebugLog)
            {
                Debug.Log("[ScreenClosedEventSO] RaiseEvent: Screen closed");
            }
        }
        
        private bool CanRaiseEvent()
        {
            // Check if enough time has passed since the last event.
            return (Time.time - m_LastEventTime) >= m_CooldownDuration;
        }

        private void UpdateLastEventTime()
        {
            // Update the last event time to the current time.
            m_LastEventTime = Time.time;
        }
    }
}