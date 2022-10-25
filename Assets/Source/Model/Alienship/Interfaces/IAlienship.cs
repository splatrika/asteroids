using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IAlienship : IAtacker
    {
        event Action Destroyed;
    }
}
