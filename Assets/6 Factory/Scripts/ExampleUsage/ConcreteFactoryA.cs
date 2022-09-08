using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class ConcreteFactoryA : Factory
    {
        // used to create a Prefab
        [SerializeField] private ProductA productPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            // create a Prefab instance and get the product component
            GameObject instance = Instantiate(productPrefab.gameObject, position, Quaternion.identity);
            ProductA newProduct = instance.GetComponent<ProductA>();

            // each product contains its own logic
            newProduct.Initialize();

            return newProduct;
        }
    }
}
