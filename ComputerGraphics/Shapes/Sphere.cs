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
            //(o + d*t - c)2 = r2
            //d2*t2 + 2*d*t*k + k2 - r2 = 0
            //a = d2
            //b = 2*d*k
            //c = k2 - r2

            Vector k = rayOrigin - Center;
            Vector d = rayDirection;

            float a = Vector.Dot(d, d); // 1
            float b = Vector.Dot(d, k) * 2;
            float c = Vector.Dot(k, k) - Radius * Radius;

            float D = b * b - 4 * a * c;

            //parameter of the ray reaching first intersection point
            float t1 = -0.5f * (b + (float)Math.Sqrt(D));

            //if intersection occured
            if (t1 >= 0)
            {
                Vector rayHit = rayDirection * t1;

                //return intersection point
                return rayOrigin + rayHit;
            }

            return null;
        }

        public Vector GetIntersectionNormal(Point intersection)
        {
            return (intersection - Center).Normalized();
        }
    }
}
