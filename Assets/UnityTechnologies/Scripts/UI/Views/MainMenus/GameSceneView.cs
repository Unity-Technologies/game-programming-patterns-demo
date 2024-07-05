using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class GameScenePresenter
    {
        // Path to the external Button data (event, text label, etc.)
        const string k_ButtonDataPath = "ButtonData/GameScene/BackButton_Data";

        // 
        public static NavigationButtonSO LoadResources()
        {
            NavigationButtonSO buttonData = Resources.Load<NavigationButtonSO>(k_ButtonDataPath);

            if (buttonData == null)
                Debug.LogWarning("[GameScenePresenter] LoadResources: No button data loaded");

            return buttonData;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class GameSceneView : UIView
    {
        protected const string k_BackButton = "back-button";

        public Action<Button> ButtonSet;
        Button m_BackButton;

        public GameSceneView(VisualElement rootElement) : base(rootElement)
        {

            // Set the Button reference
            SetVisualElements();

            // Get the resources from the Presenter
            m_BackButton.userData = GameScenePresenter.LoadResources();

            // Register the Button event to the Button data
            RegisterButtonCallbacks();
        }

        protected virtual void SetVisualElements()
        {
            m_BackButton = m_RootElement.Q<Button>(k_BackButton);
        }

        private void RegisterButtonCallbacks()
        {
            NavigationButtonSO buttonData = m_BackButton.userData as NavigationButtonSO;

            if (buttonData == null)
            {
                Debug.Log("[GameSceneView] RegisterButtonCallbacks: Missing button data");
                return;
            }
            else
            {
                m_EventRegistry.RegisterCallback<ClickEvent>(m_BackButton, evt => buttonData.ButtonActionSO.OnEventRaised());
            }
        }
    }
}
