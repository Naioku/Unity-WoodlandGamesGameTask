using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyPatrollingState : EnemyBaseState
    {
        public EnemyPatrollingState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.AISensor.TargetDetectedEvent += OnTargetDetected;
        }

        public override void Tick(float deltaTime)
        {
            StateMachine.PatrollingBehavior.Patrol();
        }

        public override void Exit()
        {
            StateMachine.AISensor.TargetDetectedEvent -= OnTargetDetected;
        }

        private void OnTargetDetected(List<Transform> detectedTargets)
        {
            foreach (Transform target in detectedTargets)
            {
                if (StateMachine.EnemyMover.CanMoveToPosition(target.position))
                {
                    StateMachine.SwitchState(new EnemyChasingState(StateMachine, detectedTargets));
                }
            }
        }
    }
}
