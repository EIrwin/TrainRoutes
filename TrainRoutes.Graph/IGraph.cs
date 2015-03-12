using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public interface IGraph<TValue> : IEnumerable<TValue>
    {
        void AddNode(GraphNode<TValue> node);

        void AddEdge(GraphNode<TValue> start, GraphNode<TValue> end);
        void AddEdge(GraphNode<TValue> start, GraphNode<TValue> end,double cost);

        bool Contains(TValue value);
        bool Remove(TValue value);

        NodeCollection<TValue> Nodes { get; }
        int Count { get; }
        bool IsDirected { get;}

        void Load(string path);
        void Load(IEnumerable<string> list);
        void Load(IEnumerable<GraphNode<TValue>> nodes, IEnumerable<Edge<TValue>> edges);
    }
}