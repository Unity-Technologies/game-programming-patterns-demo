using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.UI
{
    /// <summary>
    /// This enum defines font sizes.
    /// </summary>
    [System.Serializable]
    public enum UIFontSize
    {
        Small,
        Medium,
        Large
    }

    /// <summary>
    /// Utility class for holding text styles. Use this to pair a USS class to a corresponding font enum
    /// </summary> 
    public static class TextStyles
    {
        // Static array of strings for font sizes
        public static string[] FontSizes =
        {
            "question-text-small",
            "question-text-medium",
            "question-text-large"
        };

        // Use this to pair a USS Class Selector with a FontSize enum
        public static string GetFontClass(UIFontSize fontSize)
        {
            switch (fontSize)
            {
                case UIFontSize.Small:
                    return FontSizes[0];
                case UIFontSize.Medium:
                    return FontSizes[1];
                case UIFontSize.Large:
                    return FontSizes[2];
                default:
                    return string.Empty;
            }
        }

        // Get array of other font class names (useful for removing the other USS classes)
        public static string[] GetOtherFontClasses(UIFontSize currentFontSize)
        {
            var currentIndex = (int)currentFontSize;

            List<string> otherFontSizes = new List<string>();

            for (int i = 0; i < FontSizes.Length; i++)
            {
                if (i != currentIndex)
                    otherFontSizes.Add(FontSizes[i]);
            }

            return otherFontSizes.ToArray();
        }

    }
}
