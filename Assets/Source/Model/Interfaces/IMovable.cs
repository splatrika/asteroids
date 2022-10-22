using UnityEngine;

namespace Splatrika.Asteroids.Model
{
    public interface IMovable: IPosition
    {
        void SetPosition(Vector2 position);
    }
}
