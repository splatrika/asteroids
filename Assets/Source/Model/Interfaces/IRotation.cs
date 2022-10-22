using System;

namespace Splatrika.Asteroids.Model
{
    public interface IRotation
    {
        event Action<float> Rotated;

        float Rotation { get; }
    }
}
