using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Model {
    public class AlienshipSpawnerInstaller : MonoInstaller
    {
        [SerializeField]
        private float _speed;


        public override void InstallBindings()
        {
            var privateContainer = new DiContainer(Container);

            var configuration = new AlienshipSpawnerConfiguration(_speed);
            privateContainer.Bind<AlienshipSpawnerConfiguration>()
                .FromInstance(configuration);

            var spawner = privateContainer.Instantiate<AlienshipSpawner>();
            Container.Bind<ISpawner>()
                .FromInstance(spawner);
            Container.Bind<IAlienshipSpawner>()
                .FromInstance(spawner);
        }
    }
}
