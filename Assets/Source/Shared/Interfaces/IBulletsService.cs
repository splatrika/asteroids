using Splatrika.Asteroids.Model;

namespace Splatrika.Asteroids.Shared
{
    public interface IBulletsService
    {
        void Spawn(ShotConfiguration configuration);
    }
}
