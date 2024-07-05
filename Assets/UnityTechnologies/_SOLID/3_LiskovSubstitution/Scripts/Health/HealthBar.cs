using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DesignPatterns.LSP
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health m_Health;
        [SerializeField] private Slider m_HealthSlider;
        [SerializeField] private Text m_HealthLabel;

        // Duration over which the lerp takes place
        [SerializeField] private float m_LerpDuration = 0.5f;

        private Coroutine m_LerpCoroutine;
        
        private void OnEnable()
        {
            m_Health.HealthChanged.AddListener(UpdateHealthBar);

            if (m_HealthSlider == null)
                m_HealthSlider = GetComponent<Slider>();
        }
        private void OnDisable()
        {
            m_Health.HealthChanged.RemoveListener(UpdateHealthBar);
        }

        private void Start()
        {
            UpdateHealthBar(m_Health.CurrentHealth/m_Health.MaxHealth);
        }

        private void UpdateHealthBar(float healthValue)
        {
            if (m_LerpCoroutine != null)
            {
                StopCoroutine(m_LerpCoroutine);
            }
            m_LerpCoroutine = StartCoroutine(LerpHealthBar(healthValue));

            m_HealthSlider.value = healthValue;

            if (m_HealthLabel != null)
            {
                m_HealthLabel.text = Mathf.RoundToInt(healthValue*100f).ToString();
            }
        }

        // Add linear interpolation for simple animation
        private IEnumerator LerpHealthBar(float targetValue)
        {
            float elapsedTime = 0;
            float startValue = m_HealthSlider.value;
            
            while (elapsedTime < m_LerpDuration)
            {
                m_HealthSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime / m_LerpDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            // Set to the target value when done
            m_HealthSlider.value = targetValue;
        }
    }
}
