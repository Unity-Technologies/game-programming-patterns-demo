using UnityEngine;

namespace DesignPatterns
{
    /// <summary>
    /// 
    /// </summary>
    public class ToggleFlyweight : MonoBehaviour
    {
        [Tooltip("The topmost GameObject demonstrating the Flyweight pattern")]
        [SerializeField] private GameObject m_Flyweight;
        [Tooltip("The topmost GameObject before implementing the Flyweight pattern")]
        [SerializeField] private GameObject m_NonFlyweight;

        // Toggles the GameObjects on/off
        public void Toggle()
        {
            // Check if m_Flyweight is currently active.
            bool isFlyweightActive = m_Flyweight.activeSelf;

            // Toggle the active state of both GameObjects.
            SetActive(!isFlyweightActive);
        }
        
        public void SetActive(bool state)
        {
            if (m_NonFlyweight != null)
                m_NonFlyweight.SetActive(!state);
            
            if (m_Flyweight != null)
                m_Flyweight.SetActive(state);
        }
    }
}
