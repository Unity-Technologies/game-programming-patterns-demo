using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    public class IdleState : IState
    {

        private PlayerController player;

        // color to change player (alternately: pass in color value with constructor)
        Color meshColor = Color.white;
        public Color MeshColor { get => meshColor; set => meshColor = value; }

        // pass in any parameters you need in the constructors
        public IdleState(PlayerController player)
        {
            this.player = player;
        }

        public void Enter()
        {
            // code that runs when we first enter the state
            //Debug.Log("Entering Idle State");
        }

        // per-frame logic, include condition to transition to a new state
        public void Execute()
        {
            // if we're no longer grounded, transition to jumping
            if (!player.IsGrounded)
            {
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.jumpState);
            }

            // if we move above a minimum threshold, transition to walking
            if (Mathf.Abs(player.CharController.velocity.x) > 0.1f || Mathf.Abs(player.CharController.velocity.z) > 0.1f)
            {
                player.PlayerStateMachine.TransitionTo(player.PlayerStateMachine.walkState);
            }
        }

        public void Exit()
        {
            // code that runs when we exit the state
            //Debug.Log("Exiting Idle State");
        }
    }
}
