using Core.SaveLoad;
using Core.Serialization;

namespace Core.Modules
{
    public interface IStateProvider
    {
        string TypeName { get; }
        string SerializedState { get; }
    }

    public class StateProvider<TState> : IStateProvider where TState : StateBase, new()
    {
        private readonly ISaveLoadProvider _saveLoadProvider;
        private readonly ISerializer _serializer;

        private TState _state;

        public StateProvider(ISaveLoadProvider saveLoadProvider, ISerializer serializer)
        {
            _saveLoadProvider = saveLoadProvider;
            _serializer = serializer;
        }

        public TState State => _state ??= GetState();
        public string TypeName { get; } = typeof(TState).ToString();
        public string SerializedState => _serializer.Serialize(_state);

        private TState GetState() => TryLoadState(out var state) ? state : CreateDefaultState();

        private bool TryLoadState(out TState state)
        {
            var data = _saveLoadProvider.Load(typeof(TState).FullName);
            state = _serializer.Deserialize<TState>(data);
            return state != null;
        }

        private static TState CreateDefaultState() => new();
    }
}