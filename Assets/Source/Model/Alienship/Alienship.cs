using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Alienship: IAlienship, IUpdatable, IKickable
    {
        public event Action Destroyed;

        private float _speed;
        private readonly IPosition _target;
        private readonly IMovable _movable;


        public Alienship(IMovable movable, AlienshipConfiguration configuration)
        {
            _movable = movable;

            _speed = configuration.Speed;
            _target = configuration.Target;
            _movable.SetPosition(configuration.Position);
        }


        public void Kick(IAtacker atacker)
        {
            Destroyed?.Invoke();
        }


        public void Update(float deltaTime)
        {
            var direction = _target.Position - _movable.Position;
            direction.Normalize();
            var position = _movable.Position;
            position += direction * _speed * deltaTime;
            _movable.SetPosition(position);
        }
    }
}
