using UnityEngine;

namespace _PROJECT.Scripts.Locomotion
{
    [RequireComponent(typeof(EnemyMover))]
    public class PatrollingBehavior : MonoBehaviour
    {
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointTolerance = 1f;
        [SerializeField] private float dwellingTime = 2f;

        private EnemyMover _enemyMover;
        private Vector3 _guardPosition;
        private float _timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int _currentWaypointIndex;

        private void Awake()
        {
            _enemyMover = GetComponent<EnemyMover>();
        }

        private void Start()
        {
            _guardPosition = transform.position;
        }

        public void Patrol()
        {
            Vector3 nextPosition = _guardPosition;

            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceArrivedAtWaypoint = 0f;
                    ReloadWaypoint();
                }

                nextPosition = GetCurrentWaypointPosition();
            }

            if (_timeSinceArrivedAtWaypoint > dwellingTime)
            {
                _enemyMover.MoveToPosition(nextPosition);
                
                if (WaypointIsOutOfReach())
                {
                    ReloadWaypoint();
                }
            }
            
            UpdateTimer();
        }

        private bool WaypointIsOutOfReach()
        {
            return !_enemyMover.CanMoveToPosition();
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypointPosition());
            return distanceToWaypoint <= waypointTolerance;
        }

        private void ReloadWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
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
