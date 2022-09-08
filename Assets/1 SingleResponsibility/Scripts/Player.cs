using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.SRP
{
    [RequireComponent(typeof(PlayerAudio), typeof(PlayerInput), typeof(PlayerMovement))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerAudio playerAudio;
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private PlayerMovement playerMovement;

        private void Start()
        {
            playerAudio = GetComponent<PlayerAudio>();
            playerInput = GetComponent<PlayerInput>();
            playerMovement = GetComponent<PlayerMovement>();
        }
    }


}
