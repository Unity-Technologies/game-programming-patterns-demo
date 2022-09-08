using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{
    public class ExampleObserver : MonoBehaviour
    {
        // reference to the subject that we are observing/listening to
        [SerializeField] Subject subjectToObserve;

        // event handling method: the function signature must match the subject's event
        private void OnThingHappened()
        {
            // any logic that responds to event goes here
        }

        private void Awake()
        {
            // subscribe/register to the subject's event 
            if (subjectToObserve != null)
            {
                subjectToObserve.ThingHappened += OnThingHappened;
            }
        }

        private void OnDestroy()
        {
            // unsubscribe/unregister if we destroy the object
            if (subjectToObserve != null)
            {
                subjectToObserve.ThingHappened -= OnThingHappened;
            }
        }
    }
}
