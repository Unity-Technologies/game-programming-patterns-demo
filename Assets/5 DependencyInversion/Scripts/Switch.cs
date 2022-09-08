using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{
    public class Switch : MonoBehaviour
    {
        // old implementation:

        //public Door Door;
        //public bool IsActivated;

        //public void Activate()
        //{
        //    if (IsActivated)
        //    {
        //        IsActivated = false;
        //        Door.Close();
        //    }
        //    else
        //    {
        //        IsActivated = true;
        //        Door.Open();
        //    }
        //}

        // new implementation with ISwitchable client

        public ISwitchable client;

        public void Toggle()
        {
            if (client.IsActive)
            {
                client.Deactivate();
            }
            else
            {
                client.Activate();
            }
        }
    }
}
