using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.DIP
{
    /// <summary>
    /// Represents a switch mechanism in its unrefactored form, directly controlling a door or trap.
    /// It directly depends on concrete classes (UnrefactoredDoor, UnrefactoredTrap), which makes it
    /// less flexible and tightly coupled to the specific implementations of the mechanisms it controls.
    /// </summary>
    public class UnrefactoredSwitch : MonoBehaviour
    {

        public UnrefactoredTrap Trap;
        public UnrefactoredDoor Door;
        public bool IsActivated;
        
        public void Activate()
        {
            if (IsActivated)
            {
                IsActivated = false;
                Door.Close();
                Trap.Disable();
            }
            else
            {
                IsActivated = true;
                Door.Open();
                Trap.Enable();
            }
        }

    }
}
