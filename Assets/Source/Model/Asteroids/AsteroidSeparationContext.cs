using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class AsteroidSeparationContext
    {
        public AsteroidConfiguration[] Peases { get; set; }


        public AsteroidSeparationContext(AsteroidConfiguration[] peases)
        {
            Peases = peases;
        }
    }
}
