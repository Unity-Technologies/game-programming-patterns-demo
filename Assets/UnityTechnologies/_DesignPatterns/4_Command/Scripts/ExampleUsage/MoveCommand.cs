using UnityEngine;

namespace DesignPatterns.Command
{
    // An example of a simple command object, implementing ICommand
    public class MoveCommand : ICommand
    {
        private PlayerMover m_PlayerMover;
        private Vector3 m_MoveVector;

        // Constructor
        public MoveCommand(PlayerMover player, Vector3 moveVector)
        {
            this.m_PlayerMover = player;
            this.m_MoveVector = moveVector;
        }

        // Logic of thing to do goes here
        public void Execute()
        {
            // Add point to path visualization
            m_PlayerMover.PlayerPath.AddToPath(m_PlayerMover.transform.position + m_MoveVector);

            // Move by vector
            m_PlayerMover.Move(m_MoveVector);
        }
        // Undo logic goes here
        public void Undo()
        {
            // Move opposite direction
            m_PlayerMover.Move(-m_MoveVector);

            // Remove point from path visualization
            m_PlayerMover.PlayerPath.RemoveFromPath();
 
        }
    }
}