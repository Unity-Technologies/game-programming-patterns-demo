using System;
using UnityEngine;

using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace DesignPatterns.ISP
{

    public class ExplosionEffect : MonoBehaviour
    {
        [SerializeField] private float explosionForce = 1000f;
     
        [Tooltip("All child debris pieces")]
        [SerializeField] List<Rigidbody> m_Rigidbodies = new List<Rigidbody>();

        void Start()
        {
            Explode();
        }

        public void Explode()
        {
            foreach (Rigidbody rigidbody in m_Rigidbodies)
            {
                
                // Apply an explosion force
                Vector3 explosionDirection = (rigidbody.position - transform.position).normalized;
                Vector3 force = explosionDirection * explosionForce + Random.insideUnitSphere * (explosionForce * 0.5f);
                
                rigidbody.AddForce(force);
                
                // Apply an explosion torque
                rigidbody.AddTorque(Random.insideUnitSphere * explosionForce);
            }
        }
    }
}
