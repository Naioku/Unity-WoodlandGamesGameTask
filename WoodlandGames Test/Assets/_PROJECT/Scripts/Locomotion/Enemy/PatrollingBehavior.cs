using UnityEngine;

namespace _PROJECT.Scripts.Locomotion.Enemy
{
    [RequireComponent(typeof(EnemyMover))]
    public class PatrollingBehavior : MonoBehaviour
    {
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float dwellingTime = 2f;

        private EnemyMover _enemyMover;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex;

        private void Awake()
        {
            _enemyMover = GetComponent<EnemyMover>();
        }

        public void Patrol()
        {
            if (AtWaypoint())
            {
                _enemyMover.StopMovement();
                _timeSinceArrivedAtWaypoint = 0f;
                ReloadWaypoint();
            }

            if (ShouldMoveToNextWaypoint())
            {
                if (!_enemyMover.MoveToPosition(GetCurrentWaypointPosition()))
                {
                    ReloadWaypoint();
                }
            }
            
            UpdateTimer();
        }

        /// <summary>
        /// Vector3.SqrMagnitude() is used for better performance, because Patrol() is called on every frame (look at
        /// the EnemyPatrollingState and the StateMachine, where Tick() is performed on Update()), so the same is with
        /// AtWaypoint().
        /// </summary>
        /// <returns></returns>
        private bool AtWaypoint()
        {
            float distanceToWaypointSquared = Vector3.SqrMagnitude(transform.position - GetCurrentWaypointPosition());
            return distanceToWaypointSquared <= waypointTolerance * waypointTolerance;
        }

        private void ReloadWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }

        private bool ShouldMoveToNextWaypoint()
        {
            return _timeSinceArrivedAtWaypoint > dwellingTime;
        }

        private Vector3 GetCurrentWaypointPosition()
        {
            return patrolPath.GetWaypointPosition(_currentWaypointIndex);
        }

        private void UpdateTimer()
        {
            _timeSinceArrivedAtWaypoint += Time.deltaTime;
        }
    }
}
