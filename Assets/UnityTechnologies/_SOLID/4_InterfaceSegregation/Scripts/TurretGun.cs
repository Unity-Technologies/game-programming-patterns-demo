using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using DesignPatterns.Utilities;

namespace DesignPatterns.ISP
{
    /// <summary>
    /// This represents a gun that shoots projectiles from an ObjectPool.
    /// </summary>
    public class TurretGun : MonoBehaviour
    {
        [Tooltip("Prefab to shoot")] 
        [SerializeField]
        private Projectile m_ProjectilePrefab;

        [Tooltip("Projectile force")] 
        [SerializeField]
        private float m_MuzzleVelocity = 700f;

        [Tooltip("End point of gun where shots appear")] 
        [SerializeField]
        private Transform m_MuzzlePosition;

        [Tooltip("Time between shots / smaller = higher rate of fire")] 
        [SerializeField]
        private float m_CooldownWindow = 0.1f;

        [Tooltip("Throw errors if we try to release an item that is already in the pool")] 
        [SerializeField] private bool m_CollectionCheck = true;
        
        [Tooltip("Default pool size")] 
        [SerializeField] private int m_DefaultCapacity = 20;
        
        [Tooltip("Pool can expand to this limit")] 
        [SerializeField] private int m_MaxSize = 100;

        [SerializeField] private UnityEvent m_GunFired;
        [SerializeField] ScreenDeadZone m_ScreenDeadZone;
        
        private IObjectPool<Projectile> objectPool;
        private float nextTimeToShoot;

        private void Awake()
        {
            // Create the object pool
            objectPool = new ObjectPool<Projectile>(CreateProjectile, OnGetFromPool, OnReleaseToPool,
                OnDestroyPooledObject, m_CollectionCheck, m_DefaultCapacity, m_MaxSize);
        }

        private Projectile CreateProjectile()
        {
            Projectile projectileInstance = Instantiate(m_ProjectilePrefab);
            projectileInstance.Initialize(objectPool, m_MuzzleVelocity);
            return projectileInstance;
        }

        private void OnReleaseToPool(Projectile pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }

        private void OnGetFromPool(Projectile pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }

        private void OnDestroyPooledObject(Projectile pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }

        private void FixedUpdate()
        {
            if (m_ScreenDeadZone.IsMouseInDeadZone())
                return;
            
            // Shoot when the Fire1 button is pressed and cooldown time has passed
            if (Input.GetButton("Fire1") && Time.fixedTime >= nextTimeToShoot)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            // Get a projectile from the object pool
            Projectile bulletObject = objectPool.Get();
            // Launch the projectile
            bulletObject.Launch(m_MuzzlePosition.position, m_MuzzlePosition.rotation);
            // Set the next time to shoot
            nextTimeToShoot = Time.fixedTime + m_CooldownWindow;
            
            m_GunFired.Invoke();
        }
    }
}