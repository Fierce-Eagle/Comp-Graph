using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraph_1.Matrix
{
    static class MatrixOperations
    {
        public static double[][] Init(int rowsCount, int colsCount)
        {
            double[][] matrix = new double[rowsCount][];
            for (int i = 0; i < rowsCount; i++)
            {
                matrix[i] = new double[colsCount];
            }
            return matrix;
        }
                
        public static double[][] Mul(double[][] matrixA, double[][] matrixB)
        {
            double sum;
            double[][] result = Init(matrixA.GetLength(0), matrixB[0].Length);
            for (int i = 0; i < result.GetLength(0); i++)
                for (int j = 0; j < result[0].Length; j++)
                {
                    sum = 0;
                    for (int k = 0; k < matrixB.GetLength(0); k++)
                        sum += matrixA[i][k] * matrixB[k][j];
                    result[i][j] = sum;
                }
            return result;
        }
        
        public static double[][] IdentityMatrix(int size)
        {
            double[][] matrix = Init(size, size);
            for (int i = 0; i < size; i++)
            {
                matrix[i][i] = 1;
            }
            return matrix;
        }
   
        public static class Graph3D
        {
            private static double AngleToRadian(double angle)
            {
                return angle * Math.PI / 180;            
            }
            public static double[][] RotationX(double angle)
            {
                angle = AngleToRadian(angle);
                double[][] matrix = Init(4, 4);
                matrix[0][0] = matrix[3][3] = 1;
                matrix[1][1] = matrix[2][2] = Math.Cos(angle);
                matrix[1][2] = Math.Sin(angle);
                matrix[2][1] = -Math.Sin(angle);
                return matrix;
            }
            public static double[][] RotationY(double angle)
            {
                angle = AngleToRadian(angle);
                double[][] matrix = Init(4, 4);
                matrix[1][1] = matrix[3][3] = 1;
                matrix[0][0] = matrix[2][2] = Math.Cos(angle);
                matrix[2][0] = Math.Sin(angle);
                matrix[0][2] = -Math.Sin(angle);
                return matrix;
            }
            public static double[][] RotationZ(double angle)
            {
                angle = AngleToRadian(angle);
                double[][] matrix = Init(4, 4);
                matrix[2][2] = matrix[3][3] = 1;
                matrix[0][0] = matrix[1][1] = Math.Cos(angle);
                matrix[0][1] = Math.Sin(angle);
                matrix[1][0] = -Math.Sin(angle);
                return matrix;
            }
            public static double[][] Resize(double x, double y, double z)
            {
                double[][] matrix = IdentityMatrix(4);
                matrix[0][0] = x;
                matrix[1][1] = y;
                matrix[2][2] = z;
                return matrix;
            }
            public static double[][] Moving(double x, double y, double z)
            {
                double[][] matrix = IdentityMatrix(4);
                matrix[3][0] = x;
                matrix[3][1] = y;
                matrix[3][2] = z;
                return matrix;
            }
            public static double[][] Proection(double angle)
            {
                angle = AngleToRadian(angle);
                double[][] matrix = IdentityMatrix(4);
                matrix[2][2] = 0;
                matrix[2][0] = -0.5 * Math.Cos(angle);
                matrix[2][1] = -0.5 * Math.Sin(angle);
                return matrix;
            }
        }
    }
}
