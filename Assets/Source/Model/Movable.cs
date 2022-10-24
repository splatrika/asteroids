using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Movable : IMovable
    {
        public Vector2 Position { get; private set; }

        public event Action<Vector2> Moved;


        public void SetPosition(Vector2 position)
        {
            Position = position;
            Moved?.Invoke(position);
        }
    }
}
