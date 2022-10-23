using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class LaserGunTests
    {
        public const int MaxShotCount = 3;
        public const float RegenerationTime = 15;

        private LaserGun _laserGun;
        private Mock<IPosition> _position;
        private Mock<IRotation> _rotation;


        [SetUp]
        public void Init()
        {
            _rotation = new Mock<IRotation>();
            _position = new Mock<IPosition>();

            var configuration = new LaserGunConfiguration(
                MaxShotCount, RegenerationTime);

            _laserGun = new LaserGun(_rotation.Object, _position.Object, configuration);
        }


        [Test]
        [Sequential]
        [Description("Should shoot")]
        public void Shoot(
            [Values(1, 12)] float positionX,
            [Values(2, -2.2f)] float positionY,
            [Values(0, Mathf.PI / 2)] float rotation,
            [Values(0, 1)] float exceptedDirectionX,
            [Values(1, 0)] float exceptedDirectionY)
        {
            var exceptedPositon = new Vector2(positionX, positionY);
            var exceptedDirection =
                new Vector2(exceptedDirectionX, exceptedDirectionY);

            _position.SetupGet(x => x.Position)
                .Returns(exceptedPositon);
            _rotation.SetupGet(x => x.Rotation)
                .Returns(rotation);

            ShotConfiguration? shot = null;
            _laserGun.Shot += configurarion => shot = configurarion;
            _laserGun.Attack();

            Assert.NotNull(shot);
            Assert.AreEqual(exceptedPositon, shot.Position);
            Assert.AreEqual(exceptedDirection.x, shot.Direction.x, 0.01);
            Assert.AreEqual(exceptedDirection.y, shot.Direction.y, 0.01);
        }


        [Test]
        [Description("Shouldn't shoot when it hasn't shoots")]
        public void NoShoots()
        {
            for (int i = 0; i < MaxShotCount; i++)
            {
                _laserGun.Attack();
            }
            var used = false;
            _laserGun.Shot += _ => used = true;
            _laserGun.Attack();
        }


        [Test]
        [Description("Should use shoots")]
        public void UseShot()
        {
            _laserGun.Attack();
            Assert.AreEqual(MaxShotCount - 1, _laserGun.ShotCount);
        }


        [Test]
        [Sequential]
        [Description("Should regenerate")]
        public void Regenerate(
            [Values(15, 10, 20)] float time,
            [Values(3, 2, 3)] int exceptedCount)
        {
            _laserGun.Attack();
            _laserGun.Update(time);
            Assert.AreEqual(exceptedCount, _laserGun.ShotCount);
        }


        [Test]
        [Description("Should invoke regenerated event")]
        public void ReneratedEvent()
        {
            _laserGun.Attack();
            var invoked = false;
            _laserGun.Regenerated += () => invoked = true;
            _laserGun.Update(RegenerationTime);

            Assert.True(invoked);
        }


        [Test]
        [Description("Shouldn't regenerate when shot count is max")]
        public void RegenerateUntilMax()
        {
            _laserGun.Update(RegenerationTime);
            Assert.AreEqual(MaxShotCount, _laserGun.ShotCount);
        }
    }
}
