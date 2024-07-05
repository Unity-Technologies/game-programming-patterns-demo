using System;

namespace DesignPatterns.Events
{
    /// <summary>
    /// Public static delegates to manage UI changes (note these are "events" in the conceptual sense
    /// and not the strict C# sense).
    /// 
    /// To serialize the static delegates, use a ScriptableObject to wrap each Action (see BaseEventSO).
    /// </summary>
    public static class UIEvents
    {
 
        // Close the screen and go back
        public static Action ScreenClosed;

        // Show the Main Menu selection (Settings, Level Selection)
        public static Action MainMenuShown;

        // Show the user settings (sound volume)
        public static Action SolidMenuShown;

        // Show a level select screen to choose what quiz to play
        public static Action DesignPatternsMenuShown;

        // Show a pause screen during gameplay to abort the game
        public static Action ResourcesMenuShown;

        // Open an external link
        public static Action<string> URLOpened;

        // Shows the gameplay screen for one demo level
        public static Action DemoScreenShown;

    }
}
