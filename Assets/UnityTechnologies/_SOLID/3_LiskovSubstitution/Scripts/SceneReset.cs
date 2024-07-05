using UnityEngine;
using UnityEngine.SceneManagement;

namespace DesignPatterns
{
    public class SceneReset : MonoBehaviour
    {
        [Tooltip("Key to reset scene / reload Prefab")]
        [SerializeField] private KeyCode m_KeyCode = KeyCode.R;
        [SerializeField] private GameObject m_PrefabToLoad;

        private GameObject m_PrefabInstance;
        void Start()
        {
            InstantiatePrefab();
        }

        private void InstantiatePrefab()
        {
            if (m_PrefabInstance != null)
                Destroy((m_PrefabInstance));
            
            if (m_PrefabToLoad != null)
            {
                m_PrefabInstance = Instantiate(m_PrefabToLoad, Vector3.zero, Quaternion.identity);
                
                // Move the instantiated prefab to the scene of this GameObject
                SceneManager.MoveGameObjectToScene(m_PrefabInstance, gameObject.scene);
            }
        }

        void Update()
        {
            if (Input.GetKey(m_KeyCode))
            {
                InstantiatePrefab();
            }
        }
    }
}
