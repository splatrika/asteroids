using System;

namespace Splatrika.Asteroids.Model
{
    public interface IShipHealth : IKickable
    {
        event Action Killed;
    }
}
