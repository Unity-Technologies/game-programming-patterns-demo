using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    /// <summary>
    /// Abstract base class for menu views. It provides common functionality
    /// for menu-related UI views, such as initializing navigation bars and info graphics.
    /// </summary>
    public abstract class BaseMenuView : UIView
    {
        // Shared Element IDs
        protected const string k_NavBarContainer = "button-container";
        protected const string k_InfoGraphicContainer = "right__container";

        protected const string k_ButtonTemplatePath = "button__template";    // Resource path for the Button UXML VisualTreeAsset
        protected const string k_ButtonStyleSheetPath = "button__template";    // Resource path for the Button style sheet

        // Shared fields
        protected VisualElement m_NavBarContainer;    // Element that holds the NavigationBar
        protected VisualElement m_InfoGraphicContainer;    // Element that holds the InfoGraphic
        protected InfoGraphicView m_InfoGraphic;    // Background image with text caption
        protected NavigationButtonSO[] m_ButtonData;    // ScriptableObject data defining the buttons (id, label, action, etc.)
        protected VisualTreeAsset m_ButtonAsset;    // Button UXML template
        protected StyleSheet m_ButtonStyleSheet;    // Button USS style sheet
        protected NavigationBar m_NavigationBar;    // Set of buttons that make up the menu

        // Properties
        public NavigationBar NavigationBar => m_NavigationBar;
        public NavigationButtonSO[] ButtonData { get => m_ButtonData; set => m_ButtonData = value; }
        public VisualTreeAsset ButtonAsset { get => m_ButtonAsset; set => m_ButtonAsset = value; }
        public StyleSheet ButtonStyleSheet { get => m_ButtonStyleSheet; set => m_ButtonStyleSheet = value; }


        /// <summary> Constructor for the BaseMenuView to initializes UI elements and load necessary resources.</summary>
        /// <param name="rootElement">Topmost/parent element in the UXML hierarchy</param>
        protected BaseMenuView(VisualElement rootElement) : base(rootElement)
        {
            SetVisualElements();
            LoadResources();
            InitializeNavigationBar();
        }

        /// <summary>
        /// Derived classes should override this method to load their specific resources.
        /// </summary>
        protected virtual void LoadResources()
        {
           
        }

        /// <summary>
        /// Overrides base behavior to hide the UIView and disable the NavigationBar button
        /// (stops accidental clicks during transition).
        /// </summary>
        /// <param name="delay"></param>
        public override void Hide(float delay = 0)
        {
            m_NavigationBar.EnableButtons(false);
            base.Hide(delay);
        }

        /// <summary>
        /// Show the UIView and re-enable the NavigationBar buttons.
        /// </summary>
        public override void Show()
        {
            base.Show();
            m_NavigationBar.EnableButtons(true);
        }

        /// <summary>
        /// Set references the Visual Elements.
        /// </summary>
        protected virtual void SetVisualElements()
        {
            m_NavBarContainer = m_RootElement.Q<VisualElement>(k_NavBarContainer);
            m_InfoGraphicContainer = m_RootElement.Q<VisualElement>(k_InfoGraphicContainer);
            m_InfoGraphic = new InfoGraphicView(m_InfoGraphicContainer);
        }

        /// <summary>
        /// Uses the Builder pattern to assign parameters to the NavigationBar.
        /// </summary>
        private void InitializeNavigationBar()
        {
            m_NavigationBar = NavigationBar.CreateBuilder(m_NavBarContainer)
                .WithButtonData(m_ButtonData)
                .WithEventRegistry(m_EventRegistry)
                .WithButtonAsset(ButtonAsset)
                .WithButtonStyleSheet(ButtonStyleSheet)
                .Build();

            var infoGraphicHandler = new InfoGraphicHandler(m_InfoGraphic, m_NavigationBar);
        }

        /// <summary>
        /// Dispose of resources when disabling.
        /// </summary>
        public override void Disable()
        {
            base.Disable();
            m_InfoGraphic.Disable();
        }
    }
}
