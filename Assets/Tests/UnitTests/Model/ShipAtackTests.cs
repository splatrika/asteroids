using System.Collections;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Splatrika.Asteroids.Model;
using UnityEngine;
using UnityEngine.TestTools;

namespace Splatrika.Asteroids.Tests.UnitTests
{
    public class ShipAtackTests
    {
        private ShipAtack _atack;
        private string _lastCalledWeapon;

        [SetUp]
        public void Init()
        {
            var logger = new Mock<ILogger>();

            var weapons = new List<IWeapon>
            {
                CreateWeapon("Weapon1"),
                CreateWeapon("Weapon2")
            };

            _atack = new ShipAtack(weapons, logger.Object);
        }


        [Test]
        [Description("Should select weapon by name")]
        public void SelectWeapon(
            [Values("Weapon1", "Weapon2")] string name)
        {
            _atack.Attack(name);
            Assert.AreEqual(name, _lastCalledWeapon);
        }


        private IWeapon CreateWeapon(string name)
        {
            var mock = new Mock<IWeapon>();
            mock.SetupGet(x => x.Name)
                .Returns(name);
            mock.Setup(x => x.Attack())
                .Callback(() => _lastCalledWeapon = name);
            return mock.Object;
        }
    }
}
