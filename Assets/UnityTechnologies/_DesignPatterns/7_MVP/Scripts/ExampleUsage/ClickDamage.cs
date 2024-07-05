using System.Collections;
using System.Collections.Generic;
using DesignPatterns.MVVM;
using UnityEngine;

namespace DesignPatterns.MVP
{
    [RequireComponent(typeof(Collider))]
    public class ClickDamage : MonoBehaviour
    {
        [SerializeField] private LayerMask m_LayerMask;
        [SerializeField] private int m_DamageValue = 10;

        private Collider m_Collider;
        [SerializeField] HealthPresenter m_HealthPresenter;
        [SerializeField] HealthModel m_HealthModel;

        private void Awake()
        {
            m_Collider = GetComponent<Collider>();
            m_HealthPresenter = GetComponent<HealthPresenter>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, Mathf.Infinity, m_LayerMask))
                {
                    m_HealthPresenter?.Damage(m_DamageValue);
                }
            }
        }
    }
}
