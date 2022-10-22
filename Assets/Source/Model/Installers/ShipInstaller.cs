using UnityEngine;
using Zenject;
using System;

namespace Splatrika.Asteroids.Model
{
    public class ShipInstaller : MonoInstaller
    {
        [Header("Mover configuration")]

        [SerializeField]
        private float _acceleration;

        [SerializeField]
        private float _deceleration;

        [SerializeField]
        private float _movementSpeed;

        [Header("Rotator configuration")]

        [SerializeField]
        private float _rotationSpeed;


        public override void InstallBindings()
        {
            var privateContainer = new DiContainer(Container);


            var movable = privateContainer.Instantiate<TeleportingMovable>();
            var rotatable = privateContainer.Instantiate<Rotatable>();

            Container.Bind<IPosition>()
                .FromInstance(movable);

            Container.Bind<IRotation>()
                .FromInstance(rotatable);

            privateContainer.Bind<IMovable>()
                .FromInstance(movable);

            privateContainer.Bind<IRotatable>()
                .FromInstance(rotatable);

            var moverConfiguration = new InertMoverConfiguration(
                _acceleration, _deceleration, _movementSpeed);

            var rotatorConfiguration = new RotatorConfiguration(
                _rotationSpeed);

            privateContainer.Bind<InertMoverConfiguration>()
                .FromInstance(moverConfiguration);

            privateContainer.Bind<RotatorConfiguration>()
                .FromInstance(rotatorConfiguration);


            var mover = privateContainer.Instantiate<InertMover>();

            privateContainer.Bind<IMover>()
                .FromInstance(mover);


            var rotator = privateContainer.Instantiate<Rotator>();

            privateContainer.Bind<IRotator>()
                .FromInstance(rotator);


            var shipMovemenet = privateContainer.Instantiate<ShipMovement>();

            Container.Bind<IShipMovement>()
                .FromInstance(shipMovemenet);


            Container.Bind<IDisposable>()
                .FromInstance(shipMovemenet);


            Container.Bind<IUpdatable>()
                .FromInstance(mover);

            Container.Bind<IUpdatable>()
                .FromInstance(rotator);
        }
    }
}
