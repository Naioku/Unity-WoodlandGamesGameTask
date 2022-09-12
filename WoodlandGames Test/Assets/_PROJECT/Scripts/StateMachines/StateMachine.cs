using UnityEngine;

namespace _PROJECT.Scripts.StateMachines
{
    public abstract class StateMachine : MonoBehaviour
    {
        private State _currentState;

        private void Update()
        {
            _currentState?.Tick(Time.deltaTime);
        }

        public void SwitchState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}
