using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.StatePattern
{
    // Simple FPS Controller (logic from FPS Starter)
    [RequireComponent(typeof(PlayerInput), typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private PlayerInput playerInput;
        private SimplePlayerStateMachine playerStateMachine;

        [Header("Movement")]
        [Tooltip("Horizontal speed")]
        [SerializeField] private float moveSpeed = 5f;
        [Tooltip("Rate of change for move speed")]
        [SerializeField] private float acceleration = 10f;
        [Tooltip("Max height to jump")]
        [SerializeField] private float jumpHeight = 1.25f;

        [Tooltip("Custom gravity for player")]
        [SerializeField] private float gravity = -15f;
        [Tooltip("Time between jumps")]
        [SerializeField] private float jumpTimeout = 0.1f;

        [SerializeField] private bool isGrounded = true;
        [SerializeField] private float groundedRadius = 0.5f;
        [SerializeField] private float groundedOffset = 0.15f;
        [SerializeField] private LayerMask groundLayers;

        public CharacterController CharController => charController;
        public bool IsGrounded => isGrounded;
        public SimplePlayerStateMachine PlayerStateMachine => playerStateMachine;


        private CharacterController charController;
        private float targetSpeed;
        private float verticalVelocity;
        private float jumpCooldown;

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            charController = GetComponent<CharacterController>();

            // initialize state machine
            playerStateMachine = new SimplePlayerStateMachine(this);
        }

        private void Start()
        {
            playerStateMachine.Initialize(playerStateMachine.idleState);

        }

        private void Update()
        {
            // update the current State
            playerStateMachine.Execute();
        }

        private void LateUpdate()
        {
            CalculateVertical();
            Move();
        }

        private void Move()
        {
            Vector3 inputVector = playerInput.InputVector;

            // If we are not providing movement input, set target speed to 0
            if (inputVector == Vector3.zero)
            {
                targetSpeed = 0;
            }

            // If we are not at target speed (outside of tolerance), lerp to the target speed
            float currentHorizontalSpeed = new Vector3(charController.velocity.x, 0.0f, charController.velocity.z).magnitude;
            float tolerance = 0.1f;

            // If we are not at target speed (outside of tolerance), we lerp to the target speed to get a smooth transition
            if (currentHorizontalSpeed < targetSpeed - tolerance || currentHorizontalSpeed > targetSpeed + tolerance)
            {
                targetSpeed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed, Time.deltaTime * acceleration);
                targetSpeed = Mathf.Round(targetSpeed * 1000f) / 1000f;
            }
            else
            {
                targetSpeed = moveSpeed;
            }
            // Move the player
            charController.Move((inputVector.normalized * targetSpeed * Time.deltaTime) + new Vector3(0f, verticalVelocity, 0f) * Time.deltaTime);

        }

        private void CalculateVertical()
        {
            if (isGrounded)
            {
                if (verticalVelocity < 0f)
                {
                    verticalVelocity = -2f;
                }

                if (playerInput.IsJumping && jumpCooldown <= 0f)
                {
                    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
                }

                if (jumpCooldown >= 0f)
                {
                    jumpCooldown -= Time.deltaTime;
                }
            }
            else
            {
                jumpCooldown = jumpTimeout;
                playerInput.IsJumping = false;
            }

            verticalVelocity += gravity * Time.deltaTime;

            // check if grounded
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z);
            isGrounded = Physics.CheckSphere(spherePosition, 0.5f, groundLayers, QueryTriggerInteraction.Ignore);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (isGrounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + groundedOffset, transform.position.z), groundedRadius);
        }
    }
}
