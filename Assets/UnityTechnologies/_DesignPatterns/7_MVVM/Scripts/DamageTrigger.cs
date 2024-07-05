using System;
using DesignPatterns.Utilities;
using UnityEngine;

namespace DesignPatterns.MVVM
{
    [RequireComponent(typeof(Collider))]
    public class DamageTrigger : MonoBehaviour
    {
        [SerializeField] private LayerMask m_LayerMask;
        [SerializeField] private int m_DamageValue = 10;
        [SerializeField] private HealthViewModel m_HealthViewModel;

        private Collider m_Collider;

        private void Awake()
        {
            m_Collider = GetComponent<Collider>();
        }

        private void Start()
        {
            // We use our utility class to validate required fields
            NullRefChecker.Validate(this);
        }
        
        private void Update()
        {
            if (m_HealthViewModel is null)
                return;
            
            // We check for mouse input 
            if (Input.GetMouseButtonDown(0))
            {
                // We create a ray from the camera to the mouse position
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                // We check if the raycast hits the collider
                if (Physics.Raycast(ray, Mathf.Infinity, m_LayerMask))
                {
                    m_HealthViewModel.ApplyDamage(m_DamageValue);
                }
            }
        }
    }
}
