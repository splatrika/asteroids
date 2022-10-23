using System;

namespace Splatrika.Asteroids.Model
{
    public interface ILaser: IAtacker
    {
        float Length { get; }

        event Action Destroyed;
    }
}
