using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class InertMover : IMover, IUpdatable
    {
        public bool IsMoving => _isMoving;
        public Vector2 Velocity => _velocity;

        private bool _isMoving;
        private Vector2 _direction;
        private float _acceleration;
        private float _deceleration;
        private float _maxSpeed;
        private Vector2 _velocity;
        private readonly IMovable _movable;
        private readonly ILogger _logger;

        public event Action<Vector2> DirectionUpdated;
        public event Action MovementStarted;
        public event Action MovementStopped;


        public InertMover(
            IMovable movable,
            ILogger logger,
            InertMoverConfiguration configuration)
        {
            _velocity = Vector2.zero;
            _movable = movable;
            _logger = logger;
            _acceleration = configuration.Acceleration;
            _deceleration = configuration.Deceleration;
            _maxSpeed = configuration.Speed;
        }


        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
            DirectionUpdated?.Invoke(_direction);
        }


        public void StartMovement()
        {
            if (_isMoving)
            {
                _logger.LogWarning(nameof(InertMover),
                    "Movement is already started");
            }
            _isMoving = true;
        }


        public void StopMovement()
        {
            if (!_isMoving)
            {
                _logger.LogWarning(nameof(InertMover),
                    "Movement isn't started, but you trying to stop it");
            }
            _isMoving = false;
        }


        public void Update(float deltaTime)
        {
            var targetSpeed = _isMoving ? _maxSpeed : 0;
            var acceleration = IsMoving ? _acceleration : _deceleration;
            if (IsMoving)
            {
                _velocity = Accelerate(_velocity, _direction,
                    targetSpeed, deltaTime, acceleration);
            }
            else
            {
                _velocity = Decelerate(_velocity, _direction,
                    acceleration, deltaTime);
            }
            var position = _movable.Position;
            position += _velocity;
            _movable.SetPosition(position);
        }


        private Vector2 Accelerate(
            Vector2 velocity,
            Vector2 targetDirection,
            float targetSpeed,
            float deltaTime,
            float acceleration)
        {
            var targetVelocity = _direction * _maxSpeed;
            var distance = Vector2.Distance(_velocity, targetVelocity);
            var t = (acceleration * deltaTime) / distance;
            var updated = Vector2.Lerp(velocity, targetVelocity, t);

            return Vector2.ClampMagnitude(updated, targetSpeed);
        }


        private Vector2 Decelerate(
            Vector2 velocity,
            Vector2 targetDirection,
            float deceleration,
            float deltaTime)
        {
            var updated = velocity - velocity.normalized * (deceleration * deltaTime);
            if (Mathf.Sign(updated.x) != Mathf.Sign(velocity.x))
            {
                updated.x = 0;
            }
            if (Mathf.Sign(updated.y) != Mathf.Sign(velocity.y))
            {
                updated.y = 0;
            }
            return updated;
        }
    }
}
