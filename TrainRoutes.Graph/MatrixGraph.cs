using System;

namespace TrainRoutes.Graph
{
    public class MatrixGraph<T>:Graph<T>
    {
        public MatrixGraph(bool isDirected) : base(isDirected)
        {

        }

        public override void Insert(Edge edge)
        {
            throw new NotImplementedException();
        }

        public override bool EdgeExists(Node<T> source, Node<T> destination)
        {
            throw new NotImplementedException();
        }

        public override Edge GetEdge(Node<T> source, Node<T> destination)
        {
            throw new NotImplementedException();
        }
    }
}