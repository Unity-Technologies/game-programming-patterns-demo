using System;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// This ScriptableObject stores and manages a collection of text pages.
    /// It provides functionality to navigate (increment/decrement) through pages and access the content of the current page.
    /// The class also includes a TextUpdated event, which passes the current page index when invoked.

    /// </summary>
    /// 
    [CreateAssetMenu(fileName = "InfoText_Data", menuName = "DesignPatterns/InfoText", order = 5)]
    public class InfoTextSO : ScriptableObject
    {
        public event Action<int> TextUpdated;

        [SerializeField] string m_Title;
        [Header("Text Pages")]
        [Tooltip("Stores all of the text used in this InfoText object")]
        [TextArea(5, 10)]
        [SerializeField] List<string> m_TextPages;
        [Tooltip("The current page index")]
        [SerializeField] int m_CurrentIndex;
        [Tooltip("Use to store additional instructions or supporting text")]
        [TextArea(2,5)]
        [SerializeField] string m_AdditionalText;
        [TextArea(2, 5)]
        [SerializeField] string m_FooterText;

        public int CurrentIndex => m_CurrentIndex;
        public string Title => m_Title;
        public string CurrentTextPage => m_TextPages.Count > 0 ? m_TextPages[CurrentIndex] : string.Empty;
        public string AddditionalText => m_AdditionalText;
        public string FooterText => m_FooterText;
        public int PageCount => m_TextPages.Count;
        
        private void OnEnable()
        {
            ResetIndex();
        }

        /// <summary>
        /// Resets the index to 0.
        /// </summary>
        public void ResetIndex()
        {
            m_CurrentIndex = 0;
            TextUpdated?.Invoke(m_CurrentIndex);
        }

        /// <summary>
        /// Increments the index within the bounds of the text pages.
        /// </summary>
        public void IncrementIndex()
        {
            m_CurrentIndex = Mathf.Clamp(++m_CurrentIndex, 0, m_TextPages.Count-1);
            TextUpdated?.Invoke(m_CurrentIndex);
        }

        /// <summary>
        /// Decrements the index within the bounds of the text pages.
        /// </summary>
        public void DecrementIndex()
        {
            m_CurrentIndex = Mathf.Clamp(--m_CurrentIndex, 0, m_TextPages.Count-1);
            TextUpdated?.Invoke(m_CurrentIndex);
        }
        /// <summary>
        /// Returns a string in the form of '(current page / max pages)'
        /// </summary>
        /// <returns>(current page / max pages)</returns>
        public string GetPageCounter()
        {
            string pageCount = $"{CurrentIndex+1}/{m_TextPages.Count}";
            return pageCount;
        }

        public string GetPageCounterIcons()
        {
            return GetPageCounterUnicodeCircles(CurrentIndex + 1, m_TextPages.Count);
        }
        /// <summary>
        /// Returns a string in the form of Unicode empty and solid circles instead of
        /// (current page / max pages)
        /// </summary>
        /// <returns>Unicode circles that represent (page/max pages)</returns>
        public string GetPageCounterUnicodeCircles(int current, int total)
        {
            if (total <= 0) 
                return string.Empty;

            // Ensure current is within the bounds (minimum is 1)
            current = Mathf.Clamp(current, 1, total);

            var circles = new char[total];

            for (int i = 0; i < total; i++)
            {
                // Mark the current index as solid, others as empty
                circles[i] = (i + 1 == current) ? '\u25CF' : '\u25CB';
            }

            return new string(circles);
        }
    }
}
