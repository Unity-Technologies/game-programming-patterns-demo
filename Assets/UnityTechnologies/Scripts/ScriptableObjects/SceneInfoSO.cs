using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace DesignPatterns
{
    [CreateAssetMenu(fileName = "SceneInfo_Data", menuName = "DesignPatterns/SceneInfoData", order = 1)]
    public class SceneInfoSO : DescriptionSO
    {
        [SerializeField] string m_ScenePath;

        public string ScenePath => m_ScenePath;

    }
}
