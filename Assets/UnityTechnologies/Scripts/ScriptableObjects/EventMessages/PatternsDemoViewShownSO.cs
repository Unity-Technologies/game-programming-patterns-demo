using UnityEngine;

namespace DesignPatterns.Events
{
    /// <summary>
    /// This ScriptableObject delegate wraps around the static UIEvents.PatternsDemoViewShown event message for 
    /// easier serialization.
    /// </summary>
    [CreateAssetMenu(fileName = "PatternsDemoViewShownSO", menuName = "DesignPatterns/PatternsDemoViewShownSO")]
	public class PatternsDemoViewShownSO : BaseEventSO
    {
        public override void OnEventRaised()
        {
            base.OnEventRaised();

            UIEvents.DesignPatternsMenuShown?.Invoke();

            if (m_DebugLog)
            {
                Debug.Log("[PatternsDemoViewShownSO] RaiseEvent: Patterns Demo View Shown");
            }
            
        }
    }
}