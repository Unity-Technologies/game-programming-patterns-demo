using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace DesignPatterns.Singleton
{
    /// <summary>
    /// Provides a generic implementation of the Singleton design pattern for MonoBehaviour types.
    /// Ensures that only one instance of the Singleton exists within the application at any time.
    /// If no instance is found upon access, this script creates the Instance.
    /// </summary>
    /// <typeparam name="T">The type of the MonoBehaviour that should be a Singleton.</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Component
    {

        [Tooltip("Delays the removal of duplicate instances until explicitly invoked (for demo use only).")]
        [SerializeField]
        private bool m_DelayDuplicateRemoval;


        private static T s_Instance;

        public static T Instance
        {
            get
            {
                if (s_Instance == null)
                {
                    s_Instance = (T)FindFirstObjectByType(typeof(T));

                    if (s_Instance == null)
                    {
                        SetupInstance();
                    }
                    else
                    {
                        string typeName = typeof(T).Name;

                        Debug.Log("[Singleton] " + typeName + " instance already created: " +
                                  s_Instance.gameObject.name);
                    }
                }

                return s_Instance;
            }
        }

        public virtual void Awake()
        {
            // For demo purposes, this flag can delay the removal of duplicates
            if (!m_DelayDuplicateRemoval)
                RemoveDuplicates();
        }

        private void OnEnable()
        {
            // Clear the single instance when unloading the current scene
            SceneManager.sceneUnloaded += SceneManager_SceneUnloaded;
        }

        private void OnDisable()
        {
            if (s_Instance == this as T)
            {
                SceneManager.sceneUnloaded -= SceneManager_SceneUnloaded;
            }
        }

        private static void SetupInstance()
        {
            // lazy instantiation
            s_Instance = (T)FindFirstObjectByType(typeof(T));

            if (s_Instance == null)
            {
                GameObject gameObj = new GameObject();
                gameObj.name = typeof(T).Name;

                s_Instance = gameObj.AddComponent<T>();
                DontDestroyOnLoad(gameObj);
            }
        }

        public void RemoveDuplicates()
        {
            if (s_Instance == null)
            {
                s_Instance = this as T;

                // Use DontDestroyOnLoad to make persistent but clean up/dispose manually
                //DontDestroyOnLoad(gameObject);
            }
            else if (s_Instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Event-handling method
        
        // Destroy singleton when unloading scene (for demo use only)
        private void SceneManager_SceneUnloaded(Scene scene)
        {
            if (s_Instance != null)
                Destroy(s_Instance.gameObject);
            
            s_Instance = null;
        }
    }
}