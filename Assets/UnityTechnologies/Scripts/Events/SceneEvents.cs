using System;
using UnityEngine;

namespace DesignPatterns.Events
{
    /// <summary>
    /// Public static delegates to manage scene loading/unloading (note these are "events" in the conceptual sense
    /// and not the strict C# sense).
    /// </summary>
    public static class SceneEvents
    {
        // Sequence events:
        public static Action ExitingApplication;
        
        // Main game loop has preloaded any required assets
        public static Action PreloadCompleted;

        // The progress percentage has changed
        public static Action<float> LoadProgressUpdated;

        // The current scene has been reloaded
        public static Action SceneReloaded;

        // The next scene has been loaded
        public static Action NextSceneLoaded;

        // Unload the previous scene
        public static Action LastSceneUnloaded;

        // Additively loaded the scene with the given path
        public static Action<string> SceneLoadedByPath;

        // Unloaded the scene with the given path
        public static Action<string> SceneUnloadedByPath;

        // Additively loaded the scene with given index
        public static Action<int> SceneLoadedByIndex;

    }
}
