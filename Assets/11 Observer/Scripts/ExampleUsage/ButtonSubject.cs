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

        private Collider collider;

        void Start()
        {
            collider = GetComponent<Collider>();
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
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                if (Physics.Raycast(ray, out hitInfo, 100f))
                {
                    if (hitInfo.collider == this.collider)
                    {
                        ClickButton();
                    }
                }
            }
        }
    }
}

