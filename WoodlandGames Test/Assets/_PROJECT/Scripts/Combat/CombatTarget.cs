using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        private PlayerLifes _playerLifes;

        private void Awake()
        {
            _playerLifes = FindObjectOfType<PlayerLifes>();
        }

        internal void CatchTarget()
        {
            _playerLifes.DropLife();
        }
    }
}
