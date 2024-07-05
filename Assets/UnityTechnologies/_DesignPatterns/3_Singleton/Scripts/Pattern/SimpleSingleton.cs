using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Singleton
{
    /// <summary>
    /// Implements a simple version of the Singleton pattern, that ensures
    /// that only one instance of SimpleSingleton exists. Use the static Instance
    /// variable for global access.
    ///
    /// If more than one instance is attempted to be created, the new instances 
    /// are destroyed. 
    /// </summary>
    public class SimpleSingleton : MonoBehaviour
    {
        // global access
        public static SimpleSingleton Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                // if Instance is already set, destroy this duplicate
                Destroy(gameObject);
            }
            else
            {
                // if Instance is not set, make this instance the singleton
                Instance = this;
            }
        }
    }
}  
