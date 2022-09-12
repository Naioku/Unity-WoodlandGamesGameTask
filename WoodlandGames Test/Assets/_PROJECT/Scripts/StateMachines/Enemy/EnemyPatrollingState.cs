using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyPatrollingState : EnemyBaseState
    {
        public EnemyPatrollingState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.EnemyMover.MoveOn(new Vector3(3.59f, 1.56f, -22.73f));
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {
        }
    }
}
