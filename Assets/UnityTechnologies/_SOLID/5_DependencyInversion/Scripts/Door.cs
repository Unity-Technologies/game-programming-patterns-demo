using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{

    /// <summary>
    /// A Door component that opens and closes two sliding doors. This class demonstrates the Dependency Inversion
    /// Principle (DIP) by being controllable through an abstract interface, ISwitchable. This decouples the door
    /// from the switch that is triggering it.
    /// </summary>
    public class Door : MonoBehaviour, ISwitchable
    {
        [Tooltip("The left sliding door")]
        [SerializeField] private Transform m_LeftDoor;
        [Tooltip("The right sliding door")]
        [SerializeField] private Transform m_RightDoor;
        [Tooltip("Offset position to slide the left door open")]
        [SerializeField] private Vector3 m_LeftDoorOffset;
        [Tooltip("Offset position to slide the right door open")]
        [SerializeField] private Vector3 m_RightDoorOffset;
        [Tooltip("Door open and close speed")]
        [SerializeField] private float m_Speed = 5f;
        
        // Cache door positions
        private Vector3 m_LeftDoorStartPosition;
        private Vector3 m_RightDoorStartPosition;
        private Vector3 m_LeftDoorEndPosition;
        private Vector3 m_RightDoorEndPosition;

        // Tracks whether the doors are currently in the open state.
        private bool m_IsActive;
        public bool IsActive => m_IsActive;

        
        private void Start()
        {
            // Assumes the door transforms start in closed position
            m_LeftDoorStartPosition = m_LeftDoor.position;
            m_RightDoorStartPosition = m_RightDoor.position;
            m_LeftDoorEndPosition = m_LeftDoorStartPosition + m_LeftDoorOffset;
            m_RightDoorEndPosition = m_RightDoorStartPosition + m_RightDoorOffset;
        }

        /// Opens the doors, moving them to their designated open positions.
        public void Activate()
        {
            m_IsActive = true;
            Debug.Log("The door is open.");
            StartCoroutine(SlideDoor(m_LeftDoor, m_LeftDoorEndPosition, m_Speed));
            StartCoroutine(SlideDoor(m_RightDoor, m_RightDoorEndPosition, m_Speed));
        }

        /// Closes the doors, moving them back to their start positions.
        public void Deactivate()
        {
            m_IsActive = false;
            Debug.Log("The door is closed.");
            StartCoroutine(SlideDoor(m_LeftDoor, m_LeftDoorStartPosition, m_Speed));
            StartCoroutine(SlideDoor(m_RightDoor, m_RightDoorStartPosition, m_Speed));
        }
        
        // Interpolates a single door toward a specific position
        private IEnumerator SlideDoor(Transform door, Vector3 targetPosition, float speed)
        {
            while (door.position != targetPosition)
            {
                door.position = Vector3.MoveTowards(door.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }
        }
    }

}
