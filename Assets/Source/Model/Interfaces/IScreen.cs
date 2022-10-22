namespace Splatrika.Asteroids.Model
{
    public interface IScreen
    {
        float Top { get; }
        float Bottom { get; }
        float Left { get; }
        float Right { get; }
        float Width { get; }
        float Height { get; }
    }
}
