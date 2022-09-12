namespace _PROJECT.Scripts.StateMachines
{
    public abstract class State
    {
        /// <summary>
        /// Is called on the first frame of state.
        /// </summary>
        public abstract void Enter();
        
        /// <summary>
        /// Is called on the every frame of state.
        /// </summary>
        public abstract void Tick(float deltaTime);
        
        /// <summary>
        /// Is called on the last frame of state.
        /// </summary>
        public abstract void Exit();
    }
}
