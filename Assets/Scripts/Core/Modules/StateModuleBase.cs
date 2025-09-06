namespace Core.Modules
{
    public abstract class StateBase
    {
    }

    public abstract class StateModuleBase<TState> where TState : StateBase, new()
    {
        private readonly TState _state;

        protected TState State => _state;

        protected StateModuleBase(TState state)
        {
            _state = state;
        }
    }
}