using System.Collections.Generic;
using TrainRoutes.Graph;

namespace TrainRoutes.Service
{
    public class Route<TValue>
    {
        public  LinkedList<GraphNode<TValue>> Visited { get; set; }

        public double Distance { get; set; }

        public Route()
        {
            Visited = new LinkedList<GraphNode<TValue>>();
            Distance = 0;
        }

    }
}