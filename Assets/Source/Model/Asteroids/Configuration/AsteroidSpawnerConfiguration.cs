namespace Splatrika.Asteroids.Model
{
    public class AsteroidSpawnerConfiguration
    {
        public int Size { get; set; }
        public int SeparationPeaces { get; set; }
        public float SeparationSpeedUp { get; set; }
        public float Speed { get; set; }


        public AsteroidSpawnerConfiguration(
            int size,
            int separationPeaces,
            float separationSpeedUp,
            float speed)
        {
            Size = size;
            SeparationPeaces = separationPeaces;
            SeparationSpeedUp = separationSpeedUp;
            Speed = speed;
        }
    }
}
