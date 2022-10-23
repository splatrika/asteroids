using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class GunTests
    {
        public const float RegenerationTime = 10;

        private Gun _gun;
        private Mock<IPosition> _position;
        private Mock<IRotation> _rotation;


        [SetUp]
        public void Init()
        {
            _rotation = new Mock<IRotation>();
            _position = new Mock<IPosition>();

            var configuration = new GunConfiguration(RegenerationTime);

            _gun = new Gun(_rotation.Object, _position.Object, configuration);
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
            _gun.Shot += configurarion => shot = configurarion;
            _gun.Attack();

            Assert.NotNull(shot);
            Assert.AreEqual(exceptedPositon, shot.Position);
            Assert.AreEqual(exceptedDirection.x, shot.Direction.x, 0.01);
            Assert.AreEqual(exceptedDirection.y, shot.Direction.y, 0.01);
        }


        [Test]
        [Description("Shouldn't shoot when regeneration isn't complete")]
        public void RegenerationIsNotComplete()
        {
            var shot = false;

            _gun.Attack();
            _gun.Shot += _ => shot = true;
            _gun.Attack();

            Assert.False(shot);
        }


        [Test]
        [Sequential]
        [Description("Should regenerate")]
        public void Regenerate(
            [Values(10, 2, 12)] float time,
            [Values(true, false, true)] bool excepted)
        {
            var shot = false;

            _gun.Attack();
            _gun.Update(time);
            _gun.Shot += _ => shot = true;
            _gun.Attack();

            Assert.AreEqual(excepted, shot);
        }
    }
}
