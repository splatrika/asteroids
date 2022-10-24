using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IAsteroid
    {
        event Action Destroyed;
        event Action<AsteroidSeparationContext> Separated;

        int Size { get; }
    }
}
