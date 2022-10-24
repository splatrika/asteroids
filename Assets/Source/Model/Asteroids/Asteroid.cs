using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Asteroid : IAsteroid, IKickable, IAtacker, IUpdatable
    {
        public int Size { get; private set; }
        public int SeparationPeaces { get; private set; }
        public float SeparationSpeedUp { get; private set; }
        public Vector2 Direction { get; private set; }
        public float Speed { get; private set; }

        private const float _lifeTime = 10;
        private float _timeLeft;

        public event Action Destroyed;
        public event Action<AsteroidSeparationContext> Separated;

        private readonly IMovable _movable;


        public Asteroid(IMovable movable, AsteroidConfiguration configuration)
        {
            _movable = movable;

            Size = configuration.Size;
            SeparationPeaces = configuration.SeparationPeaces;
            SeparationSpeedUp = configuration.SeparationSpeedUp;
            Direction = configuration.Direction.normalized;
            Speed = configuration.Speed;
            _movable.SetPosition(configuration.Position);

            _timeLeft = _lifeTime;
        }


        public void Update(float deltaTime)
        {
            var position = _movable.Position;
            position += Direction * Speed * deltaTime;
            _movable.SetPosition(position);

            _timeLeft -= deltaTime;
            if (_timeLeft <= 0)
            {
                Destroyed?.Invoke();
            }
        }


        public void Kick(IAtacker atacker)
        {
            if (atacker is IBullet)
            {
                if (Size == 1)
                {
                    Destroyed?.Invoke();
                    return;
                }
                Separate();
            }
            if (atacker is ILaser)
            {
                Destroyed?.Invoke();
            }
        }


        private void Separate()
        {
            var peaces = new AsteroidConfiguration[SeparationPeaces];
            for (int i = 0; i < SeparationPeaces; i++)
            {
                var directionAngle = Mathf.PI * 2 / SeparationPeaces * i;
                var direction = new Vector2(
                    Mathf.Sin(directionAngle),
                    Mathf.Cos(directionAngle));
                peaces[i] = new AsteroidConfiguration(
                    size: Size - 1,
                    separationPeaces: SeparationPeaces,
                    separationSpeedUp: SeparationSpeedUp,
                    direction: direction,
                    position: _movable.Position,
                    speed: Speed + SeparationSpeedUp);
            }
            var configuration = new AsteroidSeparationContext(peaces);
            Separated?.Invoke(configuration);
            Destroyed?.Invoke();
        }
    }
}
