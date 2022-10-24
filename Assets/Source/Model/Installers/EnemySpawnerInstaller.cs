using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Model
{
    public class EnemySpawnerInstaller : MonoInstaller
    {
        [SerializeField]
        private float _frequency;


        public override void InstallBindings()
        {
            var privateContainer = new DiContainer(Container);

            var configuration = new EnemySpawnerConfiguration(_frequency);
            privateContainer.Bind<EnemySpawnerConfiguration>()
                .FromInstance(configuration);

            var spawner = privateContainer.Instantiate<EnemySpawner>();
            Container.Bind<IEnemySpawner>()
                .FromInstance(spawner);
        }
    }
}
