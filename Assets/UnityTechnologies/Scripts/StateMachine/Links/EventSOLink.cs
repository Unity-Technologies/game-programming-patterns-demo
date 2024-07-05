using DesignPatterns.Events;


namespace DesignPatterns.StateMachines
{

    /// <summary>
    /// Represents a link in the state machine that triggers state transitions based on a ScriptableObject-based
    /// event (BaseEventSO).
    /// </summary>
    public class EventSOLink : ILink
    {
        private IState m_NextState;  // The state to transition to when the event is raised.
        private bool m_EventRaised;  // Flag to track if the event has been raised.
        protected BaseEventSO m_GameEvent;  // The ScriptableObject-based event that triggers the state transition.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="eventSO">The event that triggers the transition.</param>
        /// <param name="nextState">The state to transition to when the event is raised.</param>

        public EventSOLink(BaseEventSO eventSO, IState nextState)
        {
            m_NextState = nextState;
            m_GameEvent = eventSO;
        }

        /// <summary>
        /// Validates if the transition condition is met and returns the next state if true.
        /// </summary>
        /// <param name="nextState">The state to transition to, if condition met.</param>
        /// <returns>True if the event has been raised, otherwise false.</returns>
        public bool Validate(out IState nextState)
        {
            nextState = m_EventRaised ? m_NextState : null;
            return m_EventRaised;
        }

        /// <summary>
        /// Enables this link, subscribes, and resets the flag
        /// </summary>
        public void Enable()
        {
            m_GameEvent.EventRaised += GameEvent_EventRaised;
            m_EventRaised = false;
        }

        /// <summary>
        /// Disables this link, unsubscribes, and resets the flag
        /// </summary>
        public void Disable()
        {
            m_GameEvent.EventRaised -= GameEvent_EventRaised;
            m_EventRaised = false;
        }

        /// <summary>
        /// Event handler that sets the flag when the game event is raised.
        /// </summary>
        public virtual void GameEvent_EventRaised()
        {
            m_EventRaised = true;
        }
    }
}
