using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemySuspicionState : EnemyBaseState
    {
        private readonly Vector3 _lastSeenTargetPosition;
        private float _suspicionTimer;
        private bool _canMoveToDestination;

        public EnemySuspicionState(EnemyStateMachine stateMachine, Vector3 lastSeenTargetPosition) : base(stateMachine)
        {
            _lastSeenTargetPosition = lastSeenTargetPosition;
        }
        
        public override void Enter()
        {
            StateMachine.AISensor.TargetDetectedEvent += OnTargetDetected;
            _suspicionTimer = StateMachine.SuspicionTime;
            _canMoveToDestination = StateMachine.EnemyMover.ChaseToPosition(_lastSeenTargetPosition);
        }

        public override void Tick(float deltaTime)
        {
            if (_canMoveToDestination && !IsDestinationReached()) return;
            
            _suspicionTimer -= Time.deltaTime;

            if (_suspicionTimer <= 0f)
            {
                StateMachine.SwitchState(new EnemyPatrollingState(StateMachine));
            }
        }

        public override void Exit()
        {
            StateMachine.AISensor.TargetDetectedEvent -= OnTargetDetected;
        }
        
        private bool IsDestinationReached()
        {
            float distanceToWaypointSquared = Vector3.SqrMagnitude(_lastSeenTargetPosition - StateMachine.transform.position);
            return distanceToWaypointSquared <= Mathf.Pow(StateMachine.ChasingWaypointsTolerance, 2);
        }
    }
}
