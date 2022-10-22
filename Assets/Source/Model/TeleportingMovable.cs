using System;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class TeleportingMovable : IMovable
    {
        public Vector2 Position => _position;
        
        private Vector2 _position;
        private readonly IScreen _screen;

        public event Action<Vector2> Moved;


        public TeleportingMovable(IScreen screen)
        {
            _screen = screen;
        }


        public void SetPosition(Vector2 position)
        {
            _position = position;
            if (_position.y > _screen.Top)
            {
                var offset = (_position.y - _screen.Top) % _screen.Height;
                _position.y = _screen.Bottom + offset;
            }
            if (_position.y < _screen.Bottom)
            {
                var offset = (_screen.Bottom - _position.y) % _screen.Height;
                _position.y = _screen.Top - offset;
            }
            if (_position.x > _screen.Right)
            {
                var offset = (_position.x - _screen.Right) % _screen.Width;
                _position.x = _screen.Left + offset;
            }
            if (_position.x < _screen.Left)
            {
                var offset = (_screen.Left - _position.x) % _screen.Width;
                _position.x = _screen.Right - offset;
            }
            Moved?.Invoke(_position);
        }
    }
}
