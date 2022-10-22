using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class RotatorConfiguration
    {
        public float Speed { get; }


        public RotatorConfiguration(float speed)
        {
            Speed = speed;
        }
    }
}
