using _PROJECT.Scripts.Locomotion;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        public PatrollingBehavior PatrollingBehavior { get; private set; }

        private void Awake()
        {
            PatrollingBehavior = GetComponent<PatrollingBehavior>();
        }

        private void Start()
        {
            SwitchState(new EnemyPatrollingState(this));
        }
    }
}
