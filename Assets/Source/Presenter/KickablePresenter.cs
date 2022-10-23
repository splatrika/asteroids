using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class KickablePresenter : MonoBehaviour
    {
        private IKickable _kickable;


        [Inject]
        public void Init(IKickable kickable)
        {
            _kickable = kickable;
        }


        public void Kick(IAtacker _atacker)
        {
            _kickable.Kick(_atacker);
        }
    }
}
