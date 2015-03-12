using System;
using System.Collections.Generic;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class PathContext
    {
        public  LinkedList<GraphNode<string>> Visited { get; set; }

        public double Distance { get; set; }

        public Guid Id { get; private set; }
        public PathContext()
        {
            Id = Guid.NewGuid(); //debug only
            Visited = new LinkedList<GraphNode<string>>();
            Distance = 0;
        }

    }
}