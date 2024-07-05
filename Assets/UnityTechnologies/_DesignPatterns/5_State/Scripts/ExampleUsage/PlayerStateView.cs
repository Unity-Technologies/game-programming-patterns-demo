using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace DesignPatterns.StatePattern
{
    /// <summary>
    /// A user interface that responds to internal state changes
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerStateView : MonoBehaviour
    {
        [SerializeField] private Text m_LabelText;

        private PlayerController m_Player;
        private SimplePlayerStateMachine m_PlayerStateMachine;

        // mesh to change color
        private MeshRenderer m_MeshRenderer;

        private void Awake()
        {
            m_Player = GetComponent<PlayerController>();
            m_MeshRenderer = GetComponent<MeshRenderer>();

            // cache to save typing
            m_PlayerStateMachine = m_Player.PlayerStateMachine;

            // listen for any state changes
            m_PlayerStateMachine.stateChanged += OnStateChanged;
        }

        void OnDestroy()
        {
            // unregister the subscription if we destroy the object
            m_PlayerStateMachine.stateChanged -= OnStateChanged;
        }

        // change the UI.Text when the state changes
        private void OnStateChanged(IState state)
        {
            if (m_LabelText != null)
            {
                m_LabelText.text = state.GetType().Name;
                m_LabelText.color = state.MeshColor;
            }

            ChangeMeshColor(state);
        }

        // set mesh material to the current state's associated color
        private void ChangeMeshColor(IState state)
        {
            if (m_MeshRenderer == null)
            {
                return;
            }

            // meshRenderer.sharedMaterial.color = state.MeshColor;
            m_MeshRenderer.material.SetColor("_BaseColor", state.MeshColor);
        }
    }
}
