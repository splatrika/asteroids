using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Gun : IGun, IUpdatable
    {
        public const string WeaponName = nameof(Gun);

        public string Name => WeaponName;
        public float RegenerationTimeLeft { get; private set; }
        public float RegenerationTime { get; }
        private readonly IRotation _rotation;
        private readonly IPosition _position;

        public event Action<ShotConfiguration> Shot;


        public Gun(
            IRotation rotation,
            IPosition position,
            GunConfiguration configuration)
        {
            _rotation = rotation;
            _position = position;

            RegenerationTime = configuration.RegenerationTime;
        }


        public void Attack()
        {
            if (RegenerationTimeLeft > 0)
            {
                return;
            }
            var direction = new Vector2(
                Mathf.Sin(_rotation.Rotation),
                Mathf.Cos(_rotation.Rotation));
            var configuration = new ShotConfiguration(
                direction, _position.Position);
            Shot?.Invoke(configuration);

            RegenerationTimeLeft = RegenerationTime;
        }


        public void Update(float deltaTime)
        {
            if (RegenerationTimeLeft > 0)
            {
                RegenerationTimeLeft -= deltaTime;
            }
        }
    }
}
