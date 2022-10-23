using Splatrika.Asteroids.Model;
using Splatrika.Asteroids.Shared;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Scene
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraScreen _screen;

        [SerializeField]
        private BulletsService _bulletsService;

        public override void InstallBindings()
        {
            Container.Bind<ILogger>()
                .FromInstance(Debug.unityLogger);
            Container.Bind<IScreen>()
                .FromInstance(_screen);
            Container.Bind<IBulletsService>()
                .FromInstance(_bulletsService);
        }
    }
}
