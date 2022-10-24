using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Model
{
    public class AsteroidsSpawnerInstaller : MonoInstaller
    {
        [SerializeField]
        private int _size;

        [SerializeField]
        private int _separatedPeaces;

        [SerializeField]
        private float _separationSpeedUp;

        [SerializeField]
        private float _speed;


        public override void InstallBindings()
        {
            var privateContainer = new DiContainer(Container);

            var configuration = new AsteroidSpawnerConfiguration(
                _size, _separatedPeaces, _separationSpeedUp, _speed);
            privateContainer.Bind<AsteroidSpawnerConfiguration>()
                .FromInstance(configuration);

            var spawner = privateContainer.Instantiate<AsteroidSpawner>();
            Container.Bind<IAsteroidSpawner>()
                .FromInstance(spawner);
            Container.Bind<ISpawner>()
                .FromInstance(spawner);
        }
    }
}
