using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public class Rotatable : IRotatable
    {
        public float Rotation => _rotation;

        private float _rotation;

        public event Action<float> Rotated;


        public void SetRotation(float rotation)
        {
            _rotation = rotation;
            Rotated.Invoke(_rotation);
        }
    }
}
