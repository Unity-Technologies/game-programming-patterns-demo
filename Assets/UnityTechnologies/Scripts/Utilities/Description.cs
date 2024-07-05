using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Utilities
{
    /// <summary>
    /// This is a simple component for holding notes or explanation.
    /// </summary>
    public class Description : MonoBehaviour
    {
        [TextArea(5, 25)]
        [SerializeField]
        private string m_Text;

        public string Text { get => m_Text; set => m_Text = value; }

    }
}
