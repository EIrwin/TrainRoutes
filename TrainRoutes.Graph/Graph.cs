using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public abstract class Graph<T>:IGraph<T>
    {
        protected bool IsDirected { get; set; }
        protected List<Node<T>> Nodes { get; set; }

        protected Graph(bool isDirected)
        {
            IsDirected = isDirected;
            Nodes = new List<Node<T>>();
        }

        protected Graph(bool isDirected, List<Node<T>> nodes)
        {
            IsDirected = isDirected;
            Nodes = nodes ?? new List<Node<T>>();
        }

        public abstract void Insert(Edge edge);

        public abstract bool EdgeExists(Node<T> source, Node<T> destination);

        public abstract Edge GetEdge(Node<T> source, Node<T> destination);
    }
}