using UnityEngine;

namespace DesignPatterns.Flyweight
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "ShipData", menuName = "Flyweight/ShipData", order = 1)]
    public class ShipData : ScriptableObject
    {
        [Header("Shared Data")]
        [Tooltip("Shared string data across all instances of the ships")]
        public string UnitName;

        [TextArea(5, 10)]
        public string Description;

        [Tooltip("This intrinsic speed data is immutable across all instances of the ships")]
        public float Speed;

        [Tooltip("This intrinsic attack power data is immutable across all instances of the ships")]
        public int AttackPower;

        [Tooltip("This intrinsic defense data is immutable across all instances of the ships")]
        public int Defense;
        
        public Texture2D IconA;
        public Texture2D IconB;
        public Texture2D IconC;
    }
}
