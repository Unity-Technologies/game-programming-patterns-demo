using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

namespace DesignPatterns.ObjectPool
{
    public class RevisedGun : MonoBehaviour
    {
        [Tooltip("Prefab to shoot")]
        [SerializeField] private RevisedProjectile projectilePrefab;
        [Tooltip("Projectile force")]
        [SerializeField] private float muzzleVelocity = 1500f;
        [Tooltip("End point of gun where shots appear")]
        [SerializeField] private Transform muzzlePosition;
        [Tooltip("Time between shots / smaller = higher rate of fire")]
        [SerializeField] private float cooldownWindow = 0.1f;

        [SerializeField] private UnityEvent m_GunFired;

        // Stack-based ObjectPool available with Unity 2021 and above
        private IObjectPool<RevisedProjectile> objectPool;

        // Throw an exception if we try to return an existing item, already in the pool
        [SerializeField] private bool collectionCheck = true;

        // extra options to control the pool capacity and maximum size
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        private float nextTimeToShoot;

        private void Awake()
        {
            objectPool = new ObjectPool<RevisedProjectile>(CreateProjectile,
                OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject,
                collectionCheck, defaultCapacity, maxSize);
        }

        // invoked when creating an item to populate the object pool
        private RevisedProjectile CreateProjectile()
        {
            RevisedProjectile projectileInstance = Instantiate(projectilePrefab);
            projectileInstance.ObjectPool = objectPool;
            return projectileInstance;
        }

        // Invoked when returning an item to the object pool
        private void OnReleaseToPool(RevisedProjectile pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        // Invoked when retrieving the next item from the object pool
        private void OnGetFromPool(RevisedProjectile pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        // Invoked when we exceed the maximum number of pooled items (i.e. destroy the pooled object)
        private void OnDestroyPooledObject(RevisedProjectile pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        private void FixedUpdate()
        {
            // Shoot if we have exceeded delay
            if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot && objectPool != null)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            // Get a pooled object instead of instantiating
            RevisedProjectile bulletObject = objectPool.Get();

            if (bulletObject == null)
                return;

            // Align to gun barrel/muzzle position
            bulletObject.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

            // Move projectile forward
            bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.forward * muzzleVelocity, ForceMode.Acceleration);

            // Turn off after a few seconds
            bulletObject.Deactivate();

            // Set cooldown delay
            nextTimeToShoot = Time.time + cooldownWindow;
            
            m_GunFired.Invoke();
        }
    }
}
