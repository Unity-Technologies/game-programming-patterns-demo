using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DesignPatterns.Utilities
{
    /// <summary>
    /// This component disables a GameObject when loading a new scene.
    ///

    /// </summary>
    public class DisableOnLoad : MonoBehaviour
    {
        // Inspector fields
        [Tooltip("Don't disable the GameObject when this is the active scene")]
        [SerializeField] private string m_ActiveWithinScene;
        [Tooltip("Defaults to current GameObject unless specified")]
        [SerializeField] private GameObject m_ObjectToDisable;
        [Tooltip("Shows debug messages.")]
        [SerializeField] private bool m_Debug;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManager_SceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManager_SceneLoaded;
            SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
        }

        private void Awake()
        {
            if (m_ObjectToDisable == null)
                m_ObjectToDisable = gameObject;
        }

        private void SceneManager_SceneLoaded(Scene loadedScene, LoadSceneMode mode)
        {
            if (loadedScene.name != m_ActiveWithinScene)
            {
                DisableGameObject(loadedScene);
            }
        }

        private void SceneManager_SceneUnloaded(Scene unloadedScene)
        {
            if (SceneManager.GetActiveScene().name == m_ActiveWithinScene)
            {
                EnableGameObject(unloadedScene);
            }
        }

        private void EnableGameObject(Scene scene)
        {
            m_ObjectToDisable.SetActive(true);

            if (m_Debug)
            {
                Debug.Log("[DisableOnLoad] re-enabled GameObject: " + m_ObjectToDisable.name + "; unloaded scene: " + scene.name);
            }
        }

        private void DisableGameObject(Scene scene)
        {
            m_ObjectToDisable.SetActive(false);

            if (m_Debug)
            {
                Debug.Log("[DisableOnLoad]  Disabled GameObject: " + m_ObjectToDisable.name + "; loaded scene: " + scene.name);
            }
        }

    }
}
