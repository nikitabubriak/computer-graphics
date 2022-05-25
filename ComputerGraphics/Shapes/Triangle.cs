using System;

namespace ComputerGraphics.Shapes
{
    public class Triangle : IShape
    {
        public Point A { get; }
        public Point B { get; }
        public Point C { get; }

        public Vector ANormal { get; }
        public Vector BNormal { get; }
        public Vector CNormal { get; }

        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Triangle(
            Point a, Point b, Point c, 
            Vector aN, Vector bN, Vector cN): this(a, b, c)
        {
            ANormal = aN;
            BNormal = bN;
            CNormal = cN;
        }

        public Point CheckIntersection(Point rayOrigin, Vector rayDirection)
        {
            //Möller–Trumbore ray-triangle intersection algorithm
            //using barycentric coordinates

            Vector edge1 = B - A;
            Vector edge2 = C - A;
            Vector h;
            Vector s;
            Vector q;
            float a, f, u, v;

            h = Vector.Cross(rayDirection, edge2);
            a = Vector.Dot(edge1, h);
            if (a > -0.0001f && a < 0.0001f)
            {
                return null;    // This ray is parallel to this triangle.
            }

            f = 1.0f / a;
            s = rayOrigin - A;
            u = f * Vector.Dot(s, h);
            if (u < 0.0f || u > 1.0f)
            {
                return null;
            }

            q = Vector.Cross(s, edge1);
            v = f * Vector.Dot(rayDirection, q);
            if (v < 0.0f || u + v > 1.0f)
            {
                return null;
            }

            //parameter of the ray reaching first intersection point
            float t = f * Vector.Dot(edge2, q);

            //if intersection occured
            if (t >= 0.0001f)
            {
                Vector rayHit = rayDirection * t;

                //return intersection point
                return rayOrigin + rayHit;
            }
            // This means that there is a line intersection but not a ray intersection.
            return null;
        }

        public Vector GetIntersectionNormal(Point intersection)
        {
            Vector edge1 = B - A;
            Vector edge2 = C - A;
            Vector normal = Vector.Cross(edge1, edge2).Normalized();
            return normal;
        }
    }
}
