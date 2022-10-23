using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using Splatrika.Asteroids.Presenter;
using Splatrika.Asteroids.Shared;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Scene
{
    public class BulletsService : MonoBehaviour, IBulletsService
    {
        [SerializeField]
        private int _poolSize;

        [SerializeField]
        private BulletPresenter _prefab;

        private Stack<BulletPresenter> _free;
        private Stack<BulletPresenter> _all;
        private ILogger _logger;
        private DiContainer _diContainer;


        [Inject]
        public void Init(ILogger logger, DiContainer diContainer)
        {
            _logger = logger;
            _diContainer = diContainer;

            _all = new Stack<BulletPresenter>();
            _free = new Stack<BulletPresenter>();
            for (int i = 0; i < _poolSize; i++)
            {
                var bulletObject =
                    _diContainer.InstantiatePrefab(_prefab.gameObject);
                var bullet = bulletObject.GetComponent<BulletPresenter>();
                _all.Push(bullet);
                _free.Push(bullet);
                bullet.Finished += OnBulletFinished;
            }
        }


        public void Spawn(ShotConfiguration configuration)
        {
            if (_free.TryPop(out BulletPresenter bullet))
            {
                bullet.Run(configuration);
            }
            else
            {
                _logger.LogError(nameof(BulletsService),
                    "There is no any free bullets in pool");
            }
        }


        private void OnDestroy()
        {
            while (_all.TryPop(out BulletPresenter bullet))
            {
                bullet.Finished -= OnBulletFinished;
            }
        }


        private void OnBulletFinished(BulletPresenter bullet)
        {
            _free.Push(bullet);
        }
    }
}
