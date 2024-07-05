using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace DesignPatterns.SRP
{
    /// <summary>
    /// Demonstrates a non-Single Responsibility Principle (SRP) approach to player functionality in Unity.
    /// 
    /// This script merges multiple responsibilities like movement control, input handling,
    /// audio management, and particle effects into a single class. 
    /// 
    /// While currently manageable due to its small size, this approach may lead to difficulties in scaling,
    /// maintaining, and extending the code. 
    /// </summary>
    public class UnrefactoredPlayer : MonoBehaviour
    {

        [Header("Movement")]
        [Tooltip("Horizontal speed")]
        [SerializeField] private float moveSpeed = 5f;
        [Tooltip("Rate of change for move speed")]
        [SerializeField] private float acceleration = 10f;
        [Tooltip("Deceleration rate when no input is provided")]
        [SerializeField] private float deceleration = 5f;

        [Header("Controls")]
        [Tooltip("Use WASD keys to move")]
        [SerializeField] private KeyCode forwardKey = KeyCode.W;
        [SerializeField] private KeyCode backwardKey = KeyCode.S;
        [SerializeField] private KeyCode leftKey = KeyCode.A;
        [SerializeField] private KeyCode rightKey = KeyCode.D;

        [Header("Collision")]
        [SerializeField] private LayerMask obstacleLayer;

        [Header("Audio")]
        [SerializeField] private AudioClip[] bounceClips;
        [SerializeField] private float audioCooldownTime = 2f;
        private float lastAudioPlayedTime;

        [Header("Effects")]
        [SerializeField] private ParticleSystem m_ParticleSystem;
        private const float effectCooldown = 1f;
        private float timeToNextEffect = -1f;

        private Vector3 inputVector;
        private float currentSpeed = 0f;
        private CharacterController charController;
        private float initialYPosition;
        private AudioSource audioSource;

        private void Awake()
        {
            charController = GetComponent<CharacterController>();
            initialYPosition = transform.position.y;
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            lastAudioPlayedTime = -audioCooldownTime;

        }

        private void Update()
        {
            HandleInput();
            Move(inputVector);
        }

        private void HandleInput()
        {
            // Reset input vector
            float xInput = 0;
            float zInput = 0;

            if (Input.GetKey(forwardKey))
                zInput++;
            if (Input.GetKey(backwardKey))
                zInput--;
            if (Input.GetKey(leftKey))
                xInput--;
            if (Input.GetKey(rightKey))
                xInput++;

            inputVector = new Vector3(xInput, 0, zInput);
        }

        private void Move(Vector3 inputVector)
        {
            if (inputVector == Vector3.zero)
            {
                if (currentSpeed > 0)
                {
                    currentSpeed -= deceleration * Time.deltaTime;
                    currentSpeed = Mathf.Max(currentSpeed, 0);
                }
            }
            else
            {
                currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * acceleration);
            }

            Vector3 movement = inputVector.normalized * currentSpeed * Time.deltaTime;
            charController.Move(movement);
            transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
        }

        public void PlayRandomAudioClip()
        {
            // If the time to play the next clip has passed and there are clips available we play a random clip.
            if (Time.time > (audioCooldownTime + lastAudioPlayedTime))
            {
                lastAudioPlayedTime = Time.time;
                audioSource.clip = bounceClips[Random.Range(0, bounceClips.Length)];
                audioSource.Play();
            }
        }

        public void PlayEffect()
        {
            if (Time.time < timeToNextEffect)
                return;

            if (m_ParticleSystem != null)
            {
                m_ParticleSystem.Stop();
                m_ParticleSystem.Play();
                timeToNextEffect = Time.time + effectCooldown;
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            // Check if the collided object's layer is in the obstacleLayer LayerMask.
            if ((obstacleLayer.value & (1 << hit.gameObject.layer)) > 0)
            {
                PlayRandomAudioClip();
                PlayEffect();
            }

        }

    }
}
