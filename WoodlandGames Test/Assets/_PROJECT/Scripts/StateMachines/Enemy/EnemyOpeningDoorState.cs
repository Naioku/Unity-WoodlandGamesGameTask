namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyOpeningDoorState : EnemyBaseState
    {
        public EnemyOpeningDoorState(EnemyStateMachine stateMachine) : base(stateMachine) {}
        
        public override void Enter()
        {
            StateMachine.EnemyMover.StopMovement();

            StateMachine.EnemyDoorDetector.DoorOpenedEvent += OnDoorOpened;
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void Exit()
        {
            StateMachine.EnemyDoorDetector.DoorOpenedEvent -= OnDoorOpened;
        }

        private void OnDoorOpened()
        {
            StateMachine.EnemyMover.ResumeMovement();
            StateMachine.SwitchState(new EnemyPatrollingState(StateMachine));
        }
    }
}
