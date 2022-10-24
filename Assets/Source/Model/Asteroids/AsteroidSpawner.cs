using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Splatrika.Asteroids.Model
{
    public class AsteroidSpawner : IAsteroidSpawner, ISpawner
    {
        private int _size;
        private int _separationPeaces;
        private float _separationSpeedUp;
        private float _speed;
        private readonly IScreen _screen;

        public event Action<AsteroidConfiguration> Spawned;


        public AsteroidSpawner(
            IScreen screen,
            AsteroidSpawnerConfiguration configuration)
        {
            _screen = screen;

            _size = configuration.Size;
            _separationPeaces = configuration.SeparationPeaces;
            _separationSpeedUp = configuration.SeparationSpeedUp;
            _speed = configuration.Speed;
        }


        public void Spawn()
        {
            var radius = Mathf.Max(_screen.Width, _screen.Height);
            var center = new Vector2(
                _screen.Left + _screen.Right,
                _screen.Bottom + _screen.Top);
            var randomAngle = Random.Range(0, Mathf.PI * 2);
            var position = new Vector2(
                Mathf.Sin(randomAngle),
                Mathf.Cos(randomAngle)) * radius;
            var direction = position.normalized * -1;

            var asteroid = new AsteroidConfiguration(
                size: _size,
                separationPeaces: _separationPeaces,
                separationSpeedUp: _separationSpeedUp,
                direction: direction,
                position: position + center,
                speed: _speed);

            Spawned?.Invoke(asteroid);
        }
    }
}
