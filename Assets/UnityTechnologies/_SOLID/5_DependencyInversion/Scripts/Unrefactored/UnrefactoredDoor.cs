using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{

    public class UnrefactoredDoor : MonoBehaviour
    {
        public void Open()
        {
            Debug.Log("The door is open.");
        }

        public void Close()
        {
            Debug.Log("The door is closed.");
        }
    }
}
