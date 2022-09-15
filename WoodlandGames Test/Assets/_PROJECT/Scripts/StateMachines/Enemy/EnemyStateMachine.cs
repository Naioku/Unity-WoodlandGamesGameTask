using _PROJECT.Scripts.Combat;
using _PROJECT.Scripts.Locomotion;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        public PatrollingBehavior PatrollingBehavior { get; private set; }
        public EnemyMover EnemyMover { get; private set; }

        [field: SerializeField] public EnemyDoorDetector EnemyDoorDetector { get; private set; }
        [field: SerializeField] public AISensor AISensor { get; private set; }

        private void Awake()
        {
            PatrollingBehavior = GetComponent<PatrollingBehavior>();
            EnemyMover = GetComponent<EnemyMover>();
        }

        private void Start()
        {
            SwitchState(new EnemyPatrollingState(this));
        }

        private void OnEnable()
        {
            EnemyDoorDetector.OpenDoorEvent += OnDoorOpening;
        }
        
        private void OnDisable()
        {
            EnemyDoorDetector.OpenDoorEvent -= OnDoorOpening;
        }

        private void OnDoorOpening()
        {
            SwitchState(new EnemyOpeningDoorState(this));
        }
    }
}
