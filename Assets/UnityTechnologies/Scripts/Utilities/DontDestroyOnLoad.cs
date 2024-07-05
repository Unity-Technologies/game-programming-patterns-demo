using UnityEngine;

namespace DesignPatterns.Utilities
{
    /// <summary>
    /// MonoBehaviour to preserve objects on scene loads. This can be useful
    /// for manager-level objects that need to persist over multiple scenes.
    /// If used, the user is responsible for manual cleanup using Object.Destroy.
    /// </summary>
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
