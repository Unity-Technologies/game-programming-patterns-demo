using DesignPatterns.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

namespace DesignPatterns.UI
{
    /// <summary>
    /// NavigationBar is a customizable UI component which defines the button information (text, action, etc.) in ScriptableObjects.
    /// Each button uses a VisualTreeAsset template and USS Style Sheet to determine its appearance and layout. The Builder pattern
    /// facilitates step-by-step construction and customization.
    /// 
    /// - This supports the custom EventRegistry utility to manage event callbacks for buttons (ClickEvent, MouseEnter, MouseExit)
    /// 
    /// - The NavigationBar can notify a supporting InfoGraphic (graphics and text) through events.
    /// </summary>
    public class NavigationBar
    {
        private NavigationButtonSO[]
            m_ButtonData; // ScriptableObjects that hold button information (id, label text, action, etc.)

        private VisualTreeAsset m_ButtonAsset; // UXML for button VisualTreeAsset
        private StyleSheet m_ButtonStyleSheet; // Stylesheet for individual button template
        private VisualElement m_Container; // Element containing the navigation bar buttons
        private EventRegistry m_EventRegistry; // Utility for registering/unregistering callbacks

        private List<Button> m_Buttons; // Tracks a List of the NavigationBar's Buttons

        private const int k_MaxButtonsPerColumn = 6; // Maximum number of buttons that can fit in one column

        // Public reference to all Buttons
        public List<Button> Buttons => m_Buttons;

        // Actions to update an InfoGraphic
        public Action<Sprite, string> InfoGraphicUpdated;
        public Action InfoGraphicReset;

        // Use a private constructor to force clients to use the Builder pattern
        private NavigationBar()
        {
            m_Buttons = new List<Button>();
        }

        // Static factory method to initiate the builder with required parameters
        public static Builder CreateBuilder(VisualElement container)
        {
            return new Builder(container);
        }

        // Set parameters using the Builder pattern
        public class Builder
        {
            private NavigationBar m_NavBar = new NavigationBar();

            // Constructor for the Builder, called by the static factory method
            public Builder(VisualElement container)
            {
                // Set the topmost VisualElement container for the buttons
                m_NavBar.m_Container = container;
            }

            public Builder WithButtonData(NavigationButtonSO[] buttonData)
            {
                m_NavBar.m_ButtonData = buttonData;
                return this;
            }

            // Set the EventRegistry Utility
            public Builder WithEventRegistry(EventRegistry eventRegistry)
            {
                m_NavBar.m_EventRegistry = eventRegistry;
                return this;
            }

            // Set the Template VisualTreeAsset
            public Builder WithButtonAsset(VisualTreeAsset asset)
            {
                m_NavBar.m_ButtonAsset = asset;
                return this;
            }

            // Set the USS StyleSheet to format the button Template
            public Builder WithButtonStyleSheet(StyleSheet styleSheet)
            {
                m_NavBar.m_ButtonStyleSheet = styleSheet;
                return this;
            }

            // Create the NavigationBar
            public NavigationBar Build()
            {
                // Remove the temporary layout elements in UI Builder
                m_NavBar.m_Container.RemovePlaceHolders();

                // Instantiate the buttons
                m_NavBar.InstantiateButtons(m_NavBar.m_ButtonData);
                return m_NavBar;
            }
        }

        // Stores NavigationButton ScriptableObject data on a given Button
        public void SetMenuButtonData(NavigationButtonSO buttonData, Button button)
        {
            // Validate button data and button
            if (buttonData == null || string.IsNullOrEmpty(buttonData.ElementID))
            {
                Debug.LogWarning("[NavigationBar] SetMenuButtonData: Invalid MenuButtonSO skipped.");
                return;
            }

            if (button == null)
            {
                Debug.LogWarning("[NavigationBar] SetMenuButtonData: Invalid Button skipped.");
                return;
            }

            button.userData = buttonData;
        }

