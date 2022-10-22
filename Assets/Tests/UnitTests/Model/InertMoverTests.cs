using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class InertMoverTests
    {
        public const float Acceleration = 5;
        public const float Deceleration = 2;
        public const float MaxSpeed = 10;

        private InertMover _mover;
        private Vector2 _position;


        [SetUp]
        public void Init()
        {
            _position = default;

            var logger = new Mock<ILogger>().Object;

            var movableMock = new Mock<IMovable>();
            movableMock.Setup(x => x.SetPosition(It.IsAny<Vector2>()))
                .Callback<Vector2>(x => _position = x);
            movableMock.SetupGet(x => x.Position)
                .Returns(() => _position);

            var configuration = new InertMoverConfiguration(
                Acceleration, Deceleration, MaxSpeed);
            _mover = new InertMover(movableMock.Object, logger, configuration);
        }


        [Test]
        [Sequential]
        [Description("Should accelerate")]
        public void Accelerate(
            [Values(1, 2, 4)] float updateTime,
            [Values(-5, -10, -10)] float exceptedX)
        {
            _mover.SetDirection(Vector2.left);
            _mover.StartMovement();
            _mover.Update(updateTime);
            Assert.AreEqual(exceptedX, _mover.Velocity.x, float.Epsilon);
            Assert.AreEqual(0, _mover.Velocity.y, float.Epsilon);
        }


        [Test]
        [Sequential]
        [Description("Should deccelerate")]
        public void Deccelerate(
            [Values(1, 2, 50)] float deccelerateTime,
            [Values(8, 6, 0)] float exceptedY)
        {
            _mover.SetDirection(Vector2.up);
            _mover.StartMovement();
            _mover.Update(2);
            _mover.StopMovement();
            _mover.Update(deccelerateTime);
            Assert.AreEqual(0, _mover.Velocity.x, float.Epsilon);
            Assert.AreEqual(exceptedY, _mover.Velocity.y, float.Epsilon);
        }


        [Test]
        [Description("Should move")]
        public void Move()
        {
            _mover.SetDirection(Vector2.up);
            _mover.Update(1);
            Assert.AreEqual(_mover.Velocity, _position);
        }
    }
}
