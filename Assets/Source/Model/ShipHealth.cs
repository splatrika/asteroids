using System;

namespace Splatrika.Asteroids.Model
{
    public class ShipHealth : IShipHealth
    {
        public event Action Killed;

        public void Kick(IAtacker atacker)
        {
            Killed?.Invoke();
        }
    }
}
