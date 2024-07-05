using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DesignPatterns.StateMachines
{
    /// <summary>
    /// Delays the state-machine for the set amount. Pass in an Action<float> to run
    /// every frame while waiting (e.g. for a loading bar on a SplashScreen).
    /// </summary>
    public class DelayState : AbstractState
    {
        readonly float m_DelayInSeconds;

        // Optional Action to run every frame while waiting
        readonly Action<float> m_ProgressUpdated;

        // Optional Action to run when execution completes
        readonly Action m_OnExit;

        /// <param name="delayInSeconds">delay in seconds</param>
        public DelayState(float delayInSeconds, Action<float> onUpdate = null, Action onExit = null, string stateName = nameof(DelayState))
        {
            m_DelayInSeconds = delayInSeconds;
            m_ProgressUpdated = onUpdate;
            m_OnExit = onExit;
            Name = stateName;
        }

        public override IEnumerator Execute()
        {
            var startTime = Time.time;

            if (m_Debug)
                base.LogCurrentState();

            while (Time.time - startTime < m_DelayInSeconds)
            {
                yield return null;
                float progressValue = (Time.time - startTime) / m_DelayInSeconds;
                m_ProgressUpdated?.Invoke(progressValue*100);
            }
        }

        public override void Exit()
        {
            m_OnExit?.Invoke();
        }
    }
}
