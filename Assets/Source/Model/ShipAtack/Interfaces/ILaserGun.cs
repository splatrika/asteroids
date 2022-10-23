using System;

namespace Splatrika.Asteroids.Model
{
    public interface ILaserGun : IWeapon
    {
        int ShotCount { get; }
        float RegenerationTimeLeft { get; }

        event Action<ShotConfiguration> Shot;
        event Action Regenerated;
    }
}
