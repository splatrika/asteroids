namespace Splatrika.Asteroids.Model
{
    public interface IWeapon
    {
        string Name { get; }

        void Attack();
    }
}
