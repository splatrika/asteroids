using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{   
    public class TeleportingMovableTests
    {
        public const float ScreenTop = 2;
        public const float ScreenBottom = -3;
        public const float ScreenLeft = -3;
        public const float ScreenRight = 3;


        private TeleportingMovable _movable;


        [SetUp]
        public void Init() {
            var screenMock = new Mock<IScreen>();
            screenMock.SetupGet(x => x.Top)
                .Returns(ScreenTop);
            screenMock.SetupGet(x => x.Bottom)
                .Returns(ScreenBottom);
            screenMock.SetupGet(x => x.Left)
                .Returns(ScreenLeft);
            screenMock.SetupGet(x => x.Right)
                .Returns(ScreenRight);
            screenMock.SetupGet(x => x.Width)
                .Returns(ScreenRight - ScreenLeft);
            screenMock.SetupGet(x => x.Height)
                .Returns(ScreenTop - ScreenBottom);

            _movable = new TeleportingMovable(screenMock.Object);
        }


        [Test]
        [Sequential]
        [Description("Should be moved")]
        public void Move(
            [Values(1, -1, 0.5f)] float x,
            [Values(-1, -1.2f, 0.3f)] float y)
        {
            var excepted = new Vector2(x, y);
            _movable.SetPosition(excepted);
            Assert.AreEqual(excepted, _movable.Position);
        }


        [Test]
        [Sequential]
        [Description("Should be teleportated after move behind thr screen")]
        public void Teleportate(
            [Values(3.5f, 0, -4f)] float x,
            [Values(0, -4f, 4)] float y,
            [Values(-2.5f, 0, 2)] float exceptedX,
            [Values(0, 1, -1)] float exceptedY)
        {
            var target = new Vector2(x, y);
            var excepted = new Vector2(exceptedX, exceptedY);
            _movable.SetPosition(target);
            Assert.AreEqual(excepted, _movable.Position);
        }


        [Test]
        [Description("Should invoke moved event")]
        public void MovedEvent()
        {
            var excepted = new Vector2(1, -0.2f);
            Vector2? invoked = null;
            _movable.Moved += position => invoked = position;
            _movable.SetPosition(excepted);
            Assert.AreEqual(excepted, invoked);
        }
    }
}
