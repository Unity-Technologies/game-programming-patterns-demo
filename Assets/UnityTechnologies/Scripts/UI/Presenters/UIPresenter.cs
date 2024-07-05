using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.Utilities;
using DesignPatterns.Events;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    /// <summary>
    /// The UI Manager manages the UI screens (View base class) using GameEvents paired
    /// to each View screen. A stack maintains a history of previously shown screens, so
    /// the UI Manager can "go back" until it reaches the default UI screen, the home screen.
    ///
    /// To add a new UIScreen under the UIManager's management:
    ///    -Define a new UIView field
    ///    -Create a new instance of that screen in Initialize (e.g. new MainMenuView(root.Q<VisualElement>("main__screen-container"));
    ///    -Register the UIScreen in the RegisterScreens method
    ///    -Subscribe/unsubscribe from the appropriate UIEvent to show the screen
    ///
    /// Alternatively, use Reflection to add the UIView to the RegisterScreens method
    /// </summary>
    public class UIPresenter : MonoBehaviour
    {
        [Tooltip("Required UI Document")]
        [SerializeField] UIDocument m_UIDocument;

        // Primary modal screen (e.g. main menu)
        UIView m_MainMenuView;

        // Project specific screens:
        // Menu screen for SOLID principles demos
        UIView m_SolidMenuView;

        // Menu screen for design patterns demos
        UIView m_PatternMenuView;

        // Menu screen to links to other resources
        UIView m_ResourcesMenuView;

        // One of the "game levels" from either the Solid or Design Patterns menus 
        UIView m_GameSceneView;

        // The currently active UIScreen
        UIView m_CurrentView;

        // A stack of previously displayed UIScreens
        Stack<UIView> m_History = new Stack<UIView>();

        // A list of all Views to show/hide
        List<UIView> m_Views = new List<UIView>();

        public UIView CurrentView => m_CurrentView;
        public UIDocument UIDocument => m_UIDocument;

        // Register event listeners to game events
        private void OnEnable()
        {
            SubscribeToEvents();
            Initialize();
        }

        // Unregister the listeners to prevent errors
        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            UIEvents.MainMenuShown += UIEvents_HomeViewShown;
            UIEvents.SolidMenuShown += UIEvents_SolidDemoViewShown;
            UIEvents.DesignPatternsMenuShown += UIEvents_PatternsDemoViewShown;
            UIEvents.ResourcesMenuShown += UIEvents_ResourcesViewShown;

            UIEvents.ScreenClosed += UIEvents_ScreenClosed;
            UIEvents.URLOpened += UIEvents_UrlOpened;

            SceneEvents.SceneLoadedByPath += SceneEvents_SceneLoadedByPath;
        }

        private void UnsubscribeFromEvents()
        {
            UIEvents.MainMenuShown -= UIEvents_HomeViewShown;
            UIEvents.SolidMenuShown -= UIEvents_SolidDemoViewShown;
            UIEvents.DesignPatternsMenuShown -= UIEvents_PatternsDemoViewShown;
            UIEvents.ResourcesMenuShown -= UIEvents_ResourcesViewShown;

            UIEvents.ScreenClosed -= UIEvents_ScreenClosed;
            UIEvents.URLOpened -= UIEvents_UrlOpened;

            SceneEvents.SceneLoadedByPath -= SceneEvents_SceneLoadedByPath;
        }

        // Event-handling methods


        // Clear the History and make the HomeScreen (MainMenu) the only View
        public void UIEvents_HomeViewShown()
        {
            m_CurrentView = m_MainMenuView;

            HideScreens();
            m_History.Push(m_MainMenuView);
            m_MainMenuView.Show();
        }

        private void UIEvents_ResourcesViewShown()
        {
            Show(m_ResourcesMenuView, true);
        }

        private void UIEvents_PatternsDemoViewShown()
        {
            Show(m_PatternMenuView, true);
        }

        private void UIEvents_SolidDemoViewShown()
        {
            Show(m_SolidMenuView, true);
        }

        // Remove the top UI screen from the stack and make that active (i.e., go back one screen)
        public void UIEvents_ScreenClosed()
        {
            if (m_History.Count != 0)
            {
                Show(m_History.Pop(), false);
            }
        }

        // Open a link to a given string URL
        private void UIEvents_UrlOpened(string link)
        {
            Application.OpenURL(link);
        }

        // Event handler for scene load events (from Solid or Design Pattern menu)
        private void SceneEvents_SceneLoadedByPath(string demoScenePath)
        {
            // Show a basic gameplay UI
            Show(m_GameSceneView, true);
        }

        // Methods

        // Clears history and hides all Views except the Start Screen
        private void Initialize()
        {
            // Because non-MonoBehaviours can't run coroutines, the Coroutines helper utility allows us to
            // designate a MonoBehaviour to manage starting/stopping coroutines
            Coroutines.Initialize(this);

            // Validate inspector fields for required dependencies
            NullRefChecker.Validate(this);

            VisualElement root = m_UIDocument.rootVisualElement;

            m_MainMenuView = new MainMenuView(root.Q<VisualElement>("main__screen-container"));
            m_SolidMenuView = new SolidDemoView(root.Q<VisualElement>("solid__screen-container"));
            m_PatternMenuView = new PatternsDemoView(root.Q<VisualElement>("design-patterns__screen-container"));
            m_ResourcesMenuView = new ResourcesView(root.Q<VisualElement>("resources__screen-container"));
            m_GameSceneView = new GameSceneView(root.Q<VisualElement>("game-scene__screen-container"));
            
            RegisterScreens();
            HideScreens();
            UIEvents_HomeViewShown();
        }

        // Store each UIScreen into a master list so we can hide all of them easily.
        private void RegisterScreens()
        {
            m_Views = new List<UIView>
            {
                // Main Menu Screen
                m_MainMenuView,

                // Add additional screens here:
                m_SolidMenuView,
                m_PatternMenuView,
                m_ResourcesMenuView,
                m_GameSceneView
            };
        }

        // Clear history and hide all Views
        private void HideScreens()
        {
            m_History.Clear();

            foreach (UIView screen in m_Views)
            {
                screen.Hide();
            }
        }

        // Finds the first registered UI View of the specified type T
        public T GetScreen<T>() where T : UIView
        {
            foreach (var screen in m_Views)
            {
                if (screen is T typeOfScreen)
                {
                    return typeOfScreen;
                }
            }
            return null;
        }

        // Shows a View of a specific type T, with the option to add it
        // to the history stack
        public void Show<T>(bool keepInHistory = true) where T : UIView
        {
            foreach (var screen in m_Views)
            {
                if (screen is T)
                {
                    Show(screen, keepInHistory);
                    break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="screen"></param>
        /// <param name="keepInHistory"></param>
        public void Show(UIView screen, bool keepInHistory = true)
        {
            if (screen == null)
                return;

            if (m_CurrentView != null)
            {
                if (!screen.IsTransparent)
                    m_CurrentView.Hide();

                if (keepInHistory)
                {
                    m_History.Push(m_CurrentView);
                }
            }

            screen.Show();
            m_CurrentView = screen;
        }

        /// <summary>
        /// Shows a UIScreen with the keepInHistory always enabled.
        /// </summary>
        /// <param name="view"></param>
        public void Show(UIView view)
        {
            Show(view, true);
        }
    }
}

