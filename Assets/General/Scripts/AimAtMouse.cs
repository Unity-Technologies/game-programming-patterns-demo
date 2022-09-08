using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    public class AimAtMouse : MonoBehaviour
    {
        // transform to rotate
        [SerializeField] private Transform aimTransform;

        // y height of transform
        private float aimY;

        private Vector3 mouseWorldSpacePosition;

        private void Start()
        {
            if (aimTransform)
            {
                aimY = aimTransform.position.y;
            }
        }

       private void Update()
        {
            // calculate 3D position of mouse
            mouseWorldSpacePosition = GetMouseWorldSpacePosition();

            // rotate gun base to face target 
            RotateToTarget(aimTransform, mouseWorldSpacePosition);
        }

        // interpolate rotation toward target
        private void RotateToTarget(Transform objectToRotate, Vector3 targetPosition)
        {
            Vector3 directionToTarget = targetPosition - transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(directionToTarget);
            objectToRotate.transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.5f);
        }

        // use Raycast to convert mouse position to 3D world coordinate
        private Vector3 GetMouseWorldSpacePosition()
        {
            if (Camera.main == null)
            {
                Debug.Log("WARNING AimAtMouse: Tag a main camera");
                return Vector3.zero;
            }

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                return new Vector3(hit.point.x, aimY, hit.point.z);
            }

            return Vector3.zero;
        }

    }
}
