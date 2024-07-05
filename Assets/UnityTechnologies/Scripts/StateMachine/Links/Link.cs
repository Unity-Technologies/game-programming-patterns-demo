namespace DesignPatterns.StateMachines
{
    /// <summary>
    /// A link that is always open for transition. The state machine moves
    /// to the next step without delay once the execution of the current step 
    /// is finished.
    /// </summary>
    public class Link : ILink
    {
        /// <summary>
        /// The next state to transition to.
        /// </summary>
        readonly IState m_NextState;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="nextState"></param>
        public Link(IState nextState)
        {
            m_NextState = nextState;
        }

        /// <summary>
        /// Always true, so moves directly to the next state.
        /// </summary>
        /// <param name="nextState"></param>
        /// <returns></returns>
        public bool Validate(out IState nextState)
        {
            nextState = m_NextState;
            return true;
        }
    }
}