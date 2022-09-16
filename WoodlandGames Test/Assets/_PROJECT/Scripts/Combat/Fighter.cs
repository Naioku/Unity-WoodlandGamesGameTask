using UnityEngine;

namespace _PROJECT.Scripts.Combat
{
    public class Fighter : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out CombatTarget combatTarget)) return;

            combatTarget.CatchTarget();
        }
    }
}
