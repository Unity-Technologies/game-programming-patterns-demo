using DesignPatterns.StateMachines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns.Utilities;


namespace DesignPatterns
{
    public class UnloadLastSceneState : AbstractState
    {
        readonly SceneLoader m_SceneLoader;

        public UnloadLastSceneState(SceneLoader sceneLoader)
        {
            m_SceneLoader = sceneLoader;
        }

        public override IEnumerator Execute()
        {
            yield return m_SceneLoader.UnloadLastScene();
        }
    }
}
