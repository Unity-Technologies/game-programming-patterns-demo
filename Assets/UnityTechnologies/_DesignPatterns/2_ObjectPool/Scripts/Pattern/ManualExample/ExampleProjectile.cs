using System.Collections;
using UnityEngine;

namespace DesignPatterns.ObjectPool
{
    // A projectile used for the object pool elements.
    [RequireComponent(typeof(PooledObject))]
    public class ExampleProjectile : MonoBehaviour
    {
        // deactivate after delay
        [SerializeField] private float timeoutDelay = 3f;

        // reference to the PooledObject component so we can return to the pool
        private PooledObject pooledObject;

        private void Awake()
        {
            pooledObject = GetComponent<PooledObject>();
        }

        public void Deactivate()
        {
            StartCoroutine(DeactivateRoutine(timeoutDelay));
        }

        IEnumerator DeactivateRoutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            // reset the moving Rigidbody
            Rigidbody rBody = GetComponent<Rigidbody>();
            rBody.linearVelocity = new Vector3(0f, 0f, 0f);
            rBody.angularVelocity = new Vector3(0f, 0f, 0f);

            // set inactive and return to pool
            pooledObject.Release();
            gameObject.SetActive(false);
        }
    }
}
