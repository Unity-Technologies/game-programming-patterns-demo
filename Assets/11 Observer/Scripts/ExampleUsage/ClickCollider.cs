using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{
    [RequireComponent(typeof(Collider), typeof(ButtonSubject))]
    public class ClickCollider : MonoBehaviour
    {
        private ButtonSubject subject;
        private Collider collider;

        void Start()
        {
            subject = GetComponent<ButtonSubject>();
            collider = GetComponent<Collider>();
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
                        subject.ClickButton();
                    }
                }
            }
        }
    }
}
