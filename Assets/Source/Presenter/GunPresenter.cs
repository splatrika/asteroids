using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using Splatrika.Asteroids.Shared;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class GunPresenter : MonoBehaviour
    {
        private IGun _gun;
        private IBulletsService _bulletsService;


        [Inject]
        public void Init(IGun gun, IBulletsService bulletsService)
        {
            _gun = gun;
            _bulletsService = bulletsService;
            _gun.Shot += OnShoot;
        }


        private void OnDestroy()
        {
            _gun.Shot -= OnShoot;
        }


        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                _gun.Attack();
            }
        }


        private void OnShoot(ShotConfiguration configuration)
        {
            _bulletsService.Spawn(configuration);
        }
    }
}
