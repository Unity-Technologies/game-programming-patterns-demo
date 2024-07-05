using UnityEngine;

namespace DesignPatterns.DIP
{
    /// <summary>
    /// A Switch component that can toggle the state of an ISwitchable client. This class demonstrates
    /// the Dependency Inversion Principle by depending on an abstraction (ISwitchable) rather than concrete implementations.
    /// </summary>
    public class Switch : MonoBehaviour
    {
        // Unity's serialization system does not directly support interfaces. Work around this limitation
        // by using a serialized reference to a MonoBehaviour that implements ISwitchable.
        
        [SerializeField] private MonoBehaviour m_ClientBehaviour;
        private ISwitchable m_Client => m_ClientBehaviour as ISwitchable;
        

        // Toggles the active state of the associated ISwitchable client.
        public void Toggle()
        {
            if (m_Client == null) 
                return;

            if (m_Client.IsActive)
            {
                m_Client.Deactivate();
            }
            else
            {
                m_Client.Activate();
            }
        }
    }
}
