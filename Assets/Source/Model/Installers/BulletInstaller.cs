using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

public class BulletInstaller : MonoInstaller
{
    [SerializeField]
    private float _lifeTime;

    [SerializeField]
    private float _speed;


    public override void InstallBindings()
    {
        var privateContainer = new DiContainer(Container);

        var configuration = new BulletConfiguration(_speed, _lifeTime);
        privateContainer.Bind<BulletConfiguration>()
            .FromInstance(configuration);

        var bullet = privateContainer.Instantiate<Bullet>();
        Container.Bind<IBullet>()
            .FromInstance(bullet);
        Container.Bind<IAtacker>()
            .FromInstance(bullet);
        Container.Bind<IPosition>()
            .FromInstance(bullet);
        Container.Bind<IUpdatable>()
            .FromInstance(bullet);
    }
}
