using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.SRP
{
    // sample code to demo single-responsibility: 
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private string inputAxisName;

        private void Update()
        {
            float delta = Input.GetAxis(inputAxisName) * Time.deltaTime;
        }
    }
}
