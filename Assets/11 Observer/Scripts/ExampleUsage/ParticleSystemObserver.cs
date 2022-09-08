using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{

    public class ParticleSystemObserver : MonoBehaviour
    {
        [SerializeField] ButtonSubject subjectToObserve;
        [SerializeField] ParticleSystem particleSystem;

        private void Awake()
        {
            if (subjectToObserve != null)
            {
                subjectToObserve.Clicked += OnThingHappened;
            }
        }

        private void OnThingHappened()
        {
            if (particleSystem != null)
            {
                particleSystem.Stop();
                particleSystem.Play();
            }
        }

        private void OnDestroy()
        {
            // unsubscribe/deregister from the event if we destroy the object
            if (subjectToObserve != null)
            {
                subjectToObserve.Clicked -= OnThingHappened;
            }
        }

    }
}
