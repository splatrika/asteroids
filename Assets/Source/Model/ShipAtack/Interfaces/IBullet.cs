using System;

namespace Splatrika.Asteroids.Model
{
    public interface IBullet: IAtacker
    {
        event Action Destroyed;
    }
}
