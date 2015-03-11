using System;
using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public class ListGraph<T>: Graph<T>
    {
        public ListGraph(bool isDirected) : base(isDirected)
        {

        }

        public ListGraph(bool isDirected, List<Node<T>> nodes) : base(isDirected, nodes)
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