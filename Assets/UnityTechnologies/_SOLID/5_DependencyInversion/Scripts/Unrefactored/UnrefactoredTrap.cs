using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{
    public class UnrefactoredTrap : MonoBehaviour
    {
        private bool m_IsActive;
        public bool IsActive => m_IsActive;

        public void Enable()
        {
            m_IsActive = true;
            Debug.Log("The trap is active.");
        }

        public void Disable()
        {
            m_IsActive = false;
            Debug.Log("The trap is inactive.");
        }
    }
}
