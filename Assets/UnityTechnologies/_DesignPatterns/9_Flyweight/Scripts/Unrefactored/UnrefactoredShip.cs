using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesignPatterns.Flyweight
{
    public class UnrefactoredShip : MonoBehaviour
    {
        [Tooltip("Shared string data across all instances of the ships")]
        public string UnitName;

        [Space] 
        [TextArea(5, 10)] public string Description;
        public float Speed;
        public int AttackPower;
        public int Defense;
        
        [Space] 
        public Texture2D IconA;
        public Texture2D IconB;
        public Texture2D IconC;

        [Space] 
        [Tooltip("This extrinsic health data is not shared with other instances of the ships")]
        public float Health;
    }
}