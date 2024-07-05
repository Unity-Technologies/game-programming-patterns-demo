using UnityEngine;
namespace DesignPatterns.Events
{
    /// <summary>
    /// ScriptableObject-based event messages for scene related actions. 
    /// Inherits from BaseEventSO and includes a string field for the scene path.
    /// </summary>
    [CreateAssetMenu(fileName = "SceneEventSO", menuName = "DesignPatterns/SceneEventSO")]
    public class SceneEventSO : BaseEventSO
    {

        // Inspector fields
        [Tooltip("Scene path associated with this event.")]
        [SerializeField]
        private string m_ScenePath;

        // Properties
        public string ScenePath { get => m_ScenePath; set => m_ScenePath = value; }


        public override void OnEventRaised()
        {
            base.OnEventRaised();

            // Notify the event channel
            SceneEvents.SceneLoadedByPath?.Invoke(m_ScenePath);
        }
    }
}
