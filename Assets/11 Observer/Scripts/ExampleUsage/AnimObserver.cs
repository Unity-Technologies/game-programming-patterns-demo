using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Observer
{
    public class AnimObserver : MonoBehaviour
    {
        [SerializeField] Animation animClip;
        [SerializeField] ButtonSubject subjectToObserve;
        void Start()
        {
            if (subjectToObserve != null)
            {
                subjectToObserve.Clicked += OnThingHappened;
            }
        }

        private void OnThingHappened()
        {
            if (animClip != null)
            {
                animClip.Stop();
                animClip.Play();
            }
        }
    }
}
