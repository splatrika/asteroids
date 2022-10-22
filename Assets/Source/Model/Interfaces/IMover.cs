using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IMover
    {
        bool IsMoving { get; }

        event Action<Vector2> DirectionUpdated;
        event Action MovementStarted;
        event Action MovementStopped;

        void SetDirection(Vector2 direction);
        void StartMovement();
        void StopMovement();
    }
}
