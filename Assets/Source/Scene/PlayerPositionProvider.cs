using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Scene
{
    public class PlayerPositionProvider : MonoBehaviour, IPlayerPositionProvider
    {
        public IPosition Position => _postion;

        private IPosition _postion;


        [Inject]
        public void Init(IPosition position)
        {
            _postion = position;
        }
    }
}
