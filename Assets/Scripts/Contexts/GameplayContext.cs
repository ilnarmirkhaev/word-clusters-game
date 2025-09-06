using Gameplay;
using VContainer;
using VContainer.Unity;

namespace Contexts
{
    public class GameplayContext : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.RegisterEntryPoint<LevelController>();
            builder.Register<LevelDataProvider>(Lifetime.Singleton);
        }
    }
}