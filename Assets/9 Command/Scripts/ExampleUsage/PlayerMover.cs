using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Command
{
    // moves the player one space and checks for walls
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private LayerMask obstacleLayer;
        
        private const float boardSpacing = 1f;

        // visualize path
        private PlayerPath playerPath;
        public PlayerPath PlayerPath => playerPath;


        private void Start()
        {
            playerPath = gameObject.GetComponent<PlayerPath>();
        }

        // simple movement along XZ-plane
        public void Move(Vector3 movement)
        {
            Vector3 destination = transform.position + movement;
            transform.position = destination;
        }

        public bool IsValidMove(Vector3 movement)
        {
            return !Physics.Raycast(transform.position, movement, boardSpacing, obstacleLayer);
        }
    }
}