using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Utilities
{
    public enum LogType
    {
        Info,
        Warning,
        Error
    }

    // <summary>
    /// This helper class logs a message at the console, useful logging messages
    /// from UnityEvents/EventTrigger components. Fill in the optional prefix here,
    /// then the message in the Inspector for the UnityEvent component.
    /// </summary>
    public class DebugLogger : MonoBehaviour
    {
        [Tooltip("This prefix is added to the start of each logged message")]
        [SerializeField] private string m_LogPrefix = "[DebugLogger]";

        // Log an informational message
        public void LogMessage(string message)
        {
            LogMessage(message, LogType.Info);
        }

        public void LogWarningMessage(string message)
        {
            LogMessage(message, LogType.Warning);
        }

        public void LogErrorMessage(string message)
        {
            LogMessage(message, LogType.Error);
        }

        // Log messages based on the provided type
        private void LogMessage(string message, LogType logType)
        {
            string formattedMessage = $"{m_LogPrefix} {message}";

            switch (logType)
            {
                case LogType.Warning:
                    Debug.LogWarning(formattedMessage);
                    break;
                case LogType.Error:
                    Debug.LogError(formattedMessage);
                    break;
                default:
                    Debug.Log(formattedMessage);
                    break;
            }
        }

        // Additional methods for other data types...

        // Log a formatted informational message
        public void LogFormat(string format, params object[] args)
        {
            LogFormat(format, LogType.Info, args);
        }

        // Log a formatted warning message
        public void LogWarningFormat(string format, params object[] args)
        {
            LogFormat(format, LogType.Warning, args);
        }

        // Log a formatted error message
        public void LogErrorFormat(string format, params object[] args)
        {
            LogFormat(format, LogType.Error, args);
        }

        // Log formatted messages based on the provided type
        private void LogFormat(string format, LogType logType, params object[] args)
        {
            LogMessage(string.Format(format, args), logType);
        }
    }
}
