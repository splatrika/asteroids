using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class BulletConfiguration
    {
        public float Speed { get; set; }
        public float Lifetime { get; set; }


        public BulletConfiguration(float speed, float lifetime)
        {
            Speed = speed;
            Lifetime = lifetime;
        }
    }
}