        // Create buttons to match the given ScriptableObject assets
        private void InstantiateButtons(NavigationButtonSO[] buttonData)
        {
            if (buttonData == null || buttonData.Length == 0)
            {
                Debug.LogWarning("[NavigationBar] InstantiateButtons: Invalid button data");
                return;
            }

            // Check if we have too many buttons to fit in one column
            int numColumns = Mathf.CeilToInt((float)buttonData.Length / k_MaxButtonsPerColumn);
            float columnWidth = 100f / numColumns;

            // Enable flexWrap if necessary
            bool wrapButtons = numColumns > 1;
            m_Container.style.flexWrap = wrapButtons ? Wrap.Wrap : Wrap.NoWrap;

            // Adjust the FlexBox spacing based on the number of elements
            foreach (NavigationButtonSO navButtonData in buttonData)
            {
                TemplateContainer buttonTemplateContainer = CreateButtonInstance(navButtonData);

                buttonTemplateContainer.style.flexBasis = new StyleLength(
                    new Length(100f / (wrapButtons ? k_MaxButtonsPerColumn : buttonData.Length), LengthUnit.Percent));

                // Split into two or three columns if we have too many buttons
                buttonTemplateContainer.style.width = new Length(columnWidth, LengthUnit.Percent);
            }
        }

        // Instantiate one button from a set of button data
        private TemplateContainer CreateButtonInstance(NavigationButtonSO buttonData)
        {
            // Instantiate the VisualTreeAsset
            var buttonTemplateContainer = m_ButtonAsset.CloneTree();

            // Disable the pickability of the template container to avoid blocking the button clicks
            buttonTemplateContainer.pickingMode = PickingMode.Ignore;

            // Group the buttons together under a VisualElement container
            if (m_Container != null)
            {
                m_Container.Add(buttonTemplateContainer);
                m_Container.style.flexDirection = FlexDirection.Column;
                m_Container.style.flexWrap = Wrap.Wrap;
                m_Container.style.maxHeight = new Length(100, LengthUnit.Percent);
            }


            // Get the UI Button component, then set up label text
            Button button = buttonTemplateContainer.Q<Button>();

            if (!m_Buttons.Contains(button))
            {
                m_Buttons.Add(button);
            }

            button.text = buttonData.LabelText;

            // Add the corresponding USS style sheet
            button.styleSheets.Add(m_ButtonStyleSheet);

            // Store the ScriptableObject data on the Button for later use
            button.userData = buttonData;

            RegisterCallback(button);

            return buttonTemplateContainer;
        }

        /// <summary>
        /// Toggle the button active state.
        /// </summary>
        /// <param name="state"></param>
        public void EnableButtons(bool state)
        {
            foreach (Button button in m_Buttons)
            {
                button.SetEnabled(state);
            }
        }

        // Register actions on each button (ClickEvent, MouseEnterEvent, MouseLeaveEvent)
        private void RegisterCallback(Button button)
        {
            if (button == null)
            {
                Debug.Log("[NavigationBar] RegisterCallback: Invalid Button specified");
                return;
            }

            // Register ScriptableObject delegate to button click
            NavigationButtonSO buttonData = button.userData as NavigationButtonSO;
            if (buttonData != null && buttonData.ButtonActionSO != null)
            {
                m_EventRegistry.RegisterCallback<ClickEvent>(button, evt => buttonData.ButtonActionSO.OnEventRaised());
            }

            // Optional: Show thumbnails and descriptions when entering and exiting mouse pointer
            m_EventRegistry.RegisterCallback<MouseEnterEvent>(button, EnterMenuHandler);
            m_EventRegistry.RegisterCallback<MouseLeaveEvent>(button, ExitMenuHandler);
        }

        // Notify an optional InfoGraphic when mouse enters and leaves the buttons

        // Handle MouseEnterEvent by updating the description text based on the button being hovered.
        private void EnterMenuHandler(MouseEnterEvent evt)
        {
            // Get the button that raised the event
            Button eventButton = evt.target as Button;

            if (eventButton != null)
            {
                // Use the button data to set the optional InfoGraphic image and text
                NavigationButtonSO buttonData = eventButton.userData as NavigationButtonSO;

                if (buttonData != null)
                    // Invoke the Action with the necessary data
                    InfoGraphicUpdated?.Invoke(buttonData.Image, buttonData.Description);
            }
        }

        // Handle MouseLeaveEvent by clearing the description text.
        private void ExitMenuHandler(MouseLeaveEvent evt)
        {
            // Invoke the Action to clear the InfoGraphic
            InfoGraphicReset?.Invoke();
        }
    }

    // IComparer used to sort the ScriptableObject assets by name
    public class NavigationButtonSOComparer : IComparer<NavigationButtonSO>
    {
        public int Compare(NavigationButtonSO x, NavigationButtonSO y)
        {
            if (x == null || y == null)
            {
                return 0;
            }

            return string.Compare(x.name, y.name, System.StringComparison.Ordinal);
        }
    }
}