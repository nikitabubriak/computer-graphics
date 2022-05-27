using System.Numerics;

namespace ComputerGraphics
{
    public class Point
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        //point + vector allows to move from one point to another by the specified vector
        public static Point operator +(Point a, Vector b)
        {
            return new Point(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        //point - point returns their difference as a vector
        public static Vector operator -(Point a, Point b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public Point Transform(Matrix4x4 matrix)
        {
            Vector4 oldT = new Vector4(X, Y, Z, 1f);
            Vector4 newT = TransformationMatrix.MultiplyByVector(matrix, oldT);

            return new Point(newT.X / newT.W, newT.Y / newT.W, newT.Z / newT.W);
        }
    }
}
