using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DesignPatterns.Command
{
    public class InputManager : MonoBehaviour
    {
        // UI Button controls
        [Header("Button Controls")]
        [SerializeField] Button forwardButton;
        [SerializeField] Button backButton;
        [SerializeField] Button leftButton;
        [SerializeField] Button rightButton;
        [SerializeField] Button undoButton;
        [SerializeField] Button redoButton;

        [SerializeField] private PlayerMover player;

        private void Start()
        {
            // button setup
            forwardButton.onClick.AddListener(OnForwardInput);
            backButton.onClick.AddListener(OnBackInput);
            rightButton.onClick.AddListener(OnRightInput);
            leftButton.onClick.AddListener(OnLeftInput);
            undoButton.onClick.AddListener(OnUndoInput);
            redoButton.onClick.AddListener(OnRedoInput);
        }

        private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
        {
            if (playerMover == null)
            {
                return;
            }

            // check if movement is unobstructed
            if (playerMover.IsValidMove(movement))
            {
                // issue the command and save to undo stack
                ICommand command = new MoveCommand(playerMover, movement);

                // we run the command immediately here, but you can also delay this for extra control over the timing
                CommandInvoker.ExecuteCommand(command);
            }
        }

        private void OnLeftInput()
        {
            RunPlayerCommand(player, Vector3.left);
        }

        private void OnRightInput()
        {
            RunPlayerCommand(player, Vector3.right);
        }

        private void OnForwardInput()
        {
            RunPlayerCommand(player, Vector3.forward);
        }

        private void OnBackInput()
        {
            RunPlayerCommand(player, Vector3.back);
        }

        private void OnUndoInput()
        {
            CommandInvoker.UndoCommand();
        }

        private void OnRedoInput()
        {
            CommandInvoker.RedoCommand();
        }
    }
}