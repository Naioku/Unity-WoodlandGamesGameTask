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

        public void MoveToPosition(Vector3 position)
        {
            _navMeshAgent.destination = position;
        }

        public bool CanMoveToPosition()
        {
            return _navMeshAgent.path.status == NavMeshPathStatus.PathComplete;
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }

        public void ResumeMovement()
        {
            _navMeshAgent.isStopped = false;
        }
    }
}
