using UnityEngine;

namespace DesignPatterns.Strategy
{
    [CreateAssetMenu(fileName = "FirstAidAbility", menuName = "Abilities/FirstAid")]
    public class FirstAidAbility : Ability
    {
        // Each Strategy can use custom logic. They can be unique and varied; 
        // execute any intended gameplay logic in the overridden Use method
        public override void Use(GameObject gameObject)
        {
            base.Use(gameObject);
            
            // Implement First Aid logic here

        }
    }
}
