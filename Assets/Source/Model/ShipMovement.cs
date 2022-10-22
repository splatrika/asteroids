using System;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class ShipMovement : IShipMovement, IDisposable
    {
        private readonly IMover _movement;
        private readonly IRotation _rotatation;
        private readonly IRotator _rotator;

        public event Action MovementStarted;
        public event Action MovementStopped;
        public event Action<int> RotationStarted;
        public event Action RotationStopped;

        public ShipMovement(
            IMover movement,
            IRotation rotatation,
            IRotator rotator)
        {
            _movement = movement;
            _rotatation = rotatation;
            _rotator = rotator;
            OnRotated(_rotatation.Rotation);

            _rotatation.Rotated += OnRotated;
            _movement.MovementStarted += OnMovementStarted;
            _movement.MovementStopped += OnMovementStopped;
            _rotator.RotationStarted += OnRotationStarted;
            _rotator.RotationStopped += OnRotationStopped;
        }


        public void Dispose()
        {
            _movement.MovementStarted -= OnMovementStarted;
            _movement.MovementStopped -= OnMovementStopped;
            _rotator.RotationStarted -= OnRotationStarted;
            _rotator.RotationStopped -= OnRotationStopped;
        }


        public void StartRotation(int direction)
        {
            _rotator.StartRotation(direction);
        }


        public void StopRotation()
        {
            _rotator.StopRotation();
        }


        public void StartMovement()
        {
            _movement.StartMovement();
        }


        public void StopMovement()
        {
            _movement.StopMovement();
        }


        private void OnMovementStarted()
        {
            MovementStarted?.Invoke();
        }


        public void OnMovementStopped()
        {
            MovementStopped?.Invoke();
        }


        private void OnRotationStarted(int direction)
        {
            RotationStarted?.Invoke(direction);
        }


        private void OnRotationStopped()
        {
            RotationStopped?.Invoke();
        }


        private void OnRotated(float rotation)
        {
            var movementDirection = new Vector2(
                Mathf.Sin(rotation),
                Mathf.Cos(rotation));
            _movement.SetDirection(movementDirection);
        }
    }
}
