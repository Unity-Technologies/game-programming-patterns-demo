using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using DesignPatterns.Events;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace DesignPatterns.Utilities
{
    /// <summary>
    /// Use this basic helper for loading scenes by name, index, etc.
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [Tooltip("Fades on and off to black")]
        [SerializeField] private ScreenFader m_ScreenFader;
        
        // Default loaded scene that serves as the entry point and does not unload
        private Scene m_BootstrapScene;

        // The previously loaded scene
        private Scene m_LastLoadedScene;

        // Collection of scenes additively loaded
        private List<Scene> m_AdditiveScenes = new List<Scene>();

        // Properties
        public Scene BootstrapScene => m_BootstrapScene;

        // MonoBehaviour event functions

        private void Start()
        {
            // Set scene 0 as the Bootloader/Bootstrapscene
            m_BootstrapScene = SceneManager.GetActiveScene();
            
        }

        private void OnEnable()
        {
            SceneEvents.SceneLoadedByPath += SceneEvents_LoadSceneByPath;
            SceneEvents.SceneUnloadedByPath += SceneEvents_UnloadSceneByPath;
            SceneEvents.SceneLoadedByIndex += SceneEvents_SceneIndexLoaded;
            SceneEvents.LastSceneUnloaded += SceneEvents_LastSceneUnloaded;
        }

        private void OnDisable()
        {
            SceneEvents.SceneLoadedByPath -= SceneEvents_LoadSceneByPath;
            SceneEvents.SceneUnloadedByPath -= SceneEvents_UnloadSceneByPath;
            SceneEvents.SceneLoadedByIndex -= SceneEvents_SceneIndexLoaded;
            SceneEvents.LastSceneUnloaded -= SceneEvents_LastSceneUnloaded;
        }

        // Event-handling methods

        private void SceneEvents_LastSceneUnloaded()
        {
            UnloadLastLoadedScene();
        }

        private void SceneEvents_SceneIndexLoaded(int sceneIndex)
        {
            LoadSceneAdditively(sceneIndex);
        }

        private void SceneEvents_LoadSceneByPath(string scenePath)
        {
            LoadSceneByPath(scenePath);
        }

        private void SceneEvents_UnloadSceneByPath(string scenePath)
        {
            UnloadSceneByPath(scenePath);
        }

        // Methods

        // Load a scene non-additively
        public void LoadSceneByPath(string scenePath)
        {
            StartCoroutine(LoadScene(scenePath));
        }

        // Coroutine to unload the previous scene and then load a new scene by scene path string
        public IEnumerator LoadScene(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogWarning("SceneLoader: Invalid scene name");
                yield break;
            }

            if (m_ScreenFader != null)
            {
                m_ScreenFader.FadeOut();
                yield return new WaitForSeconds(m_ScreenFader.FadeDuration);
            }
            
            yield return UnloadLastScene();
            yield return LoadAdditiveScene(scenePath);
            
            if (m_ScreenFader != null)
                m_ScreenFader.FadeIn();
        }

        // Load a scene by its index number (non-additively)
        public void LoadScene(int buildIndex)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

            if (string.IsNullOrEmpty(scenePath))
            {
                Debug.LogWarning("SceneLoader.LoadScene: invalid sceneBuildIndex");
                return;
            }

            SceneManager.LoadScene(scenePath);
        }

        // Method to load a scene by its index number (additively)
        public void LoadSceneAdditively(int buildIndex)
        {
            StartCoroutine(LoadAdditiveScene(buildIndex));
        }


        // Reload the current scene
        public void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }

        // Load the next scene by index in the Build Settings
        public void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
        }

        // Unload the last scene (go "back" from a Demo scene)
        public void UnloadLastLoadedScene()
        {
            StartCoroutine(UnloadLastScene());
        }


        // Load a scene additively by path (does not unload the last scene)
        public void LoadSceneAdditivelyByPath(string scenePath)
        {
            // Check if the scene at the given path is already loaded to prevent reloading
            Scene sceneToLoad = SceneManager.GetSceneByPath(scenePath);
            if (!sceneToLoad.IsValid())
            {
                // Track the scenes loaded additively by path
                if (!m_AdditiveScenes.Contains(sceneToLoad))
                    m_AdditiveScenes.Add(sceneToLoad);
                
                StartCoroutine(LoadAdditiveScene(scenePath));
            }
            else
            {
                Debug.LogWarning($"[SceneLoader]: Scene at path {scenePath} is already loaded.");
            }
        }

        // Coroutine to load a scene asynchronously by scene path string in Additive mode,
        // keeps the original scene as the active scene.
        private IEnumerator LoadAdditiveScene(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
            {
                yield break;
            }

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                float progress = asyncLoad.progress;
                yield return null;
            }

            m_LastLoadedScene = SceneManager.GetSceneByPath(scenePath);
            SceneManager.SetActiveScene(m_LastLoadedScene);
        }

        // Coroutine to load a Scene asynchronously by index in Additive mode,
        // keeps the original scene as the active scene.
        private IEnumerator LoadAdditiveScene(int buildIndex)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);
            yield return LoadAdditiveScene(scenePath);
        }

        // Unload by an explicit path
        public void UnloadSceneByPath(string scenePath)
        {
            
            Scene sceneToUnload = SceneManager.GetSceneByPath(scenePath);
            if (sceneToUnload.IsValid())
            {
                StartCoroutine(UnloadScene(sceneToUnload));
            }
        }
        // Unload by an explicit path immediately
        public void UnloadSceneImmediately(string scenePath)
        {
            Scene sceneToUnload = SceneManager.GetSceneByPath(scenePath);
            if (sceneToUnload.IsValid())
            {
                // Attempt to unload the scene immediately
                bool unloadSuccessful = SceneManager.UnloadScene(sceneToUnload);
                if (unloadSuccessful)
                {
                    Debug.Log($"Scene {scenePath} unloaded successfully.");
                    m_AdditiveScenes.Remove(sceneToUnload);
                }
                else
                {
                    Debug.LogWarning($"Failed to unload scene {scenePath}.");
                }
            }
            else
            {
                Debug.LogWarning($"Scene at path {scenePath} is not valid or already unloaded.");
            }
        }

        // Coroutine to unloads the previously loaded scene if it's not the bootstrap scene
        // Note: this only works for one scene and does not create a breadcrumb list backwards. Use UnloadSceneByPath if
        // needed.
        public IEnumerator UnloadLastScene()
        {
            if (!m_LastLoadedScene.IsValid())
                yield break;

            UnloadAllAdditiveScenes();
            
            if (m_LastLoadedScene != m_BootstrapScene)
                yield return UnloadScene(m_LastLoadedScene);
        }

        // Coroutine to unload a specific Scene asynchronously
        private IEnumerator UnloadScene(Scene scene)
        {
            // Break if only have one scene loaded
            if (SceneManager.sceneCount <= 1)
            {
                Debug.Log("[SceneLoader: Cannot unload only loaded scene " + scene.name);
                yield break;
            }

            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(scene);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }
        }

        // Unloads all scenes loaded additively by scene path (e.g. for DirtyFlag example)
        public void UnloadAllAdditiveScenes()
        {
            foreach (Scene scene in m_AdditiveScenes)
            {
                if (scene.IsValid() && scene != m_BootstrapScene)
                {
                    StartCoroutine(UnloadScene(scene));
                }
            }
            m_AdditiveScenes.Clear();
        }
        
        // Logs the scene path for a single loaded scene
        public static void ShowCurrentScenePath()
        {
            string scenePath = SceneManager.GetActiveScene().path;
            Debug.Log("Current scene path: " + scenePath);
        }
    }

    /// <summary>
    /// Logs console text for the .unity scene files in the project for easier copy and paste.
    /// </summary>
    public class ScenePathLogger
    {
        [MenuItem("DesignPatterns/Tools/Log All Scene Paths")]
        public static void LogAllScenePaths()
        {
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
            foreach (string assetPath in allAssetPaths)
            {
                if (Path.GetExtension(assetPath) == ".unity")
                {
                    Debug.Log(assetPath);
                }
            }
        }

        public static void LogScenePathsInBuildSettings()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                Debug.Log("Scene " + i + " path: " + scene.path);
            }
        }
    }
}