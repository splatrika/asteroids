using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class AsteroidSpawnerPresenter
        : MonoBehaviour, IAsteroidConfigurationProvider
    {
        [SerializeField]
        private AsteroidPresenter _prefab;

        private IAsteroidSpawner _spawner;
        private DiContainer _diContainer;

        public AsteroidConfiguration Configuration { get; private set; }


        [Inject]
        public void Init(IAsteroidSpawner spawner, DiContainer diContainer)
        {
            _spawner = spawner;
            _diContainer = new DiContainer(diContainer);
            _diContainer.Bind<IAsteroidConfigurationProvider>()
                .FromInstance(this);
            _diContainer.Bind<AsteroidSpawnerPresenter>()
                .FromInstance(this);

            _spawner.Spawned += OnSpawned;
        }


        public void Spawn(AsteroidConfiguration configuration)
        {
            Configuration = configuration;
            _diContainer.InstantiatePrefab(_prefab);
        }


        private void OnDestroy()
        {
            _spawner.Spawned -= OnSpawned;
        }


        private void OnSpawned(AsteroidConfiguration configuration)
        {
            Spawn(configuration);
        }
    }
}
