using _PROJECT.Scripts.Locomotion;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        public PatrollingBehavior PatrollingBehavior { get; private set; }

        [SerializeField] private EnemyDoorDetector enemyDoorDetector;

        private void Awake()
        {
            PatrollingBehavior = GetComponent<PatrollingBehavior>();
        }

        private void Start()
        {
            SwitchState(new EnemyPatrollingState(this));
        }

        private void OnEnable()
        {
            enemyDoorDetector.OpenDoorEvent += OnDoorOpening;
        }
        
        private void OnDisable()
        {
            enemyDoorDetector.OpenDoorEvent -= OnDoorOpening;
        }

        private void OnDoorOpening()
        {
            print("OnDoorOpening");
        }
    }
}
