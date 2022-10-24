using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class EnemySpawner : IEnemySpawner
    {
        private float _frequency;
        private float _timeLeft;
        private readonly List<ISpawner> _spawners;
        private readonly ILogger _logger;


        public EnemySpawner(
            List<ISpawner> spawners,
            EnemySpawnerConfiguration configuration,
            ILogger logger)
        {
            _spawners = spawners;
            _logger = logger;

            _frequency = configuration.Frequency;

            _timeLeft = _frequency;
        }


        public void Update(float deltaTime)
        {
            if (_spawners == null || _spawners.Count == 0)
            {
                _logger.LogWarning(nameof(EnemySpawner),
                    "There is no any spawners");
                return;
            }
            _timeLeft -= deltaTime;
            if (_timeLeft <= 0)
            {
                var spawnerIndex = Random.Range(0, _spawners.Count - 1);
                _spawners[spawnerIndex].Spawn();
                _timeLeft = _frequency;
            }
        }
    }
}
