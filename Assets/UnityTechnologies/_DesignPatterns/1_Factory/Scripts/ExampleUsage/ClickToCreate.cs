using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DesignPatterns.Factory
{
    public class ClickToCreate : MonoBehaviour
    {
        [SerializeField] 
        private LayerMask m_LayerToClick;

        [SerializeField] 
        private Vector3 m_Offset;

        [SerializeField] 
        private Factory[] m_Factories;

        // List to track all created products
        private List<GameObject> m_CreatedProducts = new List<GameObject>();

        private void Update()
        {
            GetProductAtClick();
        }

        private void GetProductAtClick()
        {
            // Check if the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                // Get a random factory from the list
                Factory selectedFactory = m_Factories[Random.Range(0, m_Factories.Length)];
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                // Check if the raycast hits a collider on the layer we want to click
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, m_LayerToClick) && selectedFactory != null)
                {
                    IProduct product = selectedFactory.GetProduct(hitInfo.point + m_Offset);
                    
                    // Add the GameObject of the created product to the list
                    if (product is Component component) 
                    {
                        m_CreatedProducts.Add(component.gameObject);
                    }
                }
            }
        }
        
        private void OnDestroy()
        {
            foreach (GameObject product in m_CreatedProducts)
            {
                Destroy(product);
            }
            // Clear the list when the object is destroyed
            m_CreatedProducts.Clear(); 
        }
    }
}

