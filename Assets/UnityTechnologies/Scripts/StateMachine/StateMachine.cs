using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StateMachines
{
    /// <summary>
    /// A Generic state machine, adapted from the Runner template
    /// https://unity.com/features/build-a-runner-game
    /// </summary>
    public class StateMachine
    {
        // The current state the statemachine is in
        public IState CurrentState { get; private set; }
        
        /// <summary>
        /// Finalizes the previous state and then runs the new state
        /// </summary>
        /// <param name="state"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public virtual void SetCurrentState(IState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            if (CurrentState != null && m_CurrentPlayCoroutine != null) 
            {
                //interrupt currently executing state
                Skip();
            }
            
            CurrentState = state;
            
            Utilities.Coroutines.StartCoroutine(Play());
        }

        Coroutine m_CurrentPlayCoroutine;
        bool m_PlayLock;
        /// <summary>
        /// Runs the life cycle methods of the current state.
        /// </summary>
        /// <returns></returns>
        IEnumerator Play()
        {
            if (!m_PlayLock)
            {
                m_PlayLock = true;
                
                CurrentState.Enter();

                //keep a ref to execute coroutine of the current state
                //to support stopping it later.
                m_CurrentPlayCoroutine = Utilities.Coroutines.StartCoroutine(CurrentState.Execute());

                yield return m_CurrentPlayCoroutine;
                
                m_CurrentPlayCoroutine = null;
            }
        }

        /// <summary>
        /// Interrupts the execution of the current state and finalizes it.
        /// </summary>
        /// <exception cref="Exception"></exception>
        void Skip()
        {
            if (CurrentState == null)
                throw new Exception($"{nameof(CurrentState)} is null!");
            
            if (m_CurrentPlayCoroutine != null)
            {
                Utilities.Coroutines.StopCoroutine(ref m_CurrentPlayCoroutine);
                //finalize current state
                CurrentState.Exit();
                m_CurrentPlayCoroutine = null;
                m_PlayLock = false;
            }
        }
        
        public virtual void Run(IState state)
        {
            SetCurrentState(state);
            Run();
        }
        
        Coroutine m_LoopCoroutine;
        /// <summary>
        /// Turns on the main loop of the StateMachine.
        /// This method does not resume previous state if called after Stop()
        /// and the client needs to set the state manually.
        /// </summary>
        public virtual void Run() 
        {
            if (m_LoopCoroutine != null) //already running
                return;
            
            m_LoopCoroutine = Utilities.Coroutines.StartCoroutine(Loop());
        }

        /// <summary>
        /// Turns off the main loop of the StateMachine
        /// </summary>
        public void Stop()
        {
            if (m_LoopCoroutine == null) //already stopped
                return;
            
            if (CurrentState != null && m_CurrentPlayCoroutine != null) 
            {
                //interrupt currently executing state
                Skip();
            }
            
            Utilities.Coroutines.StopCoroutine(ref m_LoopCoroutine);
            CurrentState = null;
        }

        /// <summary>
        /// The main update loop of the StateMachine.
        /// It checks the status of the current state and its link to provide state sequencing
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator Loop()
        {
            while (true)
            {
                if (CurrentState != null && m_CurrentPlayCoroutine == null) //current state is done playing
                {
                    if (CurrentState.ValidateLinks(out var nextState))
                    {
                        if (m_PlayLock)
                        {
                            //finalize current state
                            CurrentState.Exit();
                            m_PlayLock = false;
                        }

                        CurrentState.DisableLinks();
                        SetCurrentState(nextState);
                        CurrentState.EnableLinks();
                    }
                }

                yield return null;
            }
        }

        public bool IsRunning => m_LoopCoroutine != null;
    }
}
