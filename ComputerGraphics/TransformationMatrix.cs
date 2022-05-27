using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ComputerGraphics
{
    public static class TransformationMatrix
    {
        public static Matrix4x4 CreateTransformationMatrix(
            float scaleX, float scaleY, float scaleZ,
            float rotateX, float rotateY, float rotateZ,
            float translateX, float translateY, float translateZ)
        {
            Matrix4x4 S = ScaleTransform(scaleX, scaleY, scaleZ);
            Matrix4x4 R = RotateTransform(rotateX, rotateY, rotateZ);
            Matrix4x4 T = TranslateTransform(translateX, translateY, translateZ);

            return Matrix4x4.Multiply(Matrix4x4.Multiply(S, R), T);
        }

        public static Matrix4x4 ScaleTransform(float x, float y, float z)
        {
            return new Matrix4x4(
                x, 0, 0, 0,
                0, y, 0, 0,
                0, 0, z, 0,
                0, 0, 0, 1);
        }

        public static Matrix4x4 TranslateTransform(float x, float y, float z)
        {
            return new Matrix4x4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1);
        }

        public static Matrix4x4 RotateTransform(float xDeg, float yDeg, float zDeg)
        {
            float degToRad = (float)Math.PI / 180;

            Matrix4x4 xMatrix = RotateAroundX(xDeg * degToRad);
            Matrix4x4 yMatrix = RotateAroundY(yDeg * degToRad);
            Matrix4x4 zMatrix = RotateAroundZ(zDeg * degToRad);

            return Matrix4x4.Multiply(Matrix4x4.Multiply(xMatrix, yMatrix), zMatrix);
        }

        public static Matrix4x4 RotateAroundX(float rad)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix.M22 = (float)Math.Cos(rad);
            matrix.M23 = (float)Math.Sin(rad);
            matrix.M32 = (float)-(Math.Sin(rad));
            matrix.M33 = (float)Math.Cos(rad);

            return matrix;
        }

        public static Matrix4x4 RotateAroundY(float rad)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix.M11 = (float)Math.Cos(rad);
            matrix.M13 = (float)-(Math.Sin(rad));
            matrix.M31 = (float)Math.Sin(rad);
            matrix.M33 = (float)Math.Cos(rad);

            return matrix;
        }

        public static Matrix4x4 RotateAroundZ(float rad)
        {
            Matrix4x4 matrix = Matrix4x4.Identity;

            matrix.M11 = (float)Math.Cos(rad);
            matrix.M12 = (float)Math.Sin(rad);
            matrix.M21 = (float)-(Math.Sin(rad));
            matrix.M22 = (float)Math.Cos(rad);

            return matrix;
        }

        public static Vector4 MultiplyByVector(Matrix4x4 m, Vector4 v)
        {
            return new Vector4
            {
                X = m.M11 * v.X + m.M12 * v.Y + m.M13 * v.Z + m.M14 * v.W,
                Y = m.M21 * v.X + m.M22 * v.Y + m.M23 * v.Z + m.M24 * v.W,
                Z = m.M31 * v.X + m.M32 * v.Y + m.M33 * v.Z + m.M34 * v.W,
                W = m.M41 * v.X + m.M42 * v.Y + m.M43 * v.Z + m.M44 * v.W
            };
        }
    }
}
