using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class AlienshipSpawnerPresenter
        : MonoBehaviour, IAlienshipConfigurationProvider
    {
        private IAlienshipSpawner _spawner;
        private DiContainer _diContainer;

        public AlienshipConfiguration Congiguration { get; private set; }

        [SerializeField]
        private AlienshipPresenter _prefab;


        [Inject]
        public void Init(IAlienshipSpawner spawner, DiContainer diContainer)
        {

            _spawner = spawner;

            _diContainer = new DiContainer(diContainer);
            _diContainer.Bind<IAlienshipConfigurationProvider>()
                .FromInstance(this);

            _spawner.Spawned += OnSpawned;
        }


        private void OnDestroy()
        {
            _spawner.Spawned -= OnSpawned;
        }


        private void OnSpawned(AlienshipConfiguration configuration)
        {
            Congiguration = configuration;
            _diContainer.InstantiatePrefab(_prefab.gameObject);
        }
    }
}
