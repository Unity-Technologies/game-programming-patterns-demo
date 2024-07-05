using System;
using DesignPatterns.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.ISP
{
    /// <summary>
    /// Use two transforms to aim a rig at a mouse target;
    /// </summary>
    public class AimAtMouse : MonoBehaviour
    {
        [Header("Pan")]
        [SerializeField] private Transform panTransform;
        [SerializeField] private float panSpeed = 5f;
        
        [Header("Tilt")]
        [SerializeField] private Transform tiltTransform;
        [SerializeField] private float tiltSpeed = 50f;
        
        [SerializeField] private float maxTiltUp = -70f;
        [SerializeField] private float maxTiltDown = 10f;
        
        [Header("Target")]
        [SerializeField] private MouseToWorldPosition m_MouseToWorldPosition;
        
        [Tooltip("Offset to adjust targeting for demo purposes")]
        [SerializeField] Vector3 m_AimOffset;
        ScreenDeadZone m_ScreenDeadZone;
        
        private void Start()
        {
            // Tilt up is always negative
            maxTiltUp = -Mathf.Abs(maxTiltUp);
            
            // Tilt down is always positive
            maxTiltDown = Mathf.Abs(maxTiltDown);

            if (m_MouseToWorldPosition != null)
            {
                m_ScreenDeadZone = m_MouseToWorldPosition.ScreenDeadZone;
            }
        }

        private void Update()
        {
            if (m_MouseToWorldPosition == null)
                return;

            if (m_ScreenDeadZone.IsMouseInDeadZone())
                return;
            
            RotatePanTowards(m_MouseToWorldPosition.Position);
            RotateTiltTowards(m_MouseToWorldPosition.Position);
        }
        private void RotatePanTowards(Vector3 targetPosition)
        {
            // Ignore vertical component for pan rotation
            Vector3 targetDirection = m_AimOffset + targetPosition - panTransform.position;
            targetDirection.y = 0; 

            // Prevent undefined behavior when target is directly above or below
            if (targetDirection == Vector3.zero) 
                return; 

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            panTransform.rotation = Quaternion.Slerp(panTransform.rotation, targetRotation, panSpeed * Time.deltaTime);
        }

        private void RotateTiltTowards(Vector3 targetPosition)
        {
            if (tiltTransform == null)
                return;
            
            // Direction to target in world space
            Vector3 directionToTarget = m_AimOffset + targetPosition - tiltTransform.position;

            // Convert direction to local space
            Vector3 directionToLocal = tiltTransform.InverseTransformDirection(directionToTarget);

            // Calculate the tilt angle in degrees
            float tiltAngle = Mathf.Atan2(-directionToLocal.y, directionToLocal.x) * Mathf.Rad2Deg;

            // Clamp the tilt to prevent unwanted angles
            tiltAngle = Mathf.Clamp(tiltAngle, maxTiltUp, maxTiltDown);

            // Apply the tilt rotation as a local rotation around the X-axis
            Quaternion targetRotation = Quaternion.Euler(tiltAngle, 0, 0);
            
            tiltTransform.localRotation = Quaternion.Slerp(tiltTransform.localRotation, targetRotation, tiltSpeed * Time.deltaTime);
        }
        

    }
}
