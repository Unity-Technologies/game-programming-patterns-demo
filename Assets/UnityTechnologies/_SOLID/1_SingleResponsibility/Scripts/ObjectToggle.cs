using UnityEngine;

namespace DesignPatterns.SRP
{
    /// <summary>
    /// Demo script to toggle between the Player and its unrefactored logic.
    /// </summary>
    public class ObjectToggle : MonoBehaviour
    {
        [SerializeField] 
        GameObject m_FirstObject;
        [SerializeField] 
        GameObject m_SecondObject;

        [SerializeField] 
        KeyCode toggleKey = KeyCode.T; // The key used to toggle objects. Set to 'T' by default.

        private void Start()
        {
            SyncObjectPositions();
        }

        private void Update()
        {
            // Check if the toggle key is pressed.
            if (Input.GetKeyDown(toggleKey))
            {
                SyncObjectPositions();
                ToggleObjects();
            }
        }

        /// <summary>
        /// Toggle the active state of the Player objects.
        /// </summary>
        public void ToggleObjects()
        {
            // Ensure both objects are assigned to prevent null reference errors.
            if (m_FirstObject == null || m_SecondObject == null)
            {
                Debug.LogWarning("[ObjectToggle] ToggleObjects: One or both objects are unassigned.");
                return;
            }

            m_FirstObject.SetActive(!m_FirstObject.activeSelf);
            m_SecondObject.SetActive(!m_SecondObject.activeSelf);
        }

        /// <summary>
        /// Sync the inactive object to the active object.
        /// </summary>
        public void SyncObjectPositions()
        {
            if (m_FirstObject == null || m_SecondObject == null)
            {
                Debug.LogWarning("[ObjectToggle] SyncObjectPositions: One or both objects are unassigned.");
                return;
            }

            if (m_FirstObject.activeInHierarchy && !m_SecondObject.activeInHierarchy)
            {
                m_SecondObject.transform.position = m_FirstObject.transform.position;
            }
            else if (!m_FirstObject.activeInHierarchy && m_SecondObject.activeInHierarchy)
            {
                m_FirstObject.transform.position = m_SecondObject.transform.position;
            }
        }
    }

}
