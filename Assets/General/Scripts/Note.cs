using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns
{
    public class Note : MonoBehaviour
    {
        [TextArea(10,30)]
        public string note;

    }
}
