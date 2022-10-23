using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class ShotConfiguration
    {
        public Vector2 Direction;
        public Vector2 Position;


        public ShotConfiguration(Vector2 direction, Vector2 position)
        {
            Direction = direction;
            Position = position;
        }
    }
}
