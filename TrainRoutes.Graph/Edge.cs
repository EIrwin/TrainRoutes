namespace TrainRoutes.Graph
{
    public class Edge<TValue>
    {
        public GraphNode<TValue> Start { get; set; }

        public GraphNode<TValue> End { get; set; }

        public bool IsDirected { get; set; }

        public double Cost { get; set; }

        public Edge(GraphNode<TValue> start, GraphNode<TValue> end,bool isDirected)
        {
            Start = start;
            End = end;
            IsDirected = isDirected;
        }

        public Edge(GraphNode<TValue> start, GraphNode<TValue> end, bool isDirected, double cost)
        {
            Start = start;
            End = end;
            IsDirected = isDirected;
            Cost = cost;
        }

    }
}
