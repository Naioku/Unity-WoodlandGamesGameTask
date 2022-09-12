namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        private void Start()
        {
            SwitchState(new EnemyTestState(this));
        }
    }
}
