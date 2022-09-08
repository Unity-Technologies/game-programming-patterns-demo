using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{
    // old implementation

    //public class Door : MonoBehaviour
    //{
    //    public void Open()
    //    {
    //        Debug.Log("The door is open.");
    //    }

    //    public void Close()
    //    {
    //        Debug.Log("The door is closed.");
    //    }
    //}


    // revised implementation with ISwitchable

    public class Door : MonoBehaviour, ISwitchable
    {
        private bool isActive;
        public bool IsActive => isActive;

        public void Activate()
        {
            isActive = true;
            Debug.Log("The door is open.");
        }

        public void Deactivate()
        {
            isActive = false;
            Debug.Log("The door is closed.");
        }
    }

}
