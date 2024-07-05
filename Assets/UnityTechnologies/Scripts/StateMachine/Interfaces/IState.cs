using System.Collections;
using UnityEngine;

namespace DesignPatterns.StateMachines
{
    /// <summary>
    /// An interface for the states of state machines
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called by the owner state-machine when this state becomes the "Current State"
        /// This method is called before other state methods and is used to set up state requirements.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called by the owner state-machine when this state becomes the "Current State"
        /// This coroutine is started after Enter() and contains the main logic of the state.
        /// </summary>
        /// <returns></returns>
        IEnumerator Execute();

        /// <summary>
        /// Called by the owner state-machine when this state is not the "Current State" anymore and is moving to the next state.
        /// This method is used for cleaning up.
        /// </summary>
        void Exit();

        /// <summary>
        /// Adds a transition link to the state. Each state can have multiple links.
        /// </summary>
        /// <param name="link">the link to add</param>
        void AddLink(ILink link);

        /// <summary>
        /// Removes a transition link from the state
        /// </summary>
        /// <param name="link">the link to remove</param>
        void RemoveLink(ILink link);

        /// <summary>
        /// Removes all links of the state
        /// </summary>
        void RemoveAllLinks();

        /// <summary>
        /// The owner state-machine calls this method to examine all the links of the state and determines if it should
        /// transit to the next state.The state-machine will transit to the next state determined by the first open link
        /// of the current state. If all links of the state return false, the state-machine remains in the current state.
        /// </summary>
        /// <param name="nextState">The next state that the first open link points to</param>
        /// <returns>true: the state-machine should transit to the next step</returns>
        bool ValidateLinks(out IState nextState);

        /// <summary>
        /// Activates all the links of the state
        /// </summary>
        void EnableLinks();

        /// <summary>
        /// Deactivates all the links of the state
        /// </summary>
        void DisableLinks();
    }
}