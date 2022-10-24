using System;
using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class AsteroidPresenter : MonoBehaviour
    {
        private IAsteroid _asteroid;
        private AsteroidSpawnerPresenter _spawner;
        private ILogger _logger;

        [SerializeField]
        private float[] SizeScales;


        [Inject]
        public void Init(
            IAsteroid asteroid,
            AsteroidSpawnerPresenter spawner,
            ILogger logger)
        {
            _asteroid = asteroid;
            _spawner = spawner;
            _logger = logger;

            _asteroid.Separated += OnSeparate;
            _asteroid.Destroyed += OnAsteroidDestroy;

            SetupScale();
        }


        private void SetupScale()
        {
            if (SizeScales == null)
            {
                _logger.LogWarning(nameof(AsteroidPresenter),
                    "Size Scales isnt' setted");
            }
            var scaleIndex = _asteroid.Size - 1;
            if (SizeScales.Length <= scaleIndex || scaleIndex < 0)
            {
                _logger.LogWarning(nameof(AsteroidPresenter),
                    $"There is no scale setted for size {_asteroid.Size}");
                return;
            }
            var scale = SizeScales[scaleIndex];
            transform.localScale = new Vector2(scale, scale);
        }


        private void OnDestroy()
        {
            _asteroid.Separated -= OnSeparate;
            _asteroid.Destroyed -= OnAsteroidDestroy;
        }


        private void OnSeparate(AsteroidSeparationContext context)
        {
            foreach (var peace in context.Peases)
            {
                _spawner.Spawn(peace);
            }
        }


        private void OnAsteroidDestroy()
        {
            Destroy(gameObject);
        }
    }
}
