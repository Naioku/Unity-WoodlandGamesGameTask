using System.Collections.Generic;
using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public abstract class EnemyBaseState : State
    {
        protected readonly EnemyStateMachine StateMachine;

        protected EnemyBaseState(EnemyStateMachine stateMachine)
        {
            StateMachine = stateMachine;
        }
        
        protected void OnTargetDetected(List<Transform> detectedTargets)
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
