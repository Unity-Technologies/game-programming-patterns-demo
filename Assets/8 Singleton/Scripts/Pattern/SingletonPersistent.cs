using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Singleton
{
    public class Singleton : MonoBehaviour
    {
        private static Singleton _instance;

        // global access
        public static Singleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    SetupInstance();
                }
                return _instance;
            }
        }

        private void Awake()
        {
            // if this is the first instance, make this the persistent singleton
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            // otherwise, remove any duplicates
            else
            {
                Destroy(gameObject);
            }
        }

        private static void SetupInstance()
        {
            // lazy instantiation
            _instance = FindObjectOfType<Singleton>();

            if (_instance == null)
            {
                GameObject gameObj = new GameObject();
                gameObj.name = "Singleton";
                _instance = gameObj.AddComponent<Singleton>();
                DontDestroyOnLoad(gameObj);
            }
        }



    }
}
