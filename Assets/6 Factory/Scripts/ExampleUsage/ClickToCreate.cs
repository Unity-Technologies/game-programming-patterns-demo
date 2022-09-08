using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class ClickToCreate : MonoBehaviour
    {
        [SerializeField] private LayerMask layerToClick;
        [SerializeField] private Vector3 offset;
        [SerializeField] Factory[] factories;

        private Factory factory;

        private void Update()
        {
            GetProductAtClick();
        }

        private void GetProductAtClick()
        {
            // check click with raycast
            if (Input.GetMouseButtonDown(0))
            {
                // choose a random factory
                factory = factories[Random.Range(0, factories.Length)];

                // instantiate product at raycast intersection
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerToClick) && factory != null)
                {
                    factory.GetProduct(hitInfo.point + offset);
                }
            }
        }
    }
}

