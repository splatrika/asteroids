using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Moq;
using System;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class ShipMovementTests
    {
        private Mock<IRotation> _rotatationMock;
        private Mock<IRotator> _rotatorMock;
        private Mock<IMover> _moverMock;
        private ShipMovement _shipMovement;


        [SetUp]
        public void Init()
        {
            _rotatationMock = new Mock<IRotation>();
            _rotatorMock = new Mock<IRotator>();
            _moverMock = new Mock<IMover>();
            _shipMovement = new ShipMovement(
                _moverMock.Object,
                _rotatationMock.Object,
                _rotatorMock.Object);
        }


        [Test]
        [Description("Should start rotation")]
        public void StartRotation(
            [Values(-1, 1)] int direction)
        {
            int? actual = null;
            _rotatorMock.Setup(x => x.StartRotation(It.IsAny<int>()))
                .Callback<int>(x => actual = x);
            _shipMovement.StartRotation(direction);
            Assert.AreEqual(direction, actual);
        }


        [Test]
        [Description("Should stop rotation")]
        public void StopRotation()
        {
            var stopped = false;
            _rotatorMock.Setup(x => x.StopRotation())
                .Callback(() => stopped = true);
            _shipMovement.StopRotation();
            Assert.True(stopped);
        }


        [Test]
        [Description("Should start movement")]
        public void StartMovement()
        {
            var started = false;
            _moverMock.Setup(x => x.StartMovement())
                .Callback(() => started = true);
            _shipMovement.StartMovement();
            Assert.True(started);
        }


        [Test]
        [Description("Should stop movement")]
        public void StopMovement()
        {
            var stopped = false;
            _moverMock.Setup(x => x.StopMovement())
                .Callback(() => stopped = true);
            _shipMovement.StopMovement();
            Assert.True(stopped);
        }


        [Test]
        [Description("Should invoke start movement")]
        public void StartMovementEvent()
        {
            var invoked = false;
            _moverMock.Object.MovementStarted +=
                () => invoked = true;

            _moverMock.Raise(x => x.MovementStarted += null);

            Assert.True(invoked);
        }


        [Test]
        [Description("Should invoke stop movement")]
        public void StopMovementEvent()
        {
            var invoked = false;
            _moverMock.Object.MovementStopped +=
                () => invoked = true;

            _moverMock.Raise(x => x.MovementStopped += null);

            Assert.True(invoked);
        }


        [Test]
        [Description("Should invoke start rotation")]
        public void StartRotationEvent(
            [Values(-1, 1)] int direction)
        {
            int? invoked = null;
            _shipMovement.RotationStarted += x => invoked = x;

            _rotatorMock.Raise(x => x.RotationStarted += null, direction);

            Assert.AreEqual(direction, invoked);
        }


        [Test]
        [Description("Should invoke stop rotation")]
        public void StopRotationEvent()
        {
            bool invoked = false;
            _shipMovement.RotationStopped += () => invoked = true;

            _rotatorMock.Raise(x => x.RotationStopped += null);

            Assert.True(invoked);
        }


        [Test]
        [Sequential]
        [Description("Should update direction after rotation")]
        public void UpdateMovementDirection(
            [Values(0, Mathf.PI, Mathf.PI / 2f)] float rotation,
            [Values(0, 0, 1)] float exceptedX,
            [Values(1, -1, 0)] float exceptedY)
        {
            var exceptedDirection = new Vector2(exceptedX, exceptedY);
            var actualDirection = Vector2.zero;
            _moverMock.Setup(x => x.SetDirection(It.IsAny<Vector2>()))
                .Callback<Vector2>(x => actualDirection = x);

            _rotatationMock.Raise(x => x.Rotated += null, rotation);

            Assert.AreEqual(exceptedDirection.x, actualDirection.x, 0.1f);
            Assert.AreEqual(exceptedDirection.y, actualDirection.y, 0.1f);
        }
    }
}
