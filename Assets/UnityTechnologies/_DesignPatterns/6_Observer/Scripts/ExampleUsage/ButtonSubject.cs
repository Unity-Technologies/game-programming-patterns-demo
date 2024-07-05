using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace DesignPatterns.Observer
{
    [RequireComponent(typeof(Collider))]
    public class ButtonSubject: MonoBehaviour
    {
        public event Action Clicked;

        private Collider m_Collider;

        void Start()
        {
            m_Collider = GetComponent<Collider>();
        }

        public void ClickButton()
        {
            Clicked?.Invoke();
        }

        void Update()
        {
            CheckCollider();
        }

        private void CheckCollider()
        {
            // Check if the mouse left button is pressed over the collider
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100f))
                {
                    if (hitInfo.collider == this.m_Collider)
                    {
                        ClickButton();
                    }
                }
            }
        }
    }
}

