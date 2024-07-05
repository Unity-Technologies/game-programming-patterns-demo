using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


namespace DesignPatterns.Utilities
{
    // Use this to issue a LogError to the user when required Fields are not set in the Inspector.
    // This works only for SerializedFields and ignores any fields marked with the Optional attribute.

    public static class NullRefChecker
    {
        // Note: instance is not always a MonoBehaviour

        public static void Validate(object instance)
        {
            // Cache all non-static fields both public and private
            FieldInfo[] fields = instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (FieldInfo field in fields)
            {
                // If non-serialized or optional, do nothing
                if (!field.IsDefined(typeof(SerializeField), true) || field.IsDefined(typeof(OptionalAttribute), true))
                {
                    continue;
                }

                // If null, log an error
                if (field.GetValue(instance) == null)
                {
                    // Check if instance is a MonoBehaviour...
                    if (instance is MonoBehaviour monoBehaviour)
                    {
                        GameObject gameObject = monoBehaviour.gameObject;

                        Debug.LogError($"Missing assignment for field: {field.Name} in component: {instance.GetType().Name} on GameObject: " +
                            $"{monoBehaviour.gameObject}", monoBehaviour.gameObject);
                    }
                    // ... or a ScriptableObect
                    else if (instance is ScriptableObject scriptableObject)
                    {
                        Debug.LogError($"Missing assignment for field: {field.Name} on ScriptableObject:  " +
                            $"{scriptableObject.name} ({instance.GetType().Name})");
                    }
                    else
                    {
                        Debug.LogError($"Missing assignment for field: {field.Name} in object: {instance.GetType().Name}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// A custom PropertyAttribute to bypass the above Validate check.
    /// </summary>
    public class OptionalAttribute : PropertyAttribute
    {
    }

    // Mark a field with the Optional attribute if it's not required
    // public class MyClass : MonoBehaviour
    // {
    //    public string requiredField;

    //    [Optional]
    //    public string optionalField;
    // }
    //
    // This allows the Validate to ignore the field.


}
