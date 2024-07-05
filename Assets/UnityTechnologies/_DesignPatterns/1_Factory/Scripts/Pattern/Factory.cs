using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Factory
{
    /// <summary>
    /// Serves as the base class for all factory types. Factories create instances of products.
    /// </summary>
    public abstract class Factory : MonoBehaviour
    {
        // Abstract method to get a product instance.
        public abstract IProduct GetProduct(Vector3 position);

        // Shared method with all factories.
        public string GetLog(IProduct product)
        {
            string logMessage = "Factory: created product " + product.ProductName;
            return logMessage;
        }
    }
}