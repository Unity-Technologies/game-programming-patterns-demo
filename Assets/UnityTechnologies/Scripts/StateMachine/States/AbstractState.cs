using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StateMachines
{
    /// <summary>
    /// An abstract class that provides common functionalities for the states of state machines
    /// </summary>
    public abstract class AbstractState : IState
    {
        /// <summary>
        /// The name of the state used for debugging purposes
        /// </summary>
        public virtual string Name { get; set; }

        // Enable debug messages
        protected bool m_Debug = false;
        readonly List<ILink> m_Links = new();

        public bool DebugEnabled { get => m_Debug; set => m_Debug = value; }

        // Called when entering the state
        public virtual void Enter()
        {
            EnableLinks();
        }

        // Implement in derived classes to define behavior during state execution.
        public abstract IEnumerator Execute();

        // Called when entering the state
        public virtual void Exit()
        {
        }

        public virtual void AddLink(ILink link)
        {
            if (!m_Links.Contains(link))
            {
                m_Links.Add(link);
            }
        }

        public virtual void RemoveLink(ILink link)
        {
            if (m_Links.Contains(link))
            {
                m_Links.Remove(link);
            }
        }

        public virtual void RemoveAllLinks()
        {
            m_Links.Clear();
        }

        // Validates each link and sets the nextState if a valid link is found.
        // Returns true if a valid transition is found.
        public virtual bool ValidateLinks(out IState nextState)
        {

            if (m_Links != null && m_Links.Count > 0)
            {
                foreach (var link in m_Links)
                {
                    var result = link.Validate(out nextState);
                    if (result)
                    {
                        return true;
                    }
                }
            }

            // By default, return false without a valid IState
            nextState = null;
            return false;
        }

        public void EnableLinks()
        {
            foreach (var link in m_Links)
            {
                link.Enable();
            }
        }

        public void DisableLinks()
        {
            foreach (var link in m_Links)
            {
                link.Disable();
            }
        }

        public virtual void LogCurrentState()
        {
            if (m_Debug)
            {
                string message = "[AbstractState] Current state: " + Name + "(" + this.GetType().Name + ") ----------------------------------------------------------";
                message = message.Substring(0, 100);
                Debug.Log(message);
            }
                
        }
    }
}