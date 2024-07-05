using DesignPatterns.Events;

namespace DesignPatterns.StateMachines
{
    public class SceneEventSOLink : EventSOLink
    {
        // Next state
        private LoadSceneState m_LoadSceneState;

        public SceneEventSOLink(SceneEventSO sceneEventSO, LoadSceneState loadSceneState)
            : base(sceneEventSO, loadSceneState)
        {
            m_LoadSceneState = loadSceneState;
        }

        public override void GameEvent_EventRaised()
        {
            base.GameEvent_EventRaised();

            // Check if the base event is a SceneEventSO and set the ScenePath
            if (m_GameEvent is SceneEventSO sceneEventSO)
            {
                m_LoadSceneState.ScenePath = sceneEventSO.ScenePath;
            }
        }
    }
}