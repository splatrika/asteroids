using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class EnemySpawnerPresenter : MonoBehaviour
    {
        private IEnemySpawner _spawner;


        [Inject]
        public void Init(IEnemySpawner spawner)
        {
            _spawner = spawner;
        }


        private void Update()
        {
            _spawner.Update(Time.deltaTime);
        }
    }
}
