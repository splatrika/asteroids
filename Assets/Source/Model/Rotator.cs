using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Rotator : IRotator, IUpdatable
    {
        private float _speed;
        private int _direction;
        private readonly IRotatable _rotatable;
        private readonly ILogger _logger;

        public event Action<int> RotationStarted;
        public event Action RotationStopped;

        public Rotator(
            IRotatable rotatable,
            ILogger logger,
            RotatorConfiguration configuration)
        {
            _rotatable = rotatable;
            _logger = logger;
            _speed = configuration.Speed;
        }


        public void StartRotation(int direction)
        {
            if (_direction != 0)
            {
                _logger.LogWarning(nameof(Rotator),
                    "Rotation is already started");
            }
            if (direction != 1 && direction != -1)
            {
                throw new ArgumentException(
                    "You can pass -1 and 1 as direction only");
            }
            _direction = direction;
            RotationStarted?.Invoke(direction);
        }


        public void StopRotation()
        {
            if (_direction == 0)
            {
                _logger.LogWarning(nameof(Rotator),
                    "Rotation isn't started, byt you trying to stop it");
            } 
            _direction = 0;
            RotationStopped?.Invoke();
        }


        public void Update(float deltaTime)
        {
            if (_direction != 0)
            {
                var newRotation = _rotatable.Rotation +
                    _speed * _direction * deltaTime;
                _rotatable.SetRotation(newRotation);
            }
        }
    }
}
