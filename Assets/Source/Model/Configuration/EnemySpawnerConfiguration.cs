namespace Splatrika.Asteroids.Model
{
    public class EnemySpawnerConfiguration
    {
        public float Frequency { get; set; }


        public EnemySpawnerConfiguration(float frequency)
        {
            Frequency = frequency;
        }
    }
}
