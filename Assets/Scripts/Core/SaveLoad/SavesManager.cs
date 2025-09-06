using System.Collections.Generic;
using Core.Modules;

namespace Core.SaveLoad
{
    public class SavesManager
    {
        private readonly ISaveLoadProvider _saveLoadProvider;
        private readonly IReadOnlyList<IStateProvider> _stateProviders;

        public SavesManager(ISaveLoadProvider saveLoadProvider, IReadOnlyList<IStateProvider> stateProviders)
        {
            _saveLoadProvider = saveLoadProvider;
            _stateProviders = stateProviders;
        }

        public void Save()
        {
            foreach (var provider in _stateProviders)
            {
                _saveLoadProvider.Save(provider.TypeName, provider.SerializedState);
            }
        }
    }
}