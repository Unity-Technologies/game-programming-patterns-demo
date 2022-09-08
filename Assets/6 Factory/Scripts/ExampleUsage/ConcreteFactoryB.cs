using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    public class ConcreteFactoryB : Factory
    {
        // used to create a Prefab
        [SerializeField] private ProductB productPrefab;

        public override IProduct GetProduct(Vector3 position)
        {
            // create a Prefab instance and get the product component
            GameObject instance = Instantiate(productPrefab.gameObject, position, Quaternion.identity);
            ProductB newProduct = instance.GetComponent<ProductB>();

            // each product contains its own logic
            newProduct.Initialize();

            // add any unique behavior to this factory
            instance.name = newProduct.ProductName;
            Debug.Log(GetLog(newProduct));

            return newProduct;
        }
    }
}
