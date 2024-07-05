using UnityEngine;

namespace DesignPatterns.ISP
{
 public interface ITarget
    {
        void TakeDamage(int amount);
        void Explode();
        void TriggerEffect();
    }
    
 /// <summary>
 /// This class implements the ITarget interface, which includes methods for taking damage, exploding, and triggering effects.
 ///
 /// Even if a simple target might only need to take damage, it is forced to implement all methods defined in the ITarget
 /// interface. This leads to empty method implementations.
 /// </summary>
    public class UnrefactoredTarget : MonoBehaviour, ITarget
    {
        // Even if this target only needs to take damage, it must implement all methods.
        public void TakeDamage(int amount)
        {
            // Implement damage logic.
        }

        public void Explode()
        {
            // Even if this target does not need to explode, this method must be implemented.
        }

        public void TriggerEffect()
        {
            // Similarly, this requires an implementation, even if it's unnecessary.
        }
    }
}

