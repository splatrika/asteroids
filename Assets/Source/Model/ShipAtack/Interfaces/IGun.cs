using System;

namespace Splatrika.Asteroids.Model
{
    public interface IGun: IWeapon
    {
        event Action<ShotConfiguration> Shot;
    }
}
