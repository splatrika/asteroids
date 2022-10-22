using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IRotatable : IRotation
    {
        void SetRotation(float rotation);
    }
}
