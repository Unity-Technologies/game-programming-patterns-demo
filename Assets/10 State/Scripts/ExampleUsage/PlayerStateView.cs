using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DesignPatterns.State
{
    // a user interface that responds to internal state changes
    [RequireComponent(typeof(PlayerController))]
    public class PlayerStateView : MonoBehaviour
    {
        [SerializeField] private Text labelText;

        private PlayerController player;
        private StateMachine playerStateMachine;

        // mesh to changecolor
        private MeshRenderer meshRenderer;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
            meshRenderer = GetComponent<MeshRenderer>();

            // cache to save typing
            playerStateMachine = player.PlayerStateMachine;

            // listen for any state changes
            playerStateMachine.stateChanged += OnStateChanged;
        }

        void OnDestroy()
        {
            // unregister the subscription if we destroy the object
            playerStateMachine.stateChanged -= OnStateChanged;
        }

        // change the UI.Text when the state changes
        private void OnStateChanged(IState state)
        {
            if (labelText != null)
            {
                labelText.text = state.GetType().Name;
                labelText.color = state.MeshColor;
            }

            ChangeMeshColor(state);
        }

        // set mesh material to the current state's associated color
        private void ChangeMeshColor(IState state)
        {
            if (meshRenderer == null)
            {
                return;
            }

            meshRenderer.sharedMaterial.color = state.MeshColor;
        }
    }
}
