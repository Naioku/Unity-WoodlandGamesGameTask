using UnityEngine;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyTestState : EnemyBaseState
    {
        private float _timer = 5f;
        
        public EnemyTestState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            Debug.Log("Entering the EnemyTestState.");
        }

        public override void Tick(float deltaTime)
        {
            Debug.Log("Ticking the EnemyTestState. Timer: " + _timer);
            _timer -= deltaTime;

            if (_timer <= 0f)
            {
                StateMachine.SwitchState(new EnemyTestState(StateMachine));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exiting the EnemyTestState.");
        }
    }
}
