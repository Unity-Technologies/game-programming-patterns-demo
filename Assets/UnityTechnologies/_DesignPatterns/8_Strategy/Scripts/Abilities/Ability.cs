using UnityEngine;
using DesignPatterns.Singleton;


namespace DesignPatterns.Strategy
{
    public abstract class Ability : ScriptableObject
    {
        [SerializeField] protected string m_AbilityName;

        [Tooltip("Image texture for UI button")]
        [SerializeField] protected Sprite m_ButtonIcon;
        [Header("Visuals")]
        [SerializeField] protected ParticleSystem m_ParticleSystem;
        [SerializeField] protected AudioClip m_AudioClip;
        
        // Each Strategy can use custom logic. Implement the Use method in the subclasses
        public virtual void Use(GameObject gameObject)
        {
            // Use method logs name, plays sound, and particle effect
            Debug.Log($"Using ability: {m_AbilityName}");
            PlaySound();
            PlayParticleFX();
        }

        public Sprite ButtonIcon => m_ButtonIcon;
        
        private void PlayParticleFX()
        {
            if (m_ParticleSystem != null)
            {
                ParticleSystem instance = Instantiate(m_ParticleSystem, Vector3.zero, Quaternion.identity);
                // We ensure that the particle system is first stopped and then played
                instance.Stop();
                instance.Play();
            }
        }

        private void PlaySound()
        {
            if (m_AudioClip)
                AudioManager.Instance.PlaySoundEffect(m_AudioClip);
        }
    }



}