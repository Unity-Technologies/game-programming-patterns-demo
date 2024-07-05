using UnityEngine;
using UnityEngine.UIElements;
using DesignPatterns.Events;
using DesignPatterns.Utilities;

namespace DesignPatterns.UI
{
    /// <summary>
    /// ScriptableObject to hold associated data for each button for a menu/navigation bar.
    /// </summary>
    [CreateAssetMenu(fileName = "NavigationButton_Data", menuName = "DesignPatterns/NavigationButtonData", order = 1)]
    public class NavigationButtonSO : ScriptableObject
    {
        [Header("Description")]
        [TextArea(5, 8)]
        [SerializeField][Optional] protected string m_Note;
        [Space]
        [Space]
        [Header("Visual Element")]
        [SerializeField] string m_ElementID;    // The ID of the button element in the UI
        [SerializeField] string m_LabelText;    // The label text for each button
        [Header("Event Message")]
        [SerializeField] BaseEventSO m_ButtonActionSO;   // ScriptableObject-based delegate for the button action.
        [Space]
        [Space]
        [Header("Optional")]
        [TextArea(3, 10)]
        [SerializeField] string m_Description;    // The description text for each button
        [SerializeField] Sprite m_Image;    // The optional poster image of the button

  

        Button m_MenuButton;    // The UI Element Button object

        public string ElementID => m_ElementID;
        public string LabelText => m_LabelText;
        public string Description => m_Description;
        public Sprite Image => m_Image;

        public Button MenuButton { get => m_MenuButton; set => m_MenuButton = value; }
        public BaseEventSO ButtonActionSO => m_ButtonActionSO;


    }
}