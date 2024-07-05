using UnityEngine;

namespace DesignPatterns.Events
{
    /// <summary>
    /// This ScriptableObject delegate wraps around the static UIEvents.SolidDemoViewShown event message for 
    /// easier serialization.
    /// </summary>
    [CreateAssetMenu(fileName = "ResourcesViewShownSO", menuName = "DesignPatterns/ResourcesViewShownSO")]
    public class ResourcesViewShownSO : BaseEventSO
    {
        public override void OnEventRaised()
        {
            base.OnEventRaised();

            UIEvents.ResourcesMenuShown?.Invoke();

            if (m_DebugLog)
            {
                Debug.Log("[ResourcesViewShownSO] OnEventRaised: View Shown");
            }
        }
    }
}