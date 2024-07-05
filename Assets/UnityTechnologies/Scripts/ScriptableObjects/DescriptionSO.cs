using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DesignPatterns.Utilities;

namespace DesignPatterns
{
    /// <summary>
    /// This is a base ScriptableObject that adds a description field. The custom Optional Attribute bypasses the 
    /// NullRefChecker's Validate method.
    /// </summary>
    public class DescriptionSO : ScriptableObject
    {
        [TextArea(3, 20)]
        [SerializeField][Optional] protected string m_Description;
    }
}

