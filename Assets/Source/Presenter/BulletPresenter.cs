using System;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class BulletPresenter : MonoBehaviour
    {
        private IBullet _bullet;

        public event Action<BulletPresenter> Finished;


        [Inject]
        public void Init(IBullet bullet)
        {
            _bullet = bullet;
            _bullet.Destroyed += OnBulletDestroyed;
            _bullet.ReInited += OnBulletReinited;
            gameObject.SetActive(false);
        }


        public void Start()
        {
            gameObject.SetActive(false);
        }


        public void Run(ShotConfiguration configuration)
        {
            _bullet.ReInit(configuration);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out KickablePresenter kickable))
            {
                kickable.Kick(_bullet);
                _bullet.Hit();
            }
        }


        private void OnDestroy()
        {
            _bullet.Destroyed -= OnBulletDestroyed;
            _bullet.ReInited -= OnBulletReinited;
        }


        private void OnBulletDestroyed()
        {
            gameObject.SetActive(false);
            Finished?.Invoke(this);
        }


        private void OnBulletReinited()
        {
            gameObject.SetActive(true);
        }
    }
}
