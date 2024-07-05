using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.Utilities
{
    public static class ResourceLoader
    {
        /// <summary>
        /// Loads and returns an array of ScriptableObjects of type T from the given resource path.
        /// </summary>
        /// <typeparam name="T">The type of ScriptableObject to load.</typeparam>
        /// <param name="resourcePath">The path to the resources.</param>
        /// <returns>An array of ScriptableObjects of type T.</returns>
        public static T[] LoadScriptableObjects<T>(string resourcePath) where T : ScriptableObject
        {
            T[] loadedItems = Resources.LoadAll<T>(resourcePath);
            if (loadedItems.Length == 0)
            {
                Debug.LogWarning($"[ResourceLoader] LoadScriptableObjects: Failed to load resources at '{resourcePath}'.");
            }
            return loadedItems;
        }

        /// <summary>
        /// Loads and returns a single ScriptableObject from the given resource path.
        /// </summary>
        /// <param name="resourcePath">The path to the ScriptableObject.</param>
        /// <returns>The loaded ScriptableObject.</returns>
        public static T LoadScriptableObject<T>(string resourcePath) where T : ScriptableObject
        {
            T asset = Resources.Load<T>(resourcePath);
            if (asset == null)
            {
                Debug.LogWarning($"[ResourceLoader] LoadScriptableObject: Failed to load ScriptableObject at '{resourcePath}'.");
            }
            return asset;
        }

        /// <summary>
        /// Loads and returns a VisualTreeAsset from the given resource path.
        /// </summary>
        /// <param name="resourcePath">The path to the VisualTreeAsset.</param>
        /// <returns>The loaded VisualTreeAsset.</returns>
        public static VisualTreeAsset LoadVisualTreeAsset(string resourcePath)
        {
            VisualTreeAsset asset = Resources.Load<VisualTreeAsset>(resourcePath);
            if (asset == null)
            {
                Debug.LogWarning($"[ResourceLoader] LoadVisualTreeAsset: Failed to load VisualTreeAsset at '{resourcePath}'.");
            }
            return asset;
        }

        /// <summary>
        /// Loads and returns a StyleSheet from the given resource path.
        /// </summary>
        /// <param name="resourcePath">The path to the StyleSheet.</param>
        /// <returns>The loaded StyleSheet.</returns>
        public static StyleSheet LoadStyleSheet(string resourcePath)
        {
            StyleSheet styleSheet = Resources.Load<StyleSheet>(resourcePath);
            if (styleSheet == null)
            {
                Debug.LogWarning($"[ResourceLoader] LoadStyleSheet: Failed to load StyleSheet at '{resourcePath}'.");
            }
            return styleSheet;
        }
    }
}
