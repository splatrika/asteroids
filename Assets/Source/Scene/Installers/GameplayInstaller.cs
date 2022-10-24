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

        [SerializeField]
        private PlayerPositionProvider _playerPositionProvider;


        public override void InstallBindings()
        {
            Container.Bind<ILogger>()
                .FromInstance(Debug.unityLogger);
            Container.Bind<IScreen>()
                .FromInstance(_screen);
            Container.Bind<IPlayerPositionProvider>()
                .FromInstance(_playerPositionProvider);
            Container.Bind<IBulletsService>()
                .FromInstance(_bulletsService);
        }
    }
}
