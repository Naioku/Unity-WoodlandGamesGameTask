using UnityEngine;

namespace _PROJECT.Scripts.Core
{
    public class VariablesManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] stageVariables;

        private GameSession _gameSession;

        void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
            SpawnVariables();
        }

        private void SpawnVariables()
        {
            if (_gameSession.StageLevel > stageVariables.Length) return;
            
            Instantiate(stageVariables[_gameSession.StageLevel - 1], transform);
        }
    }
}
