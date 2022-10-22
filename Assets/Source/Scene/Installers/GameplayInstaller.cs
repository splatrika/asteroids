using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Scene
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField]
        private CameraScreen _screen;

        public override void InstallBindings()
        {
            Container.Bind<IScreen>()
                .FromInstance(_screen);
            Container.Bind<ILogger>()
                .FromInstance(Debug.unityLogger);
        }
    }
}
