using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;

namespace BvhConverter.Stuff
{
    public static class MathHelper
    {
        public static Matrix4 GetRotationMatrixY(double angle)
        {
            if (angle == 0.0)
            {
                return Matrix4.Identity;
            }

            var sin = (float)Math.Sin(angle);
            var cos = (float)Math.Cos(angle);
            
            return new Matrix4()
                {
                    M11 = cos,
                    M12 = 0,
                    M13 = sin,
                    M14 = 0,
                    M21 = 0,
                    M22 = 1,
                    M23 = 0,
                    M24 = 0,
                    M31 = -sin,
                    M32 = 0,
                    M33 = cos,
                    M34 = 0,
                    M41 = 0,
                    M42 = 0,
                    M43 = 0,
                    M44 = 1
                };
        }

        public static Matrix4 Multiplication(Matrix4 a, Matrix4 b)
        {
            var mat_a = ConvertToArray(a);
            var mat_b = ConvertToArray(a);

            return ConvertToMatrix4(Multiplication(mat_a, mat_b));
        }

        private static Matrix4 ConvertToMatrix4(float[,] p)
        {
            return new Matrix4()
                {
                    M11 = p[0, 0],
                    M12 = p[0, 1],
                    M13 = p[0, 2],
                    M14 = p[0, 3],

                    M21 = p[1, 0],
                    M22 = p[1, 1],
                    M23 = p[1, 2],
                    M24 = p[1, 3],

                    M31 = p[2, 0],
                    M32 = p[2, 1],
                    M33 = p[2, 2],
                    M34 = p[2, 3],

                    M41 = p[3, 0],
                    M42 = p[3, 1],
                    M43 = p[3, 2],
                    M44 = p[3, 3]
                };
        }

        private static float[,] ConvertToArray(Matrix4 a)
        {
            return new float[,]
                {
                    {a.M11,a.M12,a.M13,a.M14},
                    {a.M21,a.M22,a.M23,a.M24},
                    {a.M31,a.M32,a.M33,a.M34},
                    {a.M41,a.M42,a.M43,a.M44}
                };
        }

        private static float[,] Multiplication(float[,] a, float[,] b)
        {
            if (a.GetLength(1) != b.GetLength(0)) throw new Exception("Матрицы нельзя перемножить");
            
            var r = new float[a.GetLength(0), b.GetLength(1)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }

            return r;
        }

        public static Vector4 GetVectorFromArray(float[] a)
        {
            if(a.Length < 0)
                throw new Exception("Размерность вектора должна быть не менее 3-х");

            return new Vector4() {X = a[0], Y = a[1], Z = a[2], W = 0};
        }

        /// <summary>
        /// Вычисление углов эйлера для последовательности ZXY
        /// </summary>
        /// <param name="mat"></param>
        /// <returns></returns>
        public static EilerAngle GetElersAnglesZXY(Matrix4 mat, Matrix4? mat2 = null)
        {
            double angleX = 0;
            double angleZ = 0;
            double angleY = 0;

            double angleX2 = 0;
            double angleZ2 = 0;
            double angleY2 = 0;

            //Алгоритм в письме Золотых - отличается от моего транспонированной матрицей
            angleZ = (Math.Atan2(-mat.M21, mat.M22) * 180) / Math.PI;
            angleX = (Math.Asin(mat.M23) * 180) / Math.PI; ;
            angleY = (Math.Atan2(-mat.M13, mat.M33) * 180) / Math.PI;

            //Алгоритм в письме Золотых - отличается от моего транспонированной матрицей
            angleZ2 = (Math.Atan2(-mat.M21, mat.M22) * 180) / Math.PI;
            angleX2 = (Math.Asin(mat.M23) * 180) / Math.PI; ;
            angleY2 = (Math.Atan2(-mat.M13, mat.M33) * 180) / Math.PI;

            if(mat2 != null)
                return new EilerAngle((angleX + angleX2) / 2, (angleY + angleY2) / 2, (angleZ + angleZ2) / 2);
            else
                return new EilerAngle(angleX, angleY, angleZ);
        }
    }
}
