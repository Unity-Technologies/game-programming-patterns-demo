using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



namespace DesignPatterns.UI
{
    /// <summary>
    /// A utility class that adds a small delay to stop Button spamming
    /// </summary>
    public static class ButtonCooldown
    {
        // Store a button with next press time
        private static Dictionary<Button, float> m_LastPressTimes = new Dictionary<Button, float>();

        // Cooldown delay
        private static float m_CooldownDuration = 0.5f;

        // Check a new or existing Button if it's ready to be pressed
        public static bool CanPressButton(Button button)
        {
            if (!m_LastPressTimes.TryGetValue(button, out float lastPressTime))
            {
                m_LastPressTimes[button] = Time.time;
                return true;
            }

            if (Time.time - lastPressTime < m_CooldownDuration)
            {
                return false;
            }

            m_LastPressTimes[button] = Time.time;
            return true;
        }
    }
}
