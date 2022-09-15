using System;
using UnityEngine;
using UnityEngine.AI;

namespace _PROJECT.Scripts.Locomotion
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float chasingSpeed = 5f;
        [SerializeField] private float defaultSpeed = 3.5f;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            _navMeshAgent.speed = defaultSpeed;
        }

        public bool MoveToPosition(Vector3 position)
        {
            return MoveToPositionWithSpeed(position, defaultSpeed);
        }

        public bool ChaseToPosition(Vector3 position)
        {
            return MoveToPositionWithSpeed(position, chasingSpeed);
        }

        public bool CanMoveToPosition(Vector3 position)
        {
            NavMeshPath navMeshPath = new NavMeshPath();
            _navMeshAgent.CalculatePath(position, navMeshPath);
            
            return navMeshPath.status == NavMeshPathStatus.PathComplete;
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }

        public void ResumeMovement()
        {
            _navMeshAgent.isStopped = false;
        }

        private bool MoveToPositionWithSpeed(Vector3 position, float speed)
        {
            if (!CanMoveToPosition(position))
            {
                _navMeshAgent.isStopped = true;
                return false;
            }

            _navMeshAgent.destination = position;
            _navMeshAgent.speed = speed;
            _navMeshAgent.isStopped = false;
            return true;
        }
    }
}
