namespace Splatrika.Asteroids.Model
{
    public class GunConfiguration
    { 
        public float RegenerationTime { get; }


        public GunConfiguration(
            float regenerationTime)
        {
            RegenerationTime = regenerationTime;
        }
    }
}
