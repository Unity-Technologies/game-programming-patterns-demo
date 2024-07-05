using UnityEngine;
using UnityEngine.UIElements;


namespace DesignPatterns.UI
{
    /// <summary>
    /// This component manages an image and text combination
    /// </summary>
    public class InfoGraphicView : UIView
    {
        // Element name IDs
        const string k_TextLabel = "info-graphic__text";
        const string k_GraphicElement = "info-graphic__image";

        VisualElement m_GraphicElement;  // Holds the background image
        Label m_TextLabel;  // Displays caption or description

        string m_DefaultText;  // Optional default text for the label
        Sprite m_DefaultImage; // Optional default background image

        // Use to set default text and image (empty/null unless specified)
        public string DefaultText { get => m_DefaultText; set => m_DefaultText = value; }
        public Sprite DefaultImage { get => m_DefaultImage; set => m_DefaultImage = value; }

        // Constructor
        public InfoGraphicView(VisualElement rootElement) : base(rootElement)
        {
            m_HideOnAwake = false;
            SetVisualElements();
            Show();
        }

        // Clear image and text
        public void HidePlaceholders()
        {
            m_TextLabel.text = string.Empty;
            m_GraphicElement.style.backgroundImage = null;
        }

        // Store references to the VisualElements
        private void SetVisualElements()
        {
            m_TextLabel = m_RootElement.Q<Label>(k_TextLabel);
            m_GraphicElement = m_RootElement.Q<VisualElement>(k_GraphicElement);
        }

        // Apply a Sprite to the background image
        public void ShowImage(Sprite image)
        {
            if (m_GraphicElement == null)
                return;

            m_GraphicElement.style.backgroundImage = new StyleBackground(image);
        }

        /// <summary>
        /// Shows the default Sprite image.
        /// </summary>
        public void ShowDefaultImage()
        {
            ShowImage(m_DefaultImage);
        }

        /// <summary>
        /// Sets the text for the label.
        /// </summary>
        public void ShowText(string text)
        {
            if (m_TextLabel == null)
                return;

            m_TextLabel.text = text;
        }

        /// <summary>
        /// Shows the default text.
        /// </summary>
        public void ShowDefaultText()
        {
            ShowText(m_DefaultText);
        }
    }

    /// <summary>
    /// Handles events related to updating an InfoGraphic based on interactions with a NavigationBar.
    /// </summary>
    public class InfoGraphicHandler
    {
        private InfoGraphicView m_InfoGraphic;

        /// <summary>
        /// Initializes a new instance of the InfoGraphicHandler.
        /// </summary>
        /// <param name="infoGraphic">The InfoGraphic to manage.</param>
        /// <param name="navigationBar">The NavigationBar to listen to for events.</param>

        public InfoGraphicHandler(InfoGraphicView infoGraphic, NavigationBar navigationBar)
        {
            m_InfoGraphic = infoGraphic;
            navigationBar.InfoGraphicUpdated += NavigationBar_InfoGraphicUpdated;
            navigationBar.InfoGraphicReset += NavigationBar_InfoGraphicReset;
        }

        /// <summary>
        /// Updates the InfoGraphic with a new image and description.
        /// </summary>
        /// <param name="image">The image to display.</param>
        /// <param name="description">The description text.</param>

        private void NavigationBar_InfoGraphicUpdated(Sprite image, string description)
        {
            m_InfoGraphic?.ShowImage(image);
            m_InfoGraphic?.ShowText(description);
        }

        /// <summary>
        /// Resets the InfoGraphic to its default state.
        /// </summary>
        private void NavigationBar_InfoGraphicReset()
        {
            m_InfoGraphic?.ShowDefaultImage();
            m_InfoGraphic?.ShowDefaultText();
        }
    }
}
