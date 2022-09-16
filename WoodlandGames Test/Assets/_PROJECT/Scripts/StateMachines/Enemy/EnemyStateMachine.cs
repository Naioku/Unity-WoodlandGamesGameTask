using _PROJECT.Scripts.Combat;
using _PROJECT.Scripts.Locomotion.Enemy;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        public PatrollingBehavior PatrollingBehavior { get; private set; }
        public EnemyMover EnemyMover { get; private set; }

        [field: SerializeField] public EnemyDoorDetector EnemyDoorDetector { get; private set; }
        [field: SerializeField] public AISensor AISensor { get; private set; }

        [field: Header("Guarding state")]
        [field: SerializeField] public float GuardingDisplacementTolerance { get; private set; } = 1f;
        
        [field: Header("Suspicion state")]
        [field: SerializeField] public float ChasingWaypointsTolerance { get; private set; } = 1f;
        [field: SerializeField] public float SuspicionTime { get; private set; } = 2f;

        private Vector3 _guardingPosition;
            
        private void Awake()
        {
            PatrollingBehavior = GetComponent<PatrollingBehavior>();
            EnemyMover = GetComponent<EnemyMover>();
            _guardingPosition = transform.position;
        }

        private void Start()
        {
            SwitchToDefaultState();
        }

        private void OnEnable()
        {
            EnemyDoorDetector.OpenDoorEvent += OnDoorOpening;
        }

        private void OnDisable()
        {
            EnemyDoorDetector.OpenDoorEvent -= OnDoorOpening;
        }

        public void SwitchToDefaultState()
        {
            if (PatrollingBehavior == null)
            {
                SwitchState(new EnemyGuardingState(this, _guardingPosition));
            }
            else
            {
                SwitchState(new EnemyPatrollingState(this));
            }
        }
        
        private void OnDoorOpening()
        {
            SwitchState(new EnemyOpeningDoorState(this));
        }
    }
}
