using Splatrika.Asteroids.Model;
using UnityEngine;
using Zenject;

namespace Splatrika.Asteroids.Presenter
{
    public class RotationPresenter : MonoBehaviour
    {
        private IRotation _rotation;
        private Transform _transform;


        [Inject]
        public void Init(IRotation rotation)
        {
            _rotation = rotation;
            _transform = transform;
            _rotation.Rotated += OnRotated;
        }


        private void OnDestroy()
        {
            _rotation.Rotated -= OnRotated;
        }


        private void OnRotated(float rotation)
        {
            _transform.rotation = Quaternion.Euler(
                new Vector3(0, 0, rotation * Mathf.Rad2Deg * -1));
        }
    }
}
