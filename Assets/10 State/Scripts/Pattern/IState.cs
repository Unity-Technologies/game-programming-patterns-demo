using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.State
{
    public interface IState: IColorable
    {
        public void Enter()
        {
            // code that runs when we first enter the state
        }

        public void Update()
        {
            // per-frame logic, include condition to transition to a new state
        }

        public void Exit()
        {
            // code that runs when we exit the state
        }
    }
}
