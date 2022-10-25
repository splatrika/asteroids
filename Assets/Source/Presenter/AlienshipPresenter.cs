using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class AlienshipPresenter : MonoBehaviour
    {
        private IAlienship _alienship;


        [Inject]
        public void Init(IAlienship alienship)
        {
            _alienship = alienship;

            _alienship.Destroyed += OnAlienshipDestroy;
        }


        private void OnDestroy()
        {
            _alienship.Destroyed -= OnAlienshipDestroy;
        }


        private void OnAlienshipDestroy()
        {
            Destroy(gameObject);
        }
    }
}
