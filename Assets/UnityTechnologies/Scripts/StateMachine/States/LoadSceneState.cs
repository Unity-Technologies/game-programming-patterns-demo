using DesignPatterns.StateMachines;
using System;
using System.Collections;
using DesignPatterns.Utilities;
using UnityEngine;
using DesignPatterns.Events;
using UnityEditorInternal;

namespace DesignPatterns
{
    /// <summary>
    /// State that loads another scene additively
    /// </summary>
    public class LoadSceneState : AbstractState
    {
        // Signal that Scene has finished loading
        public event Action LoadCompleted;

        // Signal for transitioning out of this State
        public event Action StateExited;

        string m_ScenePath;
        SceneLoader m_SceneLoader;

        public override string Name => $"{nameof(LoadSceneState)}: {m_ScenePath}";

        public string ScenePath
        {
            get => m_ScenePath;
            set => m_ScenePath = value;
        }

        /// <param name="sceneLoader">The SceneLoader for the current loading operation</param>
        /// <param name="scene">The path to the scene</param>
        /// <param name="loadCompleted">An action that is invoked when scene loading is finished</param>
        public LoadSceneState(SceneLoader sceneLoader, Action loadCompleted = null)
        {
            m_SceneLoader = sceneLoader;
            LoadCompleted = loadCompleted;
            
        }

        public override IEnumerator Execute()
        {
            if (m_SceneLoader != null)
            {
                yield return m_SceneLoader.LoadScene(m_ScenePath);
            }

            LoadCompleted?.Invoke();

            if (m_Debug)
                base.LogCurrentState();
        }

        public override void Exit()
        {
            base.Exit();
            StateExited?.Invoke();

            if (m_Debug)
                Debug.Log("Exiting state: " + this.Name);
        }
    }
}