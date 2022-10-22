using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class InertMoverConfiguration
    {
        public float Acceleration { get; }
        public float Deceleration { get; }
        public float Speed { get; }


        public InertMoverConfiguration(
            float acceleration,
            float deceleration,
            float speed)
        {
            Acceleration = acceleration;
            Deceleration = deceleration;
            Speed = speed;
        }
    }
}
