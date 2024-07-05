using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{
    /// <summary>
    /// The Trap class represents a physics-based trapdoor which implements ISwitchable.
    /// </summary>
    public class Trap : MonoBehaviour, ISwitchable
    {
        // Rigidbody component for physics interactions.
        private Rigidbody m_Rigidbody;

        // Original position of the trap, used for resetting its position.
        private Vector3 m_OriginalPosition;
        
        // Original rotation of the trap, used for resetting its rotation.
        private Quaternion m_OriginalRotation;
        
        // ISwitchable active state
        private bool m_IsActive;
        public bool IsActive => m_IsActive;
        
        private void Start()
        {
            // Cache the Physics
            m_Rigidbody = GetComponent<Rigidbody>();
            
            // Disable physics-based movement but allow collision detection and manual movements
            m_Rigidbody.isKinematic = true;
            
            // Cache the original transform values
            m_OriginalPosition = transform.position;
            m_OriginalRotation = transform.rotation;
        }

        // Enabling physics and mark it as active.
        public void Activate()
        {
            m_IsActive = true;
            Debug.Log("The trap is active.");

            m_Rigidbody.isKinematic = false;
        }

        // Deactivates the trap and marks it as inactive.
        public void Deactivate()
        {
            // Reset the Rigidbody to kinematic to disable physics-based movement.
            m_Rigidbody.isKinematic = true;
            m_IsActive = false;
            
            // Reset the trap's position and rotation to their original values.
            transform.position = m_OriginalPosition;
            transform.rotation = m_OriginalRotation;
            
            Debug.Log("The trap is reset.");
        }
    }
}
