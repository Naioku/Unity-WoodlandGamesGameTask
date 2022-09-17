using _PROJECT.Scripts.Core;
using UnityEngine;

namespace _PROJECT.Scripts.Combat
{
    public class CombatTarget : MonoBehaviour
    {
        private StageData _stageData;

        private void Awake()
        {
            _stageData = FindObjectOfType<StageData>();
        }

        internal void CatchTarget()
        {
            _stageData.DropLife();
        }
    }
}
