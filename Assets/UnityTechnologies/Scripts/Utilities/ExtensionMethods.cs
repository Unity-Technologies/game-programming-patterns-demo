using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.Collections.Generic;

/// <summary>
/// Static class with extension methods.
/// </summary>
/// 
namespace DesignPatterns.Utilities
{

    public static class ExtensionMethods
    {
        public static void ResetTransformation(this Transform trans)
        {
            trans.position = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = new Vector3(1, 1, 1);
        }



        // Converts a screen position from UI Toolkit to world space
        public static Vector3 ScreenPosToWorldPos(this Vector2 screenPos, Camera camera = null, float zDepth = 10f)
        {
            if (camera == null)
                camera = Camera.main;

            if (camera == null)
                return Vector2.zero;

            float xPos = screenPos.x;
            float yPos = screenPos.y;
            Vector3 worldPos = Vector3.zero;

            if (!float.IsNaN(screenPos.x) && !float.IsNaN(screenPos.y))
            {
                // Flip y-coordinate; in UI Toolkit, (0,0) is top-left instead of bottom-left.
                yPos = camera.pixelHeight - yPos;

                // Convert to world space position using Camera class
                Vector3 screenCoord = new Vector3(xPos, yPos, zDepth);
                worldPos = camera.ScreenToWorldPoint(screenCoord);
            }
            return worldPos;
        }

        // Converts a number (0, 1, 2, 3,...) into a letter (A, B, C, D, etc.)
        public static char ConvertToLetter(this int number)
        {
            // Add 65 to the number to get the corresponding ASCII code for the letter
            int asciiCode = number + 65;

            // Convert the ASCII code to its corresponding letter
            char letter = System.Convert.ToChar(asciiCode);

            return letter;
        }

        // Check if a GameObject is assigned to a Layer within a LayerMask
        public static bool ContainsLayer(this LayerMask layerMask, GameObject obj)
        {
            // Check if LayerMask includes the bitwise representation of the GameObject layer
            return ((layerMask.value & (1 << obj.layer)) != 0);
        }

    }

    public static class VisualElementExtensions
    {
        /// <summary>
        /// Forces a UI element to be a square with a specified size.
        /// </summary>
        /// <param name="elementToAdjust">The VisualElement to adjust.</param>
        /// <param name="size">The desired size for both width and height.</param>
        /// <param name="minSize">The minimum size to ensure the element is not too small.</param>
        public static void MakeSquare(this VisualElement elementToAdjust, float size, float minSize = 100f)
        {
            if (elementToAdjust == null)
                throw new ArgumentNullException(nameof(elementToAdjust));

            if (size < 0)
                throw new ArgumentException("Size cannot be negative.", nameof(size));

            size = Mathf.Max(size, minSize);

            elementToAdjust.style.width = size;
            elementToAdjust.style.height = size;
        }

        // Returns the world space position that corresponds to the center of a VisualElement
        public static Vector3 GetWorldPosition(this VisualElement visualElement, Camera camera = null, float zDepth = 10f)
        {
            if (camera == null)
                camera = Camera.main;

            Vector3 worldPos = Vector3.zero;

            if (camera == null || visualElement == null)
                return worldPos;

            return visualElement.worldBound.center.ScreenPosToWorldPos(camera, zDepth);
        }

        // Removes any temporary buttons used for layout in UI Builder. This allows you to
        // keep placeholders in the UXML for visualization and then clear them at runtime.
        public static void RemovePlaceHolders(this VisualElement visualElement)
        {
            if (visualElement == null)
            {
                Debug.LogWarning("[VisualElementExtensions] RemovePlaceHolders: Invalid VisualElement container");
                return;
            }

            List<TemplateContainer> placeholders = visualElement.Query<TemplateContainer>().ToList();

            foreach (TemplateContainer placeholder in placeholders)
            {
                visualElement.Remove(placeholder);
            }
        }
    }

}