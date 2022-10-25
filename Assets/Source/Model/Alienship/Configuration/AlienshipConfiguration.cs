using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class AlienshipConfiguration
    {
        public float Speed { get; set; }
        public IPosition Target { get; set; }
        public Vector2 Position { get; set; }


        public AlienshipConfiguration(
            float speed,
            IPosition target,
            Vector2 position)
        {
            Speed = speed;
            Target = target;
            Position = position;
        }
    }
}
