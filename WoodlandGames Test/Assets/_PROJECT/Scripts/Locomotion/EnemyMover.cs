using UnityEngine;
using UnityEngine.AI;

namespace _PROJECT.Scripts.Locomotion
{
    public class EnemyMover : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveOn(Vector3 position)
        {
            _navMeshAgent.destination = position;
        }
    }
}
