using _PROJECT.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace _PROJECT.Scripts.Locomotion.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        
        [SerializeField] private float chasingSpeed = 2f;
        [SerializeField] private float defaultSpeed = 4f;
        
        private StageData _stageData;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _stageData = FindObjectOfType <StageData>();
            if (_stageData.GetDataValue(DataType.ChasingSpeed, ObjectGroupType.Enemy, out float value))
            {
                chasingSpeed = value;
            }

            if (_stageData.GetDataValue(DataType.DefaultSpeed, ObjectGroupType.Enemy, out value))
            {
                defaultSpeed = value;
            }
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
