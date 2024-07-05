using System;
using UnityEngine;
using UnityEngine.UIElements;
using DesignPatterns.Singleton;
using System.Collections;

namespace DesignPatterns
{
    /// <summary>
    /// Manages screen fading effects using a UI Toolkit VisualElement.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public class ScreenFader : MonoBehaviour
    {
        [Tooltip("Time in seconds to fade")]
        [SerializeField] private float m_FadeDuration = 0.4f;

        [Tooltip("RGB values (0 = black, 1 = white)")]
        [SerializeField] [Range(0, 1)] private float m_GrayValue = 0.4f;

        private VisualElement m_FaderElement;
        
        public float FadeDuration => m_FadeDuration;

        protected void Awake()
        {
            Initialize();
        }

        // Create a UIDocument and VisualElement
        private void Initialize()
        {
            UIDocument uiDocument = GetComponent<UIDocument>();
            
            // Create the VisualElement for the fader
            m_FaderElement = new VisualElement
            {
                style =
                {
                    position = Position.Absolute,
                    top = 0,
                    left = 0,
                    right = 0,
                    bottom = 0,
                    backgroundColor = new Color(0, 0, 0, 0),
                    opacity = 1
                },
                pickingMode = PickingMode.Ignore
            };

            // Add the VisualElement to the UIDocument's root
            uiDocument.rootVisualElement.Add(m_FaderElement);
        }

        public void FadeOut()
        {
            StartCoroutine(Fade(0, 1));
        }

        public void FadeIn()
        {
            StartCoroutine(Fade(1, 0));
        }

        // Interpolate the VisualElement color alpha 
        private IEnumerator Fade(float startOpacity, float endOpacity)
        {
            float elapsedTime = 0;
            
            Color startColor = new Color( m_GrayValue, m_GrayValue, m_GrayValue, startOpacity);
            Color endColor = new Color( m_GrayValue, m_GrayValue, m_GrayValue, endOpacity);

            while (elapsedTime < FadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / FadeDuration;
                
                // Non-linear interpolation for effect
                t = Mathf.SmoothStep(0.0f, 1.0f, t);
                
                m_FaderElement.style.backgroundColor = new StyleColor(Color.Lerp(startColor, endColor, t));
                yield return null;
            }

            m_FaderElement.style.backgroundColor = new StyleColor(endColor);
        }
        
    }
}