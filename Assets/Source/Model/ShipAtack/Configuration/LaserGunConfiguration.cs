namespace Splatrika.Asteroids.Model
{
    public class LaserGunConfiguration
    {
        public int MaxShotCount { get; }
        public float RegenerationTime { get; }


        public LaserGunConfiguration(
            int maxShootCount,
            float regenerationTime)
        {
            MaxShotCount = maxShootCount;
            RegenerationTime = regenerationTime;
        }
    }
}
