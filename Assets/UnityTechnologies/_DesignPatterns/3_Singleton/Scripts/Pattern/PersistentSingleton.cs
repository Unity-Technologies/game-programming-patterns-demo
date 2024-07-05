
using UnityEngine;

namespace DesignPatterns.Singleton
{
    /// <summary>
    /// A generic and persistent implementation of the Singleton design pattern. Use this for
    /// singletons that need to survive scene loading/unloading. 
    /// </summary>
    /// <typeparam name="T">The type of the MonoBehaviour that should be a Singleton.</typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        private static T s_Instance;

        public static T Instance
        {
            get
            {
                // If the singleton instance does not exist, try to find it in the scene
                if (s_Instance == null)
                {
                    s_Instance = FindFirstObjectByType<T>();

                    // Create a new GameObject with the Type T if it does not exist
                    if (s_Instance == null)
                    {
                        GameObject singletonObject = new GameObject();
                        s_Instance = singletonObject.AddComponent<T>();
                        
                        // Name the singleton instance for the Type
                        singletonObject.name = typeof(T).ToString();

                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return s_Instance;
            }
        }

        protected virtual void Awake()
        {
            if (s_Instance == null)
            {
                s_Instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (s_Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
