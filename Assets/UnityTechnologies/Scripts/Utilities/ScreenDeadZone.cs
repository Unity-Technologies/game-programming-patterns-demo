using UnityEngine;

namespace DesignPatterns.Utilities
{
    [System.Serializable]
    public struct ScreenDeadZone
    {
        [Tooltip("Dead zone as a percentage of screen size (xMin, yMin, width, height)")]
        public Rect percentage;

        public Rect CalculateActualDeadZone()
        {
            return new Rect(
                percentage.x * Screen.width,
                percentage.y * Screen.height,
                percentage.width * Screen.width,
                percentage.height * Screen.height
            );
        }
        
        public bool IsMouseInDeadZone()
        {
            Vector2 mousePosition = Input.mousePosition;
            Rect actualDeadZone = CalculateActualDeadZone();

            // Invert the Y coordinate for mouse position to align with Unity's GUI Rect coordinate system
            mousePosition.y = Screen.height - mousePosition.y;

            return actualDeadZone.Contains(mousePosition);
        }
    }
}
