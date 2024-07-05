using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.Observer
{

    public class ParticleSystemObserver : MonoBehaviour
    {
        [SerializeField] ButtonSubject m_SubjectToObserve;
        [SerializeField] ParticleSystem m_ParticleSystem;

        private void Awake()
        {
            if (m_SubjectToObserve != null)
            {
                m_SubjectToObserve.Clicked += OnThingHappened;
            }
        }

        private void OnThingHappened()
        {
            if (m_ParticleSystem != null)
            {
                m_ParticleSystem.Stop();
                m_ParticleSystem.Play();
            }
        }

        private void OnDestroy()
        {
            // unsubscribe/deregister from the event if we destroy the object
            if (m_SubjectToObserve != null)
            {
                m_SubjectToObserve.Clicked -= OnThingHappened;
            }
        }

    }
}
