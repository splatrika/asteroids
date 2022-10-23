using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class LaserGun : ILaserGun, IUpdatable
    {
        public const string WeaponName = nameof(LaserGun);

        public int ShotCount { get; private set; }
        public int MaxShotCount { get; private set; }
        public float RegenerationTimeLeft { get; private set; }
        public float RegenerationTime { get; private set; }
        public string Name => WeaponName;

        private readonly IRotation _rotation;
        private readonly IPosition _position;

        public event Action<ShotConfiguration> Shot;
        public event Action Regenerated;


        public LaserGun(
            IRotation rotation,
            IPosition position,
            LaserGunConfiguration configuration)
        {
            _rotation = rotation;
            _position = position;

            MaxShotCount = configuration.MaxShotCount;
            RegenerationTime = configuration.RegenerationTime;

            ShotCount = MaxShotCount;
            RegenerationTimeLeft = RegenerationTime;
        }


        public void Attack()
        {
            if (ShotCount == 0)
            {
                return;
            }

            var direction = new Vector2(
                Mathf.Sin(_rotation.Rotation),
                Mathf.Cos(_rotation.Rotation));
            var configuration = new ShotConfiguration(
                direction, _position.Position);
            Shot?.Invoke(configuration);

            ShotCount--;
        }


        public void Update(float deltaTime)
        {
            if (ShotCount == MaxShotCount)
            {
                return;
            }
            RegenerationTimeLeft -= deltaTime;
            if (RegenerationTimeLeft <= 0)
            {
                RegenerationTimeLeft = RegenerationTime;
                ShotCount++;
                Regenerated?.Invoke();
            }
        }
    }
}
