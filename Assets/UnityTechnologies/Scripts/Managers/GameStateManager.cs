using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.StateMachines;
using DesignPatterns.Utilities;
using DesignPatterns.Events;


namespace DesignPatterns
{
    /// <summary>
    /// A GameStateManager controls the overall flow of the application using a state machine.
    /// 
    /// Use this class to define how each State will transition to the next. Each state can
    /// transition to the next state when receiving an event or reaching a specific condition.
    /// </summary>
    public class GameStateManager: MonoBehaviour
    {
        // Inspector fields
        [Header("Preload (Splash Screen)")]
        [Tooltip("Prefab assets that load first. These can include level management Prefabs or textures, sounds, etc.")]
        [SerializeField] GameObject[] m_PreloadedAssets;  // Required assets to load before the GameStateManager sets up states

        [Header("Menu Events")]
        [Tooltip("Notify listeners that the SOLID principles menu is active")]
        [SerializeField] BaseEventSO m_SolidMenuShown;
        [Tooltip("Notify listeners that the Design Patterns menu is active")]
        [SerializeField] BaseEventSO m_DesignPatternMenuShown; 
        [Tooltip("Notify listeners that the Resource screen with additional links is active")]
        [SerializeField] BaseEventSO m_ResourcesMenuShown;
        [Space]
        [Tooltip("Backs out of the currently active UI screen")]
        [SerializeField] BaseEventSO m_ScreenClosed;

        [Header("SOLID Scene Events")]
        [Tooltip("ScriptableObject-based events to load the SOLID demo scenes")]
        [SerializeField] List<SceneEventSO> m_SolidSceneLoadEvents; 

        [Header("Design Pattern Scene Events")]
        [Tooltip("ScriptableObject-based events to load the Design Pattern demo scenes")]
        [SerializeField] List<SceneEventSO> m_DesignPatternSceneLoadEvents; 

        [Space(10)]
        [Tooltip("Debug state changes in the console")]
        [SerializeField] bool m_Debug;

        StateMachine m_StateMachine = new StateMachine();

        // Define all States here
        IState m_MainMenuState;   // Show the main menu screen
        IState m_SolidMenuState;   // Show the Solid Menu screen
        IState m_DesignPatternMenuState;   // Show the Design Patterns Menu screen
        IState m_ResourcesMenuState;   // Show the Resources Menu screen
        LoadSceneState m_SolidDemoState;   // Show/load a SOLID demo scene
        LoadSceneState m_DesignPatternDemoState;  // Show/load a Design Patterns demo scene

        SceneLoader m_SceneLoader;

        private void Start()
        {
            Initialize();
        }

        // 
        private void Initialize()
        {
            // Set up the coroutines helper for non-MonoBehaviours
            Coroutines.Initialize(this);

            // Load any assets or prefabs required to start the game
            InstantiatePreloadedAssets();

            // Define the Game States
            SetStates();
            AddLinks();

            // Start the State Machine
            RunStateMachine();

            // Event subscriptions
            if (m_SolidDemoState != null)
                m_SolidDemoState.StateExited += SolidDemoState_StateExited;

            if (m_DesignPatternDemoState != null)
                m_DesignPatternDemoState.StateExited += DesignPatternDemoState_StateExited;
        }

        private void SolidDemoState_StateExited()
        {
            SceneEvents.LastSceneUnloaded?.Invoke();
        }

        private void DesignPatternDemoState_StateExited()
        {
            SceneEvents.LastSceneUnloaded?.Invoke();
        }

        // Event unsubscriptions
        private void OnDisable()
        {
            if (m_SolidDemoState != null)
                m_SolidDemoState.StateExited -= SolidDemoState_StateExited;

            if (m_DesignPatternDemoState != null)
                m_DesignPatternDemoState.StateExited -= SolidDemoState_StateExited;
        }


        // Begin tracking game states
        private void RunStateMachine()
        {
            // Start with the main menu scene
            m_StateMachine.Run(m_MainMenuState);

            // Notify other systems using the UIEvents event channel
            UIEvents.MainMenuShown?.Invoke();
        }

        // Use this to preload any assets. This is an opportunity to load any prefabs (with textures, models, etc.) 
        // in advance to avoid loading during gameplay 
        private void InstantiatePreloadedAssets()
        {
            foreach (var asset in m_PreloadedAssets)
            {
                if (asset != null)
                    Instantiate(asset);
            }
        }

        /// <summary>
        /// Defines the runtime states of the game/tutorial application.
        /// </summary>
        private void SetStates()
        {
            // Menu states (optional names added for debugging)
            m_MainMenuState = new State(null, "MainMenuState", m_Debug);
            m_SolidMenuState = new State(null, "SolidMenuState", m_Debug);
            m_DesignPatternMenuState = new State(null, "DesignPatternsMenuState", m_Debug);
            m_ResourcesMenuState = new State(null, "ResourcesMenuState", m_Debug);

            // Demo scene states (loads up a separate scene additively)
            m_SolidDemoState = new LoadSceneState(m_SceneLoader, null);
            m_DesignPatternDemoState = new LoadSceneState(m_SceneLoader, null);
        }

        /// <summary>
        /// This defines how the different states can transition to other states.
        /// </summary>
        private void AddLinks()
        {
            // Transition from main menu to submenu states
            m_MainMenuState.AddLink(new EventSOLink(m_SolidMenuShown, m_SolidMenuState));
            m_MainMenuState.AddLink(new EventSOLink(m_DesignPatternMenuShown, m_DesignPatternMenuState));
            m_MainMenuState.AddLink(new EventSOLink(m_ResourcesMenuShown, m_ResourcesMenuState));

            // Back out from submenu to MainMenu state
            m_SolidMenuState.AddLink(new EventSOLink(m_ScreenClosed, m_MainMenuState));
            m_DesignPatternMenuState.AddLink(new EventSOLink(m_ScreenClosed, m_MainMenuState));
            m_ResourcesMenuState.AddLink(new EventSOLink(m_ScreenClosed, m_MainMenuState));

            // Transition from menu to demo play states for Solid
            foreach (SceneEventSO sceneEvent in m_SolidSceneLoadEvents)
            {
                m_SolidMenuState.AddLink(new SceneEventSOLink(sceneEvent, m_SolidDemoState));
            }

            // Transition from menu to demo play states for Design Patterns
            foreach (SceneEventSO sceneEvent in m_DesignPatternSceneLoadEvents)
            {
                m_DesignPatternMenuState.AddLink(new SceneEventSOLink(sceneEvent, m_DesignPatternDemoState));
            }

            // Return from demo scenes to menu states
            m_SolidDemoState.AddLink(new EventSOLink(m_ScreenClosed, m_SolidMenuState));
            m_DesignPatternDemoState.AddLink(new EventSOLink(m_ScreenClosed, m_DesignPatternMenuState));

        }
    }
}
