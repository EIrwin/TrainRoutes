using System;
using System.Collections.Generic;
using TrainRoutes.Graph;

namespace TrainRoutes.Service
{
    public class RouteProvider:IRouteProvider<string>
    {
        private readonly IGraph<string> _graph;

        public RouteProvider(IGraph<string> graph)
        {
            _graph = graph;
        }

        public double CalculateDistance(string route)
        {
            throw new NotImplementedException();
        }

        public double CalculateDistance(string[] route)
        {
            throw new NotImplementedException();
        }

        public double CalculateDistance(GraphNode<string>[] route)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route<string>> GetPaths(string start, string end)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route<string>> GetPaths(string start, string end, Func<Route<string>, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route<string>> GetPaths(GraphNode<string> startNode, GraphNode<string> endNode)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Route<string>> GetPaths(GraphNode<string> startNode, GraphNode<string> endNode, Func<Route<string>, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}