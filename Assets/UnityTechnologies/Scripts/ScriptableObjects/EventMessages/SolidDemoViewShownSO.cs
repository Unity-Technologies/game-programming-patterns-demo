using UnityEngine;

namespace DesignPatterns.Events
{
    /// <summary>
    /// This ScriptableObject delegate wraps around the static UIEvents.SolidDemoViewShown event message for 
    /// easier serialization.
    /// </summary>
    [CreateAssetMenu(fileName = "SolidDemoViewShownSO", menuName = "DesignPatterns/SolidDemoViewShownSO")]
    public class SolidDemoViewShownSO : BaseEventSO
    {
        public override void OnEventRaised()
        {
            base.OnEventRaised();

            UIEvents.SolidMenuShown?.Invoke();

            if (m_DebugLog)
            {
                Debug.Log("[SolidDemoViewShownSO] RaiseEvent: View Shown");
            }
        }
    }
}