using System;

namespace Splatrika.Asteroids.Model
{
    public interface IBullet: IAtacker
    {
        event Action ReInited;
        event Action Destroyed;

        void ReInit(ShotConfiguration configuration);
        void Hit();
    }
}
