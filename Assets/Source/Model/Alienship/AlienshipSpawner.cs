using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Splatrika.Asteroids.Model
{
    public class AlienshipSpawner: IAlienshipSpawner, ISpawner
    {
        private readonly float _speed;
        private readonly IPlayerPositionProvider _playerPositionProvider;
        private readonly IScreen _screen;

        public event Action<AlienshipConfiguration> Spawned;


        public AlienshipSpawner(
            IPlayerPositionProvider playerPositionProvider,
            IScreen screen,
            AlienshipSpawnerConfiguration configuration)
        {
            _playerPositionProvider = playerPositionProvider;
            _screen = screen;

            _speed = configuration.Speed;
        }


        public void Spawn()
        {
            var target = _playerPositionProvider.Position;
            var radius = Mathf.Max(_screen.Width, _screen.Height);
            var center = new Vector2(
                _screen.Left + _screen.Right,
                _screen.Bottom + _screen.Top);
            var randomAngle = Random.Range(0, Mathf.PI * 2);
            var position = new Vector2(
                Mathf.Sin(randomAngle),
                Mathf.Cos(randomAngle)) * radius;

            var configuration = new AlienshipConfiguration(
                _speed, target, position);

            Spawned?.Invoke(configuration);
        }
    }
}
