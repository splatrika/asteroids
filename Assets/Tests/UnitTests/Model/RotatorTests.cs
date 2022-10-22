using System;
using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class RotatorTests
    {
        public const float RotationSpeed = 25;

        private float _rotation;
        private Rotator _rotator;


        [SetUp]
        public void Init()
        {
            _rotation = default;

            var logger = new Mock<ILogger>().Object;

            var rotatableMock = new Mock<IRotatable>();
            rotatableMock.Setup(x => x.SetRotation(It.IsAny<float>()))
                .Callback<float>(rotation => _rotation = rotation);
            rotatableMock.SetupGet(x => x.Rotation)
                .Returns(() => _rotation);

            var configuration = new RotatorConfiguration(RotationSpeed);

            _rotator = new Rotator(rotatableMock.Object, logger, configuration);
        }


        [Test]
        [Sequential]
        [Description("Should rotate")]
        public void Rotation(
            [Values(1, -1)] int direction,
            [Values(RotationSpeed, RotationSpeed * -1)] float excepted)
        {
            _rotator.StartRotation(direction);
            _rotator.Update(1);
            Assert.AreEqual(excepted, _rotation);
        }


        [Test]
        [Description("Should stop rotation")]
        public void StopRotation()
        {
            _rotator.StartRotation(1);
            _rotator.StopRotation();
            _rotator.Update(1);
            Assert.Zero(_rotation);
        }


        [Test]
        [Description("Should throw excepion on passing invalid rotation direction")]
        public void InvalidValue(
            [Values(2, -2, 14)] int invalidValue)
        {
            Assert.Throws<ArgumentException>(
                () => _rotator.StartRotation(invalidValue));
        }


        [Test]
        [Description("Should invoke start rotation event")]
        public void StartRotationEvent (
            [Values(-1, 1)] int direction)
        {
            int? invoked = null;
            _rotator.RotationStarted += x => invoked = x;

            _rotator.StartRotation(direction);

            Assert.AreEqual(direction, invoked);
        }


        [Test]
        [Description("Should invoke stop rotation event")]
        public void StopRotationEvent()
        {
            var invoked = false;
            _rotator.RotationStopped += () => invoked = true;
            _rotator.StartRotation(1);
            _rotator.StopRotation();
            Assert.True(invoked);
        }
    }
}
