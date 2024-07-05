using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Events;

namespace DesignPatterns
{
    /// <summary>
    /// This ScriptableObject delegate wraps around the static UIEvents.HomeViewShown event message for 
    /// easier serialization.
    /// </summary>
	[CreateAssetMenu(fileName = "HomeViewShownSO", menuName = "DesignPatterns/HomeViewShownSO")]
	public class HomeViewShownSO : BaseEventSO
	{
        public override void OnEventRaised()
        {
            base.OnEventRaised();

            UIEvents.MainMenuShown?.Invoke();
            Debug.Log("Home Screen Shown (Main Menu)");
        }
	}
}