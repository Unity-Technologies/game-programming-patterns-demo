using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Singleton
{
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
