using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class AsteroidConfiguration
    {
        public int Size { get; set; }
        public int SeparationPeaces { get; set; }
        public float SeparationSpeedUp { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }


        public AsteroidConfiguration(
            int size,
            int separationPeaces,
            float separationSpeedUp,
            Vector2 direction,
            Vector2 position,
            float speed)
        {
            Size = size;
            SeparationPeaces = separationPeaces;
            SeparationSpeedUp = separationSpeedUp;
            Direction = direction;
            Position = position;
            Speed = speed;
        }
    }
}
