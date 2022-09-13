using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyPatrollingState : EnemyBaseState
    {
        public EnemyPatrollingState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
        }

        public override void Tick(float deltaTime)
        {
            StateMachine.PatrollingBehavior.Patrol();
        }

        public override void Exit()
        {
        }
    }
}
