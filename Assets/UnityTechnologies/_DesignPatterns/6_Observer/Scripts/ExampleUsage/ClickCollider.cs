using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{
    [RequireComponent(typeof(Collider), typeof(ButtonSubject))]
    public class ClickCollider : MonoBehaviour
    {
        private ButtonSubject m_ButtonSubject;
        private Collider m_Collider;

        void Start()
        {
            m_ButtonSubject = GetComponent<ButtonSubject>();
            m_Collider = GetComponent<Collider>();
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
                        m_ButtonSubject.ClickButton();
                    }
                }
            }
        }
    }
}
