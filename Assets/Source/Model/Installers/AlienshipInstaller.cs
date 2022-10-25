    using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Model
{
    public class AlienshipInstaller : MonoInstaller
    {
        private ILogger _logger;

        [SerializeField]
        private float _speed;


        [Inject]
        public void Init(ILogger logger)
        {
            _logger = logger;
        }


        public override void InstallBindings()
        {
            var privateContainer = new DiContainer(Container);

            AlienshipConfiguration configuration = null;
            var configurationProvider =
                privateContainer.TryResolve<IAlienshipConfigurationProvider>();
            if (configurationProvider != null)
            {
                configuration = configurationProvider.Congiguration;
            }
            else
            {
                _logger.LogError(nameof(AlienshipInstaller),
                    "There is no providen alienship configuration by container");
                return;
            }
            privateContainer.Bind<AlienshipConfiguration>()
                .FromInstance(configuration);

            var movable = privateContainer.Instantiate<Movable>();
            Container.Bind<IMovable>()
                .FromInstance(movable);
            Container.Bind<IPosition>()
                .FromInstance(movable);

            var alienship = privateContainer.Instantiate<Alienship>();
            Container.Bind<IAlienship>()
                .FromInstance(alienship);
            Container.Bind<IUpdatable>()
                .FromInstance(alienship);
            Container.Bind<IKickable>()
                .FromInstance(alienship);
        }
    }
}
