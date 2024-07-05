using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public enum PlayerControllerState
    {
        Idle,
        Walk,
        Jump
    }

    public class UnrefactoredPlayerController : MonoBehaviour
    {
        // this works but does not scale; you would need to add a case
        // each time you created a new internal state. Use the state pattern instead
        private PlayerControllerState state;

        private void Update()
        {
            GetInput();

            switch (state)
            {
                case PlayerControllerState.Idle:
                    Idle();
                    break;
                case PlayerControllerState.Walk:
                    Walk();
                    break;
                case PlayerControllerState.Jump:
                    Jump();
                    break;
            }
        }

        private void GetInput()
        {
            // process walk and jump controls
        }

        private void Walk()
        {
            // walk logic
        }

        private void Idle()
        {
            // idle logic
        }

        private void Jump()
        {
            // jump logic
        }
    }
}
