using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class PositionPresenter : MonoBehaviour
    {
        private IPosition _position;
        private Transform _transform;


        [Inject]
        public void Init(IPosition position)
        {
            _position = position;
            _transform = transform;
            _position.Moved += OnMoved;
        }


        private void OnDestroy()
        {
            _position.Moved -= OnMoved;
        }


        private void OnMoved(Vector2 position)
        {
            _transform.position = (Vector3)position;
        }
    }
}
