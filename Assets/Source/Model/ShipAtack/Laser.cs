using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Laser : ILaser, IUpdatable
    {
        public float TimeLeft { get; private set; }
        public float Length { get; private set; }
        public Vector2 Direction { get; private set; }
        public Vector2 Position { get; }

        private bool _destroyed = false;

        public event Action Destroyed;


        public Laser(
            float timeLeft,
            float length,
            Vector2 direction,
            Vector2 startPosition)
        {
            TimeLeft = timeLeft;
            Length = length;
            Direction = direction.normalized;
            Position = Position;
        }


        public void Update(float deltaTime)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= deltaTime;
                return;
            }
            if (!_destroyed)
            {
                Destroyed?.Invoke();
                _destroyed = true;
            }
        }
    }
}
