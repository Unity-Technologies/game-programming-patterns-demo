using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioObserver : MonoBehaviour
    {
        // dependency to observe
        [SerializeField] ButtonSubject subjectToObserve;
        [SerializeField] float delay = 0f;
        private AudioSource source;

        private void Awake()
        {
            source = GetComponent<AudioSource>();

            if (subjectToObserve != null)
            {
                subjectToObserve.Clicked += OnThingHappened;
            }
        }

        public void OnThingHappened()
        {
            StartCoroutine(PlayWithDelay());
        }

        IEnumerator PlayWithDelay()
        {
            yield return new WaitForSeconds(delay);
            source.Stop();
            source.Play();
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
