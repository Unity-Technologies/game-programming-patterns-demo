using UnityEngine.UIElements;
using UnityEngine;
using DesignPatterns.Utilities;

namespace DesignPatterns.UI
{
    public class MainMenuView : BaseMenuView
    {

        protected new const string k_NavBarContainer = "button-container";    // Element holding the NavigationBar
        protected new const string k_InfoGraphicContainer = "right__container";    // Element holding the InfoGraphic

        protected const string k_ButtonDataPath = "ButtonData/MainMenu";
        protected new const string k_ButtonTemplatePath = "UI/button__template";
        protected new const string k_ButtonStyleSheetPath = "UI/button__template";

        // Constructor
        public MainMenuView(VisualElement rootElement) : base(rootElement)
        {
            
        }

        protected override void LoadResources()
        {

            // Load the data for each button (ID, label text, Action, Sprite, etc.)
            m_ButtonData = ResourceLoader.LoadScriptableObjects<NavigationButtonSO>(k_ButtonDataPath);

            // Load the UXML VisualTreeAsset for the Button template
            ButtonAsset = ResourceLoader.LoadVisualTreeAsset(k_ButtonTemplatePath);

            // Load the USS style sheet for the Button template
            ButtonStyleSheet = ResourceLoader.LoadStyleSheet(k_ButtonStyleSheetPath);
        }
    }
}
