using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.MVP
{
    [RequireComponent(typeof(HealthPresenter), typeof(Collider))]
    public class ClickDamage : MonoBehaviour
    {
        private Collider collider;
        private HealthPresenter healthPresenter;
        [SerializeField] private LayerMask layerToClick;
        [SerializeField] private int damageValue = 10;

        private void Start()
        {
            collider = GetComponent<Collider>();
            healthPresenter = GetComponent<HealthPresenter>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, Mathf.Infinity, layerToClick))
                {
                    healthPresenter?.Damage(damageValue);
                }
            }
        }
    }
}
