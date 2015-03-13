using System;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Graph;

namespace TrainRoutes.Service
{
    public class RouteProvider:IRouteProvider<string>
    {
        private readonly IGraph<string> _graph;
        private GraphNode<string> _start;
        private GraphNode<string> _end;
        private List<Route<string>> _routes;
        private Func<Route<string>, bool> _predicate;

        public RouteProvider(IGraph<string> graph)
        {
            _graph = graph;
        }
            
        public double CalculateDistance(string routeDefinition)
        {
            if (string.IsNullOrEmpty(routeDefinition))
                throw new ArgumentNullException("routeDefinition");

            if (routeDefinition.Length == 1)
                throw new InvalidOperationException("Invalid route definitions");

            int currentIndex = 0;
            int nextIndex = currentIndex + 1;
            char[] route = routeDefinition.ToArray();
            char current = route[currentIndex];
            char next = route[nextIndex];

            //This covers the edge case that
            //repeating route stops are provided before
            //the while(...) loop
            if (current == next)
                throw new InvalidOperationException("Invalid route definition");

            GraphNode<string> currentNode = _graph.Nodes.FindByValue(current.ToString());

            double distance = 0;

            while (nextIndex < route.Length)
            {
                //grab the next route item
                char destination = route[nextIndex];

                //use the next route item to retrieve the next node
                GraphNode<string> nextNode = currentNode.Neighbors.FirstOrDefault(p => p.Value == destination.ToString());

                //the 'next' does not exist in the 'current' adjacency list
                if (nextNode == null) throw new InvalidOperationException("NO SUCH ROUTE");

                //add distance between 'current' and 'next' to total distance
                var cost = currentNode.Costs[nextNode.Id];
                distance += cost;

                //'next' becomes 'current'
                currentNode = nextNode;

                currentIndex++;
                nextIndex++;
            }

            return distance;
        }

        public double CalculateDistance(string[] routeDefinition)
        {
            string result = string.Join(".", routeDefinition);
            return CalculateDistance(result);
        }

        public double CalculateDistance(GraphNode<string>[] routeDefinition)
        {
            string result = string.Join(".", routeDefinition.Select(p => p.Value).ToArray());
            return CalculateDistance(result);
        }

        public IEnumerable<Route<string>> GetRoutes(string start, string end)
        {
            return GetRoutes(start, end, (route) => true);
        }

        public IEnumerable<Route<string>> GetRoutes(string start, string end, Func<Route<string>, bool> predicate)
        {
            _start = _graph.Nodes.FirstOrDefault(p => p.Value == start);
            _end = _graph.Nodes.FirstOrDefault(p => p.Value == end);

            return GetRoutes(_start,_end, predicate);
        }

        public IEnumerable<Route<string>> GetRoutes(GraphNode<string> startNode, GraphNode<string> endNode)
        {
            return GetRoutes(startNode, endNode, (route) => true);
        }

        public IEnumerable<Route<string>> GetRoutes(GraphNode<string> startNode, GraphNode<string> endNode, Func<Route<string>, bool> predicate)
        {
            _start = null;
            _end = null;
            _predicate = null;
            _routes = new List<Route<string>>();

            _start = startNode;
            _end = endNode;
            _predicate = predicate;

            if (_start == null || _end == null) throw new Exception("NO SUCH ROUTE");

            Route<string> initial = new Route<string>();
            initial.Visited.AddFirst(_start);

            DepthFirstSearch(initial);

            return _routes;
        }

        private void DepthFirstSearch(Route<string> route)
        {
            var neighbors = route.Visited.Last.Value.Neighbors.ToList();

            //examine adjacent nodes
            foreach (var neighbor in neighbors)
            {
                //check if this is the end node
                if (neighbor.Equals(_end))
                {
                    //calculate distance from current node to 
                    //neighbor and update the distance on path
                    var distance = route.Visited.Last.Value.Costs[neighbor.Id];
                    route.Distance += distance;
                    route.Visited.AddLast(neighbor);

                    if (_predicate(route))
                        _routes.Add(new Route<string>() { Visited = route.Visited, Distance = route.Distance });

                    //we need to remove recently added
                    //node and distance as we backtrack
                    route.Visited.RemoveLast();
                    route.Distance -= distance;
                    break;
                }
            }

            foreach (var neighbor in neighbors)
            {
                if (route.Visited.Contains(neighbor) ||
                    neighbor.Equals(_end))
                    continue;

                //calculate distance from current node to 
                //neighbor and update the distance on path
                var distance = route.Visited.Last.Value.Costs[neighbor.Id];
                route.Distance += distance;
                route.Visited.AddLast(neighbor);

                //recursively search updated path
                DepthFirstSearch(route);

                //we need to remove recently added
                //node and distance as we backtrack
                route.Visited.RemoveLast();
                route.Distance -= distance;
            }
        }
    }
}