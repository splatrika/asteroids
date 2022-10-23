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


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.otherRigidbody
                .TryGetComponent(out KickablePresenter kickable))
            {
                kickable.Kick(_bullet);
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
