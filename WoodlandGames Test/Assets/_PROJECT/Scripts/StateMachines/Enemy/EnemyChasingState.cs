using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly List<Transform> _detectedTargets;

        public EnemyChasingState(EnemyStateMachine stateMachine, List<Transform> detectedTargets) : base(stateMachine)
        {
            _detectedTargets = detectedTargets;
        }
        
        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            Transform closestTarget = GetClosestTarget();
            if (closestTarget == null)
            {
                Debug.Log("Switching to SuspicionState...");
                // StateMachine.SwitchState(new EnemySuspicionState(StateMachine));
                StateMachine.SwitchState(new EnemyPatrollingState(StateMachine));
                return;
            }

            if (!StateMachine.EnemyMover.ChaseToPosition(closestTarget.position))
            {
                Debug.Log("Switching to SuspicionState...");
                // StateMachine.SwitchState(new EnemySuspicionState(StateMachine));
                StateMachine.SwitchState(new EnemyPatrollingState(StateMachine));
            }
        }

        public override void Exit()
        {
        }

        private Transform GetClosestTarget()
        {
            Vector3 enemyPosition = StateMachine.transform.position;
            Transform closestTarget = null;
            float distanceToClosestTargetSquared = Mathf.Infinity;
            foreach (Transform detectedTarget in _detectedTargets)
            {
                float distanceToTargetSquared = Vector3.SqrMagnitude(detectedTarget.position - enemyPosition);
                if (distanceToTargetSquared < distanceToClosestTargetSquared)
                {
                    distanceToClosestTargetSquared = distanceToTargetSquared;
                    closestTarget = detectedTarget;
                }
            }

            return closestTarget;
        }
    }
}
