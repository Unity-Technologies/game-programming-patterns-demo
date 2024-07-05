using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class ConcreteFactoryA : Factory
    {
        // Used to create a Prefab
        [SerializeField] 
        private ProductA m_ProductPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            // Create a Prefab instance and get the product component
            GameObject instance = Instantiate(m_ProductPrefab.gameObject, position, Quaternion.identity);
            ProductA newProduct = instance.GetComponent<ProductA>();

            // Each product contains its own logic
            newProduct.Initialize();

            return newProduct;
        }
    }
}
