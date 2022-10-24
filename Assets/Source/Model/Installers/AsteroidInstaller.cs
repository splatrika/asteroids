using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Model
{
    public class AsteroidInstaller : MonoInstaller
    {
        [SerializeField]
        private int _size;

        [SerializeField]
        private int _separationPeaces;

        [SerializeField]
        private float _separationSpeedUp;

        [SerializeField]
        private Vector2 _direction;

        [SerializeField]
        private float _speed;


        public override void InstallBindings()
        {
            var privateContainer = new DiContainer(Container);

            var configurationProvider =
                privateContainer.TryResolve<IAsteroidConfigurationProvider>();
            AsteroidConfiguration configuration = null;
            if (configurationProvider != null)
            {
                configuration = configurationProvider.Configuration;
            }
            else
            {
                configuration = new AsteroidConfiguration(
                    _size,
                    _separationPeaces,
                    _separationSpeedUp,
                    _direction,
                    transform.position,
                    _speed);
            }

            privateContainer.Bind<AsteroidConfiguration>()
                .FromInstance(configuration);

            var movable = privateContainer.Instantiate<Movable>();
            privateContainer.Bind<IMovable>()
                .FromInstance(movable);
            Container.Bind<IPosition>()
                .FromInstance(movable);

            var asteroid = privateContainer.Instantiate<Asteroid>();

            Container.Bind<IKickable>()
                .FromInstance(asteroid);
            Container.Bind<IAtacker>()
                .FromInstance(asteroid);
            Container.Bind<IUpdatable>()
                .FromInstance(asteroid);
            Container.Bind<IAsteroid>()
                .FromInstance(asteroid);
        }
    }
}
