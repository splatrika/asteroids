using System;

namespace Splatrika.Asteroids.Model
{
    public interface IAlienshipSpawner
    {
        event Action<AlienshipConfiguration> Spawned;
    }
}
