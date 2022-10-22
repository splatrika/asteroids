using System;

namespace Splatrika.Asteroids.Model
{
    public interface IRotator
    {
        event Action<int> RotationStarted;
        event Action RotationStopped;

        void StartRotation(int direction);
        void StopRotation();
    }
}
