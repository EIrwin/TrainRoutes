namespace TrainRoutes.Graph
{
    public class WeightedEdge:Edge
    {
        public double Weight { get; set; }

        public WeightedEdge(Node source, Node destination,double weight) : base(source, destination)
        {
            Weight = weight;
        }
    }
}