using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Bullet : IBullet, IUpdatable, IAtacker, IPosition
    {
        public Vector2 Position => _movable.Position;

        private float _timeLeft;
        private float _lifeTime;
        private float _speed;
        private Vector2 _direction;
        private IMovable _movable;

        private bool _destroyed;

        public event Action ReInited;
        public event Action Destroyed;
        public event Action<Vector2> Moved;

        public Bullet(
            IScreen screen,
            BulletConfiguration configuration)
        {
            _movable = new TeleportingMovable(screen);
            _lifeTime = configuration.Lifetime;
            _speed = configuration.Speed;
            _destroyed = true;
        }


        public void Update(float deltaTime)
        {
            if (_timeLeft > 0 && !_destroyed)
            {
                var position = _movable.Position;
                position += _direction * _speed * deltaTime;
                _movable.SetPosition(position);
                _timeLeft -= deltaTime;
                Moved?.Invoke(position);
                return;
            }
            if (!_destroyed)
            {
                Destroyed?.Invoke();
                _destroyed = true;
            }
        }

        public void ReInit(ShotConfiguration configuration)
        {
            _timeLeft = _lifeTime;
            _destroyed = false;
            _movable.SetPosition(configuration.Position);
            _direction = configuration.Direction.normalized;
            ReInited?.Invoke();
        }


        public void Hit()
        {
            Destroyed?.Invoke();
            _destroyed = true;
        }
    }
}
