using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompGraph_1.Figures
{
    class Edge
    {
        private Vertex vertex2;
        private Vertex vertex1;

        public Edge (Vertex v1, Vertex v2)
        {
            vertex1 = v1;
            vertex2 = v2;
        }

        internal Vertex Vertex1 { get => vertex1; set => vertex1 = value; }
        internal Vertex Vertex2 { get => vertex2; set => vertex2 = value; }
    }
}
