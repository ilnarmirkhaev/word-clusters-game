using Core.Modules;
using Core.SaveLoad;
using Core.SceneLoading;
using Core.Serialization;
using VContainer;
using VContainer.Unity;

namespace Contexts
{
    public class AppRoot : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            DontDestroyOnLoad(gameObject);
            base.Configure(builder);

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<SceneLoadingModule>(Lifetime.Singleton);
            builder.Register<SavesManager>(Lifetime.Singleton);

            builder.Register<ISerializer, JsonSerializer>(Lifetime.Singleton);
            builder.Register<ISaveLoadProvider, SaveLoadProviderByPrefs>(Lifetime.Singleton);

            RegisterModule<LevelsProgressionModule, LevelsProgressionState>(builder);
        }

        private static void RegisterModule<TModule, TState>(IContainerBuilder builder)
            where TModule : StateModuleBase<TState>
            where TState : StateBase, new()
        {
            builder.Register<TModule>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            RegisterStateProvider<TState>(builder);
        }

        private static void RegisterStateProvider<TState>(IContainerBuilder builder) where TState : StateBase, new()
        {
            builder.Register<StateProvider<TState>>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();
            builder.Register<TState>(resolver => resolver.Resolve<StateProvider<TState>>().State, Lifetime.Singleton);
        }
    }
}