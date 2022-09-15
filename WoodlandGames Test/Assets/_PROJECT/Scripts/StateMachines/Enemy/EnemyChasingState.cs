using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly List<Transform> _detectedTargets;
        private Vector3 _lastSeenTargetPosition;

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
            
            if (closestTarget != null)
            {
                Debug.Log("closestTarget != null");

                SaveLastSeenTargetPosition(closestTarget);
                
                if (!StateMachine.EnemyMover.ChaseToPosition(closestTarget.position))
                {
                    Debug.Log("!ChaseToPosition");
                    
                    StateMachine.SwitchState(new EnemySuspicionState(StateMachine, _lastSeenTargetPosition));
                }
            } 
            else
            {
                Debug.Log("closestTarget == null");
                
                StateMachine.SwitchState(new EnemySuspicionState(StateMachine, _lastSeenTargetPosition));
            }
        }

        public override void Exit()
        {
        }

        private void SaveLastSeenTargetPosition(Transform closestTarget)
        {
            _lastSeenTargetPosition = closestTarget.position;
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
