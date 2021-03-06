using System;
using System.Numerics;

namespace ComputerGraphics
{
    public class Vector
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public float Magnitude { get; }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
            Magnitude = (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        //vector addition
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        //vector subtraction
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        //scalar multiplication
        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }

        //dot product (produces a scalar)
        //is mainly used to evaluate the directions between vectors
        //dot = 1   angle = 0 deg. (vectors face same direction)
        //dot > 0   angle < 90 deg.
        //dot = 0   angle = 90 deg. (vectors are perpendicular to each other)
        //dot < 0   angle > 90 deg.
        //dot = -1  angle = 180 deg. (vectors face opposite direction)
        public static float Dot(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        //cross product (produces normal vector, perpendicular to both vectors) 
        public static Vector Cross(Vector a, Vector b)
        {
            return new Vector(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }

        //normalization returns unit vector
        public Vector Normalized()
        {
            return new Vector(X / Magnitude, Y / Magnitude, Z / Magnitude);
        }

        public Vector Transform(Matrix4x4 matrix)
        {
            Vector4 oldT = new Vector4(X, Y, Z, 1f);
            Vector4 newT = TransformationMatrix.MultiplyByVector(matrix, oldT);

            return new Vector(newT.X / newT.W, newT.Y / newT.W, newT.Z / newT.W);
        }
    }
}
