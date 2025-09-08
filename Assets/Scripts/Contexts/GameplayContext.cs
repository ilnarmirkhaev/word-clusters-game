using Gameplay;
using UI.Views;
using VContainer;
using VContainer.Unity;

namespace Contexts
{
    public class GameplayContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<LevelController>().AsSelf();
            builder.Register<ILevelDataProvider, LevelDataProvider>(Lifetime.Singleton);
            builder.Register<ILevelStateFactory, LevelStateFactory>(Lifetime.Singleton);
            builder.Register<IPlayingFieldFactory, PlayingFieldFactory>(Lifetime.Singleton);

            builder.Register<LevelState>(container =>
            {
                var provider = container.Resolve<ILevelDataProvider>();
                var factory = container.Resolve<ILevelStateFactory>();
                var levelData = provider.GetLevelData();
                return factory.Create(levelData);
            }, Lifetime.Scoped);

            builder.RegisterComponentInHierarchy<ClusterDragHandler>();
        }
    }
}