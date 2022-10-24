using System;

namespace Splatrika.Asteroids.Model
{
    public interface IAsteroidSpawner
    {
        event Action<AsteroidConfiguration> Spawned;
    }
}
