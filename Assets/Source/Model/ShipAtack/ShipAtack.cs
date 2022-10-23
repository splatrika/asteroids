using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class ShipAtack : IShipAtack
    {
        private readonly Dictionary<string, IWeapon> _weapons;
        private readonly ILogger _logger;


        public ShipAtack(List<IWeapon> weapons, ILogger logger)
        {
            _weapons = new Dictionary<string, IWeapon>();
            foreach (var weapon in weapons)
            {
                _weapons.Add(weapon.Name, weapon);
            }
            _logger = logger;
        }


        public void Attack(string weaponName)
        {
            if (!_weapons.ContainsKey(weaponName))
            {
                _logger.LogWarning(nameof(ShipAtack),
                    $"There is no weapon names {weaponName}");
            }
            _weapons[weaponName].Attack();
        }
    }
}
