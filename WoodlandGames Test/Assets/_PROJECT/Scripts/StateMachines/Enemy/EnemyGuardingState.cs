using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyGuardingState : EnemyBaseState
    {
        private Vector3 _guardingPosition;

        public EnemyGuardingState(EnemyStateMachine stateMachine, Vector3 guardingPosition) : base(stateMachine)
        {
            _guardingPosition = guardingPosition;
        }
        
        public override void Enter()
        {
            StateMachine.AISensor.TargetDetectedEvent += OnTargetDetected;
        }

        public override void Tick(float deltaTime)
        {
            if (IsDestinationReached(_guardingPosition, StateMachine.GuardingDisplacementTolerance)) return;
            
            if (!StateMachine.EnemyMover.MoveToPosition(_guardingPosition))
            {
                _guardingPosition = StateMachine.transform.position;
            }
        }

        public override void Exit()
        {
            StateMachine.AISensor.TargetDetectedEvent -= OnTargetDetected;
        }
    }
}
