using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Bullet : IUpdatable, IAtacker, IPosition
    {
        public Vector2 Position => _movable.Position;

        private float _timeLeft;
        private float _speed;
        private Vector2 _direction;
        private IMovable _movable;

        private bool _destroyed;

        public event Action Destoryed;
        public event Action<Vector2> Moved;


        public Bullet(
            IScreen screen,
            float lifeTime,
            Vector2 direction,
            float speed,
            Vector2 startPosition)
        {
            _movable = new TeleportingMovable(screen);
            _timeLeft = lifeTime;
            _direction = direction.normalized;

            _movable.SetPosition(startPosition);
            _speed = speed;
        }


        public void Update(float deltaTime)
        {
            if (_timeLeft > 0)
            {
                var position = _movable.Position;
                position += _direction * _speed * deltaTime;
                _movable.SetPosition(position);
                _timeLeft -= deltaTime;
                Moved?.Invoke(position);
            }
            if (!_destroyed)
            {
                Destoryed?.Invoke();
                _destroyed = true;
            }
        }
    }
}
