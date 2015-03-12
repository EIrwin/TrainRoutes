using System.Collections.Generic;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class PathContext
    {
        public  LinkedList<GraphNode<string>> Visited { get; set; }

        public double Distance { get; set; }

        public PathContext()
        {
            Visited = new LinkedList<GraphNode<string>>();
            Distance = 0;
        }

    }
}