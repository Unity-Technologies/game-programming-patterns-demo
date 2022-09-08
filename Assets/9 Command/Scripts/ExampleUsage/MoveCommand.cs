using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
    // an example of a simple command object, implementing ICommand
    public class MoveCommand : ICommand
    {
        private PlayerMover _playerMover;
        private Vector3 _movement;

        // pass parameters into the constructor
        public MoveCommand(PlayerMover player, Vector3 moveVector)
        {
            this._playerMover = player;
            this._movement = moveVector;
        }

        // logic of thing to do goes here
        public void Execute()
        {
            // add point to path visualization
            _playerMover?.PlayerPath.AddToPath(_playerMover.transform.position + _movement);

            // move by vector
            _playerMover.Move(_movement);
        }
        // undo logic goes here
        public void Undo()
        {
            // move opposite direction
            _playerMover.Move(-_movement);

            // remove point from path visualization
            _playerMover?.PlayerPath.RemoveFromPath();
 
        }
    }
}