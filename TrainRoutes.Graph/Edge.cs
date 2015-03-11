namespace TrainRoutes.Graph
{
    public class Edge
    {
        public Node Source { get; set; }

        public Node Destination { get; set; }

        public Edge(Node source, Node destination)
        {
            Source = source;
            Destination = destination;
        }
    }
}