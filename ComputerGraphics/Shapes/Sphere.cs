using System;

namespace ComputerGraphics.Shapes
{
    public class Sphere : IShape
    {
        public Point Center { get; }
        public float Radius { get; }

        public Sphere(Point center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public Point CheckIntersection(Point rayOrigin, Vector rayDirection)
        {
            throw new NotImplementedException();
        }

        public Vector GetIntersectionNormal(Point intersection)
        {
            throw new NotImplementedException();
        }
    }
}
