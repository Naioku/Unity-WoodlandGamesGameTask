using UnityEngine;

namespace _PROJECT.Scripts.Core
{
    public class VariablesManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] stageVariables;

        private StageData _stageData;

        void Awake()
        {
            _stageData = FindObjectOfType<StageData>();
            SpawnVariables();
        }

        private void SpawnVariables()
        {
            if (_stageData.StageLevel > stageVariables.Length) return;
            
            Instantiate(stageVariables[_stageData.StageLevel - 1], transform);
        }
    }
}
