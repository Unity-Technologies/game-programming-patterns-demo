using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ObjectPool
{
    public class ObjectPool : MonoBehaviour
    {
        // initial number of cloned objects
        [SerializeField] private uint initPoolSize;
        public uint InitPoolSize => initPoolSize;

        // PooledObject prefab
        [SerializeField] private PooledObject objectToPool;

        // store the pooled objects in stack
        private Stack<PooledObject> stack;

        private void Start()
        {
            SetupPool();
        }

        // creates the pool (invoke when the lag is not noticeable)
        private void SetupPool()
        {
            // missing objectToPool Prefab field
            if (objectToPool == null)
            {
                return;
            }

            stack = new Stack<PooledObject>();

            // populate the pool
            PooledObject instance = null;

            for (int i = 0; i < initPoolSize; i++)
            {
                instance = Instantiate(objectToPool);
                instance.Pool = this;
                instance.gameObject.SetActive(false);
                stack.Push(instance);
            }
        }

        // returns the first active GameObject from the pool
        public PooledObject GetPooledObject()
        {
            // missing objectToPool field
            if (objectToPool == null)
            {
                return null;
            }

            // if the pool is not large enough, instantiate extra PooledObjects
            if (stack.Count == 0)
            {
                PooledObject newInstance = Instantiate(objectToPool);
                newInstance.Pool = this;
                return newInstance;
            }

            // otherwise, just grab the next one from the list
            PooledObject nextInstance = stack.Pop();
            nextInstance.gameObject.SetActive(true);
            return nextInstance;
        }

        // returns the GameObject to the pool
        public void ReturnToPool(PooledObject pooledObject)
        {
            stack.Push(pooledObject);
            pooledObject.gameObject.SetActive(false);
        }
    }
}
