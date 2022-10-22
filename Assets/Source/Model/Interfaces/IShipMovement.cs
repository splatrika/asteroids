using System;

namespace Splatrika.Asteroids.Model
{
    public interface IShipMovement
    {
        event Action MovementStarted;
        event Action MovementStopped;
        event Action<int> RotationStarted;
        event Action RotationStopped;

        void StartRotation(int direction);
        void StopRotation();
        void StartMovement();
        void StopMovement();
    }
}
