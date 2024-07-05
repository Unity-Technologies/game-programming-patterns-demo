using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace DesignPatterns.Core.Editor
{
    /// <summary>
    /// This class auto-loads a bootstrap screen (first scene in Build Settings) while working in the Editor.
    /// It also adds menu items to toggle behavior. 
    /// </summary>
    /// 
    // This SceneBootstrapper is adapted from here:
    // https://github.com/Unity-Technologies/com.unity.multiplayer.samples.coop/blob/main/Assets/Scripts/Editor/SceneBootstrapper.cs

    [InitializeOnLoad]
    public class SceneBootstrapper
    {
        // Use these keys for Editor preferences
        private const string k_PreviousScene = "PreviousScene";
        private const string k_ShouldLoadBootstrap = "LoadBootstrapScene";

        // These appear as menu names
        private const string k_LoadBootstrapMenu = "DesignPatterns/Load Bootstrap Scene On Play";
        private const string k_DontLoadBootstrapMenu = "DesignPatterns/Don't Load Bootstrap Scene On Play";

        // This gets the bootstrap scene, which must be first scene in Build Settings
        private static string BootstrapScene => EditorBuildSettings.scenes[0].path;

        // This string is the scene name where we entered Play mode 
        private static string PreviousScene
        {
            get => EditorPrefs.GetString(k_PreviousScene);
            set => EditorPrefs.SetString(k_PreviousScene, value);
        }

        // Is the bootstrap behavior enabled?
        private static bool ShouldLoadBootstrapScene
        {
            get => EditorPrefs.GetBool(k_ShouldLoadBootstrap, true);
            set => EditorPrefs.SetBool(k_ShouldLoadBootstrap, value);
        }

        // A static constructor runs with InitializeOnLoad attribute
        static SceneBootstrapper()
        {
            // Listen for the Editor changing play states
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        // This runs when the Editor changes play state (e.g. entering Play mode, exiting Play mode)
        private static void OnPlayModeStateChanged(PlayModeStateChange playModeStateChange)
        {
            // Do nothing if disabled from the menus
            if (!ShouldLoadBootstrapScene)
            {
                return;
            }

            switch (playModeStateChange)
            {
                // This loads bootstrap scene when entering Play mode
                case PlayModeStateChange.ExitingEditMode:

                    PreviousScene = EditorSceneManager.GetActiveScene().path;

                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() && IsSceneInBuildSettings(BootstrapScene))
                    {
                        EditorSceneManager.OpenScene(BootstrapScene);
                    }
                    break;

                // This restores the PreviousScene when exiting Play mode
                case PlayModeStateChange.EnteredEditMode:

                    if (!string.IsNullOrEmpty(PreviousScene))
                    {
                        EditorSceneManager.OpenScene(PreviousScene);
                    }
                    break;
            }
        }

        // These menu items toggle behavior.

        // This adds a menu item called "Load Bootstrap Scene On Play" to the GameSystems menu and
        // enables the behavior if selected.
        [MenuItem(k_LoadBootstrapMenu)]
        private static void EnableBootstrapper()
        {
            ShouldLoadBootstrapScene = true;
        }

        // Validates the above function and menu item, which grays out if ShouldLoadBootstrapScene is true.
        [MenuItem(k_LoadBootstrapMenu, true)]
        private static bool ValidateEnableBootstrapper()
        {
            return !ShouldLoadBootstrapScene;
        }

        // Adds a menu item called "Don't Load Bootstrap Scene On Play" to the GameSystems menu and
        // disables the behavior if selected.
        [MenuItem(k_DontLoadBootstrapMenu)]
        private static void DisableBootstrapper()
        {
            ShouldLoadBootstrapScene = false;
        }

        // Validates the above function and menu item, which grays out if ShouldLoadBootstrapScene is false.
        [MenuItem(k_DontLoadBootstrapMenu, true)]
        private static bool ValidateDisableBootstrapper()
        {
            return ShouldLoadBootstrapScene;
        }

        // Is a scenePath a valid scene in the File > Build Settings?
        private static bool IsSceneInBuildSettings(string scenePath)
        {
            if (string.IsNullOrEmpty(scenePath))
                return false;

            foreach (var scene in EditorBuildSettings.scenes)
            {
                if (scene.path == scenePath)
                {
                    return true;
                }
            }

            return false;
        }

        // This is a more compact version of the same IsSceneInBuildSettings logic:

        //private static bool IsSceneInBuildSettings(string scenePath)
        //{
        //    return !string.IsNullOrEmpty(scenePath) &&
        //           System.Array.Exists(EditorBuildSettings.scenes, scene => scene.path == scenePath);
        //}

    }
}
