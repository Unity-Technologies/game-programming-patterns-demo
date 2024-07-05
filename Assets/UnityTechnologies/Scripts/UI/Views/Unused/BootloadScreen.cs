using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using DesignPatterns.Utilities;

namespace DesignPatterns
{
    /// <summary>
    /// This class controls the display of the BootloaderScreen. This screen appears only briefly
    /// before pressing Play so it uses an ExecuteInEditMode attribute.
    ///
    /// Here, a warning message displays if the user chooses a non-landscape screen aspect ratio.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    [ExecuteInEditMode]
    public class BootloadScreen : MonoBehaviour
    {
        [Tooltip("Required UI Document")]
        [SerializeField] UIDocument m_Document;

        [Header("Example Display Toggle")]
        [SerializeField][Optional] string m_MessageElementID = "";

        VisualElement m_Root; // The root VisualElement of the UI
        Label m_MessageLabel; // The warning Label to show in portrait mode

        Vector2 m_CurrentResolution;

        private void OnEnable()
        {
            // Reference Visual Elements (try-catch block suppresses errors on startup)
            try
            {
                SetVisualElements();

                // Set the screen dimensions
                m_CurrentResolution = new Vector2(Screen.width, Screen.height);
            }
            catch (NullReferenceException)
            {
                // Suppresses errors on startup
            }
        }

        // Set up UI elements
        private void SetVisualElements()
        {
            if (m_Document == null)
                m_Document = GetComponent<UIDocument>();

            m_Root = m_Document.rootVisualElement;
            m_MessageLabel = m_Root.Q<Label>(m_MessageElementID);
        }

        // Shows warning message in portrait mode
        private void ShowMessage()
        {
            if (m_MessageLabel == null)
                return;

            m_CurrentResolution = new Vector2(Screen.width, Screen.height);

            // Check aspect ratio and warn if unsupported
            if (m_CurrentResolution.x > m_CurrentResolution.y)
            {
                // Disable warning (user is using landscape mode). Use DisplayStyle.None to disable.
                m_MessageLabel.style.display = DisplayStyle.None;
            }
            else
            {
                // Show portrait warning (inform user to switch to landscape mode). Use DisplayStyle.None to enable.
                m_MessageLabel.style.display = DisplayStyle.Flex;
            }
        }
    }
}


// You can use a simple script to modify a VisualElement's style properties, including:

//  Dimensions: width and height (as shown in the example).
//  Positioning: top, right, bottom, left, position (static, relative, absolute, or fixed)
//  Background: backgroundImage, backgroundColor, backgroundSize
//  Borders: borderTopWidth, borderRightWidth, borderBottomWidth, borderLeftWidth, borderColor, borderRadius 
//  Layout: flexDirection, flexWrap, flexBasis, flexGrow, flexShrink, alignContent, alignItems, alignSelf, justifyContent 
//  Opacity: opacity
//  Transforms: scale, translate, rotate

//  Set these USS style properties in the UI Builder or edit the USS text files directly. Modify them at runtime using C#.

//  See the documentation for more about USS style properties:
//  https://docs.unity3d.com/2020.1/Documentation/Manual/UIE-USS-PropertyTypes.html
