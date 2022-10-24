using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class AsteroidTests
    {
        public const int Speed = 5;
        public const int SeparationSpeedUp = 5;
        public const int SeparationPeaces = 4;

        private Vector2 _position;
        private ILaser _laser;
        private IBullet _bullet;
        private IMovable _movable;
        private AsteroidConfiguration _defaultConfiguration;


        [SetUp]
        public void Init()
        {
            _laser = new Mock<ILaser>().Object;
            _bullet = new Mock<IBullet>().Object;

            var movableMock = new Mock<IMovable>();
            movableMock.Setup(x => x.SetPosition(It.IsAny<Vector2>()))
                .Callback<Vector2>(position => _position = position);
            movableMock.SetupGet(x => x.Position)
                .Returns(() => _position);
            _movable = movableMock.Object;

            _defaultConfiguration = new AsteroidConfiguration(
                size: 4,
                separationPeaces: SeparationPeaces,
                separationSpeedUp: SeparationSpeedUp,
                direction: Vector2.left,
                position: Vector2.zero,
                speed: Speed);
        }


        [Test]
        [Description("Should move")]
        public void Move()
        {
            var asteroid = new Asteroid(_movable, _defaultConfiguration);
            var previousPosition = _position;
            asteroid.Update(1);
            Assert.AreNotEqual(previousPosition, _position);
        }


        [Test]
        [Description("Should be destroyed on kick by laser")]
        public void DestroyByLaser()
        {
            var asteroid = new Asteroid(_movable, _defaultConfiguration);
            var destroyed = false;
            asteroid.Destroyed += () => destroyed = true;
            asteroid.Kick(_laser);
            Assert.True(destroyed);
        }


        [Test]
        [Description("Should be separated on kick by bullet")]
        public void SeparateByBullet()
        {
            var asteroid = new Asteroid(_movable, _defaultConfiguration);
            AsteroidSeparationContext separationContext = null;
            asteroid.Separated += context => separationContext = context;
            asteroid.Kick(_bullet);
            var exceptedSpeed = 10;

            Assert.NotNull(separationContext);
            Assert.AreEqual(SeparationPeaces, separationContext.Peases.Length);
            foreach (var peace in separationContext.Peases)
            {
                Assert.AreEqual(exceptedSpeed, peace.Speed);
            }
        }


        [Test]
        [Description("Should be destoryed on kick by bullet when size is 1")]
        public void DestroyWhenSmall()
        {
            var configuration = new AsteroidConfiguration(
                size: 1,
                separationPeaces: _defaultConfiguration.SeparationPeaces,
                separationSpeedUp: _defaultConfiguration.SeparationSpeedUp,
                direction: _defaultConfiguration.Direction,
                position: _defaultConfiguration.Position,
                speed: _defaultConfiguration.Speed);
            var asteroid = new Asteroid(_movable, configuration);
            var destroyed = false;
            asteroid.Destroyed += () => destroyed = true;
            asteroid.Kick(_bullet);
            Assert.True(destroyed);
        }
    }
}
