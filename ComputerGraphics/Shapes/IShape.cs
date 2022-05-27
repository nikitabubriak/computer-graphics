using System.Numerics;

namespace ComputerGraphics.Shapes
{
    public interface IShape
    {
        public Point CheckIntersection(Point rayOrigin, Vector rayDirection);
        public Vector GetIntersectionNormal(Point intersection);
        public IShape Transform(Matrix4x4 matrix);
    }
}
