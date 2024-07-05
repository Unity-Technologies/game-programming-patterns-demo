using UnityEngine.UIElements;
using DesignPatterns.Events;

namespace DesignPatterns.UI
{
    /// <summary>
    /// This fullscreen UI hides the main menu until the user is ready to begin
    /// </summary>

    public class StartScreen : UIView
    {
        Button m_StartButton;

        // Constructor 
        public StartScreen(VisualElement parentElement) : base(parentElement)
        {
            m_RootElement = parentElement;

            m_StartButton = m_RootElement.Q<Button>("start__start-button");

            // The custom Event Registry unregisters the callback automatically on disable
            m_EventRegistry.RegisterCallback<ClickEvent>(m_StartButton, evt => UIEvents.MainMenuShown?.Invoke());

        }
    }
}
