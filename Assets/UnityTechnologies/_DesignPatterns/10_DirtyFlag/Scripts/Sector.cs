using System;
using DesignPatterns.Utilities;
using UnityEngine;
using UnityEngine.Serialization;


namespace DesignPatterns.DirtyFlag
{
    /// <summary>
    /// Each Sector manages the loading and unloading of content for a specific part of the level based on proximity to the player.
    ///
    /// This works with the GameSectors script to set/unset a dirty flag to minimize unnecessary updates.
    /// </summary>
    public class Sector : MonoBehaviour
    {
        [Header("Scene assets")] [SerializeField]
        SceneLoader m_SceneLoader;

        [SerializeField] string m_ScenePath;

        [Tooltip("Offset to transform position")]
        public Vector3 m_CenterOffset;

        [Tooltip("Minimum distance to load")] public float m_LoadRadius;

        [Header("Visualization")] [Tooltip("Material used when the sector's content is loaded.")] [SerializeField]
        Material m_ActiveMaterial;

        [Tooltip("Material used when the sector's content is unloaded.")] [SerializeField]
        Material m_InactiveMaterial;

        // Reference to the MeshRenderer for visualization
        MeshRenderer m_MeshRenderer;

        // Properties
        public bool IsLoaded { get; private set; } = false;
        public bool IsDirty { get; private set; } = false;

        void Awake()
        {
            m_MeshRenderer = GetComponent<MeshRenderer>();
            m_SceneLoader = FindFirstObjectByType<SceneLoader>();

            if (m_SceneLoader == null)
            {
                Debug.LogError("[Sector]: SceneLoader not found in the scene.");
            }

            // Reset the dirty flag to start
            Clean();

            IsLoaded = false;
        }

        // Mark the sector as needing an update
        public void MarkDirty()
        {
            IsDirty = true;

            Debug.Log("Sector " + gameObject.name + " is marked dirty");
        }

        // Load sector content
        public void LoadContent()
        {
            // Implement content loading logic
            IsLoaded = true;

            if (m_MeshRenderer != null)
                m_MeshRenderer.material = m_ActiveMaterial;

            Debug.Log($"{gameObject.name} Loading sector content...");
            
            if (!string.IsNullOrEmpty(m_ScenePath))
                m_SceneLoader.LoadSceneAdditivelyByPath(m_ScenePath);
        }

        // Unload sector content
        public void UnloadContent()
        {
            // Content unloading logic
            IsLoaded = false;

            if (m_MeshRenderer != null)
                m_MeshRenderer.material = m_InactiveMaterial;

            m_SceneLoader.UnloadSceneByPath(m_ScenePath);
            Debug.Log("Unloading sector content...");
        }

        // Check if the player is close enough to consider loading this sector
        public bool IsPlayerClose(Vector3 playerPosition)
        {
            return Vector3.Distance(playerPosition, transform.position + m_CenterOffset) <= m_LoadRadius;
        }

        // Reset the dirty flag after updating
        public void Clean()
        {
            IsDirty = false;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position + m_CenterOffset, m_LoadRadius);
        }

        void OnDestroy()
        {
            m_SceneLoader.UnloadSceneImmediately(m_ScenePath);
        }
    }
}