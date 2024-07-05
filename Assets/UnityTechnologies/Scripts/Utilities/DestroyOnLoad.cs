using UnityEngine;
using UnityEngine.SceneManagement;

namespace DesignPatterns.Utilities
{
    /// <summary>
    /// This component marks temporary objects in the scene that can be destroyed when
    /// loading into another scene.
    ///
    /// This is useful when loading gameplay scenes additively. Attach this to any GameObject
    /// only needed for testing (e.g. cameras) but not necessary when loading into another scene.
    /// </summary>
    public class DestroyOnLoad : MonoBehaviour
    {
        // Inspector fields
        [Tooltip("Don't destroy the GameObject when this is the active scene")]
        [SerializeField] private string m_ActiveWithinScene;
        [Tooltip("Defaults to current GameObject unless specified")]
        [SerializeField] private GameObject m_ObjectToDestroy;
        [Tooltip("Shows debug messages.")]
        [SerializeField] private bool m_Debug;

        private void Start()
        {
            if (SceneManager.GetActiveScene().name != m_ActiveWithinScene)
            {
                if (m_ObjectToDestroy == null)
                    m_ObjectToDestroy = gameObject;

                Destroy(m_ObjectToDestroy);

                if (m_Debug)
                {
                    Debug.Log("Active scene: " + SceneManager.GetActiveScene().name);
                    Debug.Log("Do not destroy in scene: " + m_ActiveWithinScene);
                    Debug.Log("Destroy on load: " + m_ObjectToDestroy);
                }
            }
        }
    }
}
