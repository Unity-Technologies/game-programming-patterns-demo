using System;
using UnityEngine;
using DesignPatterns.StatePattern;
using UnityEngine.Serialization;

namespace DesignPatterns.DirtyFlag
{
    /// <summary>
    /// Manages the loading and unloading of game sectors based on the player proximity.
    /// Utilizes the Dirty Flag pattern to optimize performance by minimizing unnecessary updates.
    /// </summary>
    public class GameSectors : MonoBehaviour
    {
        [Tooltip("Reference to the PlayerController")]
        public PlayerController m_Player;
        [Tooltip("Array of all sectors in the game")]
        public Sector[] m_Sectors;

        // Checks each sector's proximity to the player and updates its loaded/unloaded state.
        private void Update()
        {

            if (m_Player == null)
                return;

            foreach (Sector sector in m_Sectors)
            {
                bool isPlayerClose = sector.IsPlayerClose(m_Player.transform.position);

                // Check if the sector's state needs to change
                if (isPlayerClose != sector.IsLoaded)
                {
                    sector.MarkDirty();
                }

                // Update the sector based on its dirty flag, skipping expensive loading/unloading
                // operations if unnecessary
                if (sector.IsDirty)
                {
                    if (isPlayerClose)
                    {
                        sector.LoadContent();
                    }
                    else
                    {
                        sector.UnloadContent();
                    }

                    // Reset the dirty flag
                    sector.Clean();
                }
            }
    }
        
        private void UnloadAllScenes()
        {
            foreach (Sector sector in m_Sectors)
            {
                if (sector.IsLoaded)
                {
                    sector.UnloadContent();
                }
            }
        }

        void OnDestroy()
        {
            // LogLoadedSectors();
            // UnloadAllScenes();
            //
            // Debug.Log("Unloading all sectors...");
        }
        
        // Diagnostic tool to log all sectors where IsLoaded is true
        private void LogLoadedSectors()
        {
            Debug.Log("Logging loaded sectors:");
            foreach (Sector sector in m_Sectors)
            {
                if (sector.IsLoaded)
                {
                    Debug.Log($"Sector loaded: {sector.name}");
                }
            }
        }
    }
}