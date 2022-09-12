using _PROJECT.Scripts.Locomotion;

namespace _PROJECT.Scripts.StateMachines.Enemy
{
    public class EnemyStateMachine : StateMachine
    {
        public EnemyMover EnemyMover { get; private set; }

        private void Awake()
        {
            EnemyMover = GetComponent<EnemyMover>();
        }

        private void Start()
        {
            SwitchState(new EnemyPatrollingState(this));
        }
    }
}
