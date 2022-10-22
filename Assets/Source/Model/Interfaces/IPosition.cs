using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IPosition
    {
        event Action<Vector2> Moved;

        Vector2 Position { get; }
    }
}
