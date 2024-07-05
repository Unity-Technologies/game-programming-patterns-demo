using DesignPatterns.LSP;
using Unity.Mathematics;
using UnityEngine;


namespace DesignPatterns.ISP
{
    /// <summary>
    /// Alternative type of target that can explode and instantiate an effect on death. Here we
    /// inherit from the base Target and add the IExplodable interface
    /// </summary>
    public class ExplodableTarget : Target, IExplodable
    {
         [Tooltip("Effect to instantiate on explosion")]
        [SerializeField] GameObject m_ExplosionPrefab;

        protected override void Die()
        {
            base.Die();
            Explode();
        }

        public void Explode()
        {
            if (m_ExplosionPrefab)
            {
                GameObject instance = Instantiate(m_ExplosionPrefab, transform.position, quaternion.identity);
            }

            // Add custom explosion logic here
        }
    }
}