using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.UI
{

    [Serializable]
    public class ButtonHighlighter
    {

        private const string k_SelectedButtonClassName = "button--selected";

        List<Button> m_Buttons;

        public ButtonHighlighter(List<Button> buttons)
        {
            m_Buttons = buttons;
        }

        // Highlight the clicked button
        public void HighlightButton(int buttonIndex)
        {

            // Clear all currently selected buttons
            UnhighlightButtons();

            if (buttonIndex >= 0 && buttonIndex < m_Buttons.Count)
            {
                // Get the clicked button using its index
                Button clickedButton = m_Buttons[buttonIndex];

                // Add the selected button class to the clicked button
                clickedButton.AddToClassList(k_SelectedButtonClassName);
            }
            else
            {
                Debug.LogWarning("[NavigationBar]: Button index out of range");
            }
        }

        // Removes highlight styles from all Buttons
        private void UnhighlightButtons()
        {
            //List<Button> selectedButtons = m_ButtonContainer.Query<Button>()
            //    .Where(x => x.ClassListContains(k_SelectedButtonClassName)).ToList();

            foreach (Button button in m_Buttons)
            {
                button.RemoveFromClassList(k_SelectedButtonClassName);
            }
        }
    }
}