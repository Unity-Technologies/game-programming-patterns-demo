using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace DesignPatterns.Singleton
{
    [RequireComponent(typeof(Text))]
    public class EnableTextOnStart: MonoBehaviour
    {
        void Start()
        {
            Text text = GetComponent<Text>();
            text.enabled = true;
        }
    }
}
