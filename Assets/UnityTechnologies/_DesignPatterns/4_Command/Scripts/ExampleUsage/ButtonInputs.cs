using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace DesignPatterns.Command
{
    public class ButtonInputs : MonoBehaviour
    {
        [Header("Key Controls")]
        [SerializeField] private KeyCode m_ForwardKey = KeyCode.W;
        [SerializeField] private KeyCode m_BackKey = KeyCode.S;
        [SerializeField] private KeyCode m_LeftKey = KeyCode.A;
        [SerializeField] private KeyCode m_RightKey = KeyCode.D;
        [SerializeField] private KeyCode m_UndoKey = KeyCode.U;
        [SerializeField] private KeyCode m_RedoKey = KeyCode.R;
        
        // UI Button controls
        [Header("Button Controls")]
        [SerializeField] private Button m_ForwardButton;
        [SerializeField] private Button m_BackButton;
        [SerializeField] private Button m_LeftButton;
        [SerializeField] private Button m_RightButton;
        [SerializeField] private Button m_UndoButton;
        [SerializeField] private Button m_RedoButton;

        [SerializeField] private PlayerMover m_Player;

        private void Start()
        {
            // button setup
            m_ForwardButton.onClick.AddListener(OnForwardInput);
            m_BackButton.onClick.AddListener(OnBackInput);
            m_RightButton.onClick.AddListener(OnRightInput);
            m_LeftButton.onClick.AddListener(OnLeftInput);
            m_UndoButton.onClick.AddListener(OnUndoInput);
            m_RedoButton.onClick.AddListener(OnRedoInput);
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

        private void Update()
        {
            // Check for forward movement key
            if (Input.GetKeyDown(m_ForwardKey))
            {
                m_ForwardButton.onClick.Invoke();
            }

            // Check for back movement key
            if (Input.GetKeyDown(m_BackKey))
            {
                m_BackButton.onClick.Invoke();
            }

            // Check for left movement key
            if (Input.GetKeyDown(m_LeftKey))
            {
                m_LeftButton.onClick.Invoke();
            }

            // Check for right movement key
            if (Input.GetKeyDown(m_RightKey))
            {
                m_RightButton.onClick.Invoke();
            }

            // Check for undo key
            if (Input.GetKeyDown(m_UndoKey))
            {
                m_UndoButton.onClick.Invoke();
            }

            // Check for redo key
            if (Input.GetKeyDown(m_RedoKey))
            {
                m_RedoButton.onClick.Invoke();
            }
        }


        private void OnLeftInput()
        {
            RunPlayerCommand(m_Player, Vector3.left);
        }

        private void OnRightInput()
        {
            RunPlayerCommand(m_Player, Vector3.right);
        }

        private void OnForwardInput()
        {
            RunPlayerCommand(m_Player, Vector3.forward);
        }

        private void OnBackInput()
        {
            RunPlayerCommand(m_Player, Vector3.back);
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