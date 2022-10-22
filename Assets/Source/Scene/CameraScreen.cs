using System.Collections;
using System.Collections.Generic;
using Splatrika.Asteroids.Model;
using UnityEngine;

namespace Splatrika.Asteroids.Scene
{
    public class CameraScreen : MonoBehaviour, IScreen
    {
        public float Top { get; private set; }
        public float Bottom { get; private set; }
        public float Left { get; private set; }
        public float Right { get; private set; }
        public float Width => Right - Left;
        public float Height => Top - Bottom;

        [SerializeField]
        private Camera _camera;

        void Start()
        {
            var width = _camera.aspect * _camera.orthographicSize;
            var height = _camera.orthographicSize;
            var center = _camera.transform.position;

            Left = center.x - width;
            Right = center.x + width;
            Top = center.y + height;
            Bottom = center.y - height;
        }
    }
}
