using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        private GameSession _gameSession;

        private void Awake()
        {
            _gameSession = FindObjectOfType<GameSession>();
        }

        internal void CatchTarget()
        {
            _gameSession.DropLife();
        }
    }
}
