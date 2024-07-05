using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{
    /// <summary>
    /// A VisualElement that represents a Tooltip. This shows/hides the tooltip and
    /// positions it on-screen.
    /// </summary>
    public class TooltipView : VisualElement
    {
        Label m_TooltipLabel;
        Vector2 m_Offset = new Vector2(60, -60f);

        public Label TooltipLabel { get => m_TooltipLabel; set => m_TooltipLabel = value; }
        public Vector2 Offset { get => m_Offset; set => m_Offset = value; }

        // Define basic setup in Constructor
        public TooltipView()
        {
            m_TooltipLabel = new Label();
            m_TooltipLabel.text = "TOOLTIP_PLACEHOLDER";
            hierarchy.Add(m_TooltipLabel);

            style.position = Position.Absolute;
            pickingMode = PickingMode.Ignore;
            visible = false;
        }

        // Display the tooltip at a specific position with a specified text.
        public void ShowTooltip(MouseEnterEvent evt, string tooltipText)
        {
            visible = true;

            VisualElement target = evt.currentTarget as VisualElement;

            Vector2 targetPosition = target.worldBound.position;
            Vector2 adjustedPosition = targetPosition + Offset;

            adjustedPosition = ClampToScreen(target, adjustedPosition);

            m_TooltipLabel.style.left = adjustedPosition.x;
            m_TooltipLabel.style.top = adjustedPosition.y;

            m_TooltipLabel.text = tooltipText;
        }

        public void HideTooltip()
        {
            visible = false;
        }

        //  Clamps the position of the tooltip to the screen dimensions.
        private Vector2 ClampToScreen(VisualElement element, Vector2 targetPosition)
        {
            float x = Mathf.Clamp(targetPosition.x, 0, Screen.width - element.layout.width);
            float y = Mathf.Clamp(targetPosition.y, 0, Screen.height - element.layout.height);

            return new Vector2(x, y);
        }
    }

    /// <summary>
    /// An input controller that handles tooltip interaction with mouse events. This is an
    /// example of a Manipulator, which groups several event handlers together into one
    /// class for easier management.
    /// </summary>
    public class TooltipManipulator : MouseManipulator
    {
        // Reference to the custom Tooltip, create this once and reuse it
        TooltipView m_TooltipView;

        // Create a new Manipulator for each element
        public TooltipManipulator(TooltipView tooltip)
        {
            this.m_TooltipView = tooltip;
        }

        // Register callbacks for mouse enter and leave events.
        protected override void RegisterCallbacksOnTarget()
        {
            target.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
            target.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }

        // Unregister callbacks for mouse enter and leave events.
        protected override void UnregisterCallbacksFromTarget()
        {
            target.UnregisterCallback<MouseEnterEvent>(OnMouseEnter);
            target.UnregisterCallback<MouseLeaveEvent>(OnMouseLeave);
        }

        // Method to handle mouse enter events.
        private void OnMouseEnter(MouseEnterEvent evt)
        {
            string tooltipText = target.tooltip;
            m_TooltipView.ShowTooltip(evt, tooltipText);
        }

        // Method to handle mouse leave events.
        private void OnMouseLeave(MouseLeaveEvent evt)
        {
            m_TooltipView.HideTooltip();
        }
    }

    /// <summary>
    /// Manages the creation and assignment of tooltips.
    /// </summary>
    public class TooltipController
    {
        const string k_StyleSheet = "Uss/Tooltips";
        const string k_TooltipClassName = "tooltip-label";

        private TooltipView m_TooltipView;
        private VisualElement m_Root;

        public TooltipView TooltipView { get => m_TooltipView; set => m_TooltipView = value; }

        // Constructor
        public TooltipController(VisualElement rootElement)
        {
            m_Root = rootElement;
            InitializeTooltip();
        }

        // Set starting values and properties
        private void InitializeTooltip()
        {
            TooltipView = new TooltipView();
            m_Root.Add(TooltipView);

            StyleSheet stylesheet = Resources.Load<StyleSheet>(k_StyleSheet);

            if (stylesheet == null)
            {
                Debug.LogError("[TooltipController]: Failed to load Tooltips StyleSheet.");
            }
            else
            {
                TooltipView.TooltipLabel.styleSheets.Add(stylesheet);
                TooltipView.TooltipLabel.AddToClassList(k_TooltipClassName);
            }
        }

        // Assign a tooltip to a specific VisualElement.
        public void AssignTooltipToElement(VisualElement element, string tooltipText)
        {
            if (!string.IsNullOrEmpty(tooltipText))
            {
                // Set the element's tooltip text
                element.tooltip = tooltipText;

                // Use that to create a new TooltipMouseManipulator
                element.AddManipulator(new TooltipManipulator(TooltipView));
            }
        }
    }
}
