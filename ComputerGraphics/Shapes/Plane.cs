using System;

namespace ComputerGraphics.Shapes
{
    public class Plane : IShape
    {
        public Point Center { get; }
        public Vector Normal { get; }

        public Plane(Point center, Vector normal)
        {
            Center = center;
            Normal = normal.Normalized();
        }

        public Point CheckIntersection(Point rayOrigin, Vector rayDirection)
        {
            //parameter scalar of the ray
            float t;

            //if angle between plane normal and ray is < 90 deg., calculate parameter t
            float denom = Vector.Dot(Normal, rayDirection);
            if (Math.Abs(denom) > 0.0001f)
            {
                t = Vector.Dot(Center - rayOrigin, Normal) / denom;

                //if intersection occured
                if (t >= 0)
                {
                    Vector rayHit = rayDirection * t;

                    //return intersection point
                    return rayOrigin + rayHit;
                }
            }

            return null;
        }

        public Vector GetIntersectionNormal(Point intersection)
        {
            return Normal;
        }

        public IShape Transform(System.Numerics.Matrix4x4 matrix)
        {
            throw new NotImplementedException();
        }
    }
}
