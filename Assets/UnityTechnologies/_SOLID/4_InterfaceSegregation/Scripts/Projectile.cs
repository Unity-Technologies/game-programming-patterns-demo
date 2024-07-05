using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

namespace DesignPatterns.ISP
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private int m_DamageValue = 5;

        // Deactivate after delay
        [SerializeField] private float m_Lifetime = 3f;
        private IObjectPool<Projectile> m_ObjectPool;
        private Rigidbody m_Rigidbody;
        private float m_MuzzleVelocity;

        // Public property to give the projectile a reference to its ObjectPool
        public IObjectPool<Projectile> ObjectPool
        {
            set => m_ObjectPool = value;
        }

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            CheckCollisionInterfaces(collision);
            DeactivateProjectile();
        }

        private void DeactivateProjectile()
        {
            m_Rigidbody.linearVelocity = Vector3.zero;
            m_Rigidbody.angularVelocity = Vector3.zero;

            m_ObjectPool.Release(this);
        }

        private void CheckCollisionInterfaces(Collision collision)
        {
            // Get the first contact
            ContactPoint contactPoint = collision.GetContact(0);

            // Slight offset to move it outside of the surface
            float pushDistance = 0.1f;
            Vector3 offsetPosition = contactPoint.point + contactPoint.normal * pushDistance;

            var monoBehaviours = collision.gameObject.GetComponents<MonoBehaviour>();
            foreach (var monoBehaviour in monoBehaviours)
            {
                HandleDamageableInterface(monoBehaviour);
                HandleEffectTriggerInterface(monoBehaviour, offsetPosition);
            }
        }

        private void HandleDamageableInterface(MonoBehaviour monoBehaviour)
        {
            if (monoBehaviour is IDamageable damageable)
            {
                damageable.TakeDamage(m_DamageValue);
            }
        }

        private void HandleEffectTriggerInterface(MonoBehaviour monoBehaviour, Vector3 position)
        {
            if (monoBehaviour is IEffectTrigger effectTrigger)
            {
                effectTrigger.TriggerEffect(position);
            }
        }

        public void Initialize(IObjectPool<Projectile> pool, float velocity)
        {
            ObjectPool = pool;

            // Cache Rigidbody or other necessary components here
            m_MuzzleVelocity = velocity;
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        public void Launch(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);

            // Use cached Rigidbody to apply force
            m_Rigidbody.AddForce(transform.forward * m_MuzzleVelocity, ForceMode.Acceleration);
            
            StartCoroutine(LifetimeCoroutine());
        }
        
        private IEnumerator LifetimeCoroutine()
        {
            yield return new WaitForSeconds(m_Lifetime);
            DeactivateProjectile();
        }
    }
}