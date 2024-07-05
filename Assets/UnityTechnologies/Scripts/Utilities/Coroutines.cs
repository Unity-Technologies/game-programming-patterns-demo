using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.Utilities
{
    /// <summary>
    ///  The Coroutines class provides static methods for managing coroutines
    ///  without the need for a MonoBehaviour component attached to a game object.
    ///  This is useful if you have a ScriptableObject that needs to run a coroutine.
    /// </summary>
    public static class Coroutines
    {
        private static MonoBehaviour s_CoroutineRunner;

        public static bool IsInitialized => s_CoroutineRunner != null;

        public static void Initialize(MonoBehaviour runner)
        {
            s_CoroutineRunner = runner;
        }

        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            if (s_CoroutineRunner == null)
            {
                throw new InvalidOperationException("CoroutineRunner is not initialized.");
            }

            return s_CoroutineRunner.StartCoroutine(coroutine);
        }

        public static void StopCoroutine(Coroutine coroutine)
        {
            if (s_CoroutineRunner != null)
            {
                s_CoroutineRunner.StopCoroutine(coroutine);
            }
        }

        public static void StopCoroutine(ref Coroutine coroutine)
        {
            if (s_CoroutineRunner != null && coroutine != null)
            {
                s_CoroutineRunner.StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}
