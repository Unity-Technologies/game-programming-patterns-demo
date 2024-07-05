using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.ObjectPool
{
    // This is an example client that uses our simple object pool.
    public class ExampleGun : MonoBehaviour
    {
        [Tooltip("Prefab to shoot")]
        [SerializeField] private GameObject projectile;
        [Tooltip("Projectile force")]
        [SerializeField] float muzzleVelocity = 700f;
        [Tooltip("End point of gun where shots appear")]
        [SerializeField] private Transform muzzlePosition;
        [Tooltip("Time between shots / smaller = higher rate of fire")]
        [SerializeField] float cooldownWindow = 0.1f;
        [Tooltip("Reference to Object Pool")]
        [SerializeField] ObjectPool objectPool;

        private float nextTimeToShoot;

        private void FixedUpdate()
        {
            // shoot if we have exceeded delay
            if (Input.GetButton("Fire1") && Time.time > nextTimeToShoot && objectPool != null)
            {

                // get a pooled object instead of instantiating
                GameObject bulletObject = objectPool.GetPooledObject().gameObject;

                if (bulletObject == null)
                    return;

                bulletObject.SetActive(true);

                // align to gun barrel/muzzle position
                bulletObject.transform.SetPositionAndRotation(muzzlePosition.position, muzzlePosition.rotation);

                // move projectile forward
                bulletObject.GetComponent<Rigidbody>().AddForce(bulletObject.transform.forward * muzzleVelocity, ForceMode.Acceleration);

                // turn off after a few seconds
                ExampleProjectile projectile = bulletObject.GetComponent<ExampleProjectile>(); 
                projectile?.Deactivate();

                // set cooldown delay
                nextTimeToShoot = Time.time + cooldownWindow;

            }
        }
    }
}


