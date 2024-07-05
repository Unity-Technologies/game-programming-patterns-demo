using System;
using DesignPatterns.Utilities;
using UnityEngine;
using UnityEngine.Serialization;


namespace DesignPatterns.MVP_UIToolkit
{
    [RequireComponent(typeof(Collider))]
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask m_LayerMask;
        [SerializeField] private int m_DamageValue = 10;
        [SerializeField] private HealthPresenter m_HealthPresenter;

        private Collider m_Collider;

        private void Start()
        {
            NullRefChecker.Validate(this);
            m_Collider = GetComponent<Collider>();
        }
        
        private void Update()
        {
            if (m_HealthPresenter is null)
                return;
            
            // Check if the mouse left button is pressed over the collider
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, Mathf.Infinity, m_LayerMask))
                {
                    m_HealthPresenter.ApplyDamage(m_DamageValue);
                }
            }
        }
    }
}
