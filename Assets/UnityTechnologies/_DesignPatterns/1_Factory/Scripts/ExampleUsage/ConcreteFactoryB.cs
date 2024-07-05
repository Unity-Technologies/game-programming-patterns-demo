using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.Factory
{
    public class ConcreteFactoryB : Factory
    {
        // Used to create a Prefab
        [SerializeField] 
        private ProductB m_ProductPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            // Create a Prefab instance and get the product component
            GameObject instance = Instantiate(m_ProductPrefab.gameObject, position, Quaternion.identity);
            ProductB newProduct = instance.GetComponent<ProductB>();

            // Each product contains its own logic
            newProduct.Initialize();

            // Add any unique behavior to this factory
            instance.name = newProduct.ProductName;
            Debug.Log(GetLog(newProduct));

            return newProduct;
        }
    }
}
