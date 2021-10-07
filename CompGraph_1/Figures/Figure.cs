using CompGraph_1.Matrix;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompGraph_1.Figures
{
    public class Figure
    {

        /*
         * Немного теории:
         * 1) Фигура представляет собой набор вершин и ребер, что их соединяют, координаты точек и вершин входящие в ребра берутся ИЗ ФАЙЛА
         * 2) После формирования вершин и ребер формируем матрицу по принципу:
         *      [вершина1.x    вершина1.y   вершина1.z   1]
         *      [вершина2.x    вершина2.y   вершина2.z   1]
         *      ...
         *      [вершинаN.x    вершинаN.y   вершинаN.z   1]             
         *      где N - число вершин
         *      => матрица имеет след. размеры [N][4]
         * 3) Перемножаем данную матрицу на любую другую, например на матрицу поворота, для совершения поворота
         *  (для смены направления дать угол со знаком "-")
         * 4) переписываем старые вершины на новые (из матрицы в список вершин) и перерисовываем
         *  (это делается для того, что-бы нормально перерисовать ребра)
         */

        private List<Edge> edges; // ребра
        private List<Vertex> vertex; // вершины
        private double[][] matrix; // матрица для перемножений
        private Pen pen = new Pen(Color.Black, 1);
        private const int START_X = 400, START_Y = 400, SIZE_COEF = 25;

        /// <summary>
        /// Ссаный конструктор
        /// </summary>
        /// <param name="filename"></param>
        public Figure(string filename)
        {
            vertex = new List<Vertex>();
            edges = new List<Edge>();
            StreamReader reader = new StreamReader(filename);
            string readLine = reader.ReadLine();
            string[] strings;
            //
            // Вершины
            //
            while (readLine != "EDGE")
            {

                strings = readLine.Split(" ");
                vertex.Add(new Vertex(Convert.ToDouble(strings[0]) * -SIZE_COEF, Convert.ToDouble(strings[1]) * SIZE_COEF, Convert.ToDouble(strings[2]) * SIZE_COEF));
                readLine = reader.ReadLine();
            }
            readLine = reader.ReadLine();
            //
            // Ребра
            //
            while (readLine != "/EDGE")
            {
                strings = readLine.Split(" ");
                edges.Add(new Edge(vertex[Convert.ToInt32(strings[0])], vertex[Convert.ToInt32(strings[1])]));
                readLine = reader.ReadLine();
            }

            matrix = MatrixOperations.Init(vertex.Count, 4);
            for (int i = 0; i < vertex.Count; i++)
            {
                matrix[i][0] = vertex[i].X;
                matrix[i][1] = vertex[i].Y;
                matrix[i][2] = vertex[i].Z;
                matrix[i][3] = 1;
            }
            RewriteVertex();
        }

        /// <summary>
        /// Переписать вершины
        /// </summary>
        private void RewriteVertex()
        {
            double[][] proection = MatrixOperations.Mul(matrix, MatrixOperations.Graph3D.Proection(135));           
            for (int i = 0; i < vertex.Count; i++)
            {
                vertex[i].X = proection[i][0];
                vertex[i].Y = proection[i][1];
            }
        }

        /// <summary>
        /// Рисование
        /// </summary>
        public void Draw(Graphics canvas)
        {
            Point p1 = new();
            Point p2 = new();
            for (int i = 0; i < edges.Count; i++)
            {
                p1.X = START_X - (int)edges[i].Vertex1.X;
                p1.Y = START_Y - (int)edges[i].Vertex1.Y;
                p2.X = START_X - (int)edges[i].Vertex2.X;
                p2.Y = START_Y - (int)edges[i].Vertex2.Y;
                canvas.DrawLine(pen, p1, p2);
            }
        }

        /// <summary>
        /// Поворот фигуры вправо
        /// </summary>
        /// <param name="angle"></param>
        public void RotationX(double angle)
        {
            matrix = MatrixOperations.Mul(matrix, MatrixOperations.Graph3D.RotationX(angle));
            RewriteVertex();
        }

        /// <summary>
        /// Поворот фигуры снизу вверх
        /// </summary>
        /// <param name="angle"></param>
        public void RotationY(double angle)
        {
            matrix = MatrixOperations.Mul(matrix, MatrixOperations.Graph3D.RotationY(angle));
            RewriteVertex();
        }

        /// <summary>
        /// Наклон фигуры вправо
        /// </summary>
        /// <param name="angle"></param>
        public void RotationZ(double angle)
        {
            matrix = MatrixOperations.Mul(matrix, MatrixOperations.Graph3D.RotationZ(angle));
            RewriteVertex();
        }

        /// <summary>
        /// Перемещение фигуры в пространстве
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Moving(double x, double y, double z)
        {
            matrix = MatrixOperations.Mul(matrix, MatrixOperations.Graph3D.Moving(x, y, z));
            RewriteVertex();
        }

        /// <summary>
        /// Изменение размеров фигуры
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        public void Resize(double x, double y, double z)
        {
            matrix = MatrixOperations.Mul(matrix, MatrixOperations.Graph3D.Resize(x, y, z));
            RewriteVertex();
        }
    }
}
