using System;using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class RouteProvider
    {
        private readonly Graph<string> _graph;

        public RouteProvider(Graph<string> graph)
        {
            _graph = graph;
        }

        public double CalculateRouteDistance(string routeDefinition)
        {
            int sourceIndex = 0;
            int destinationIndex = sourceIndex + 1;
            char[] route = routeDefinition.ToArray();
            char source = route[sourceIndex];

            GraphNode<string> sourceNode = _graph.Nodes.FindByValue(source.ToString());

            double distance = 0;

            while (destinationIndex < route.Length)
            {
                //grab the next route item
                char destination = route[destinationIndex];

                //use the next route item to retrieve the destination node
                GraphNode<string> destinationNode = sourceNode.Neighbors.FirstOrDefault(p => p.Value == destination.ToString());

                //the 'destination' does not exist in the 'source' adjacency list
                if (destinationNode == null) throw new Exception("NO SUCH ROUTE");

                //add distance between 'source' and 'destination' to total distance
                var index = sourceNode.Neighbors.IndexOf(destinationNode);
                var cost = sourceNode.Costs[index];
                distance += cost;

                //'destination' becomes source'
                sourceNode = destinationNode;

                sourceIndex++;
                destinationIndex++;
            }

            return distance;
        }

        private LinkedList<GraphNode<string>> _visited;
        private GraphNode<string> _start;
        private GraphNode<string> _end;

        public IEnumerable<List<GraphNode<string>>> CalculatePaths(string start, string end)
        {
            return CalculatePaths(start, end, (route) => true);
        }
        public IEnumerable<List<GraphNode<string>>> CalculatePaths(string start,string end,Func<List<GraphNode<string>>,bool> predicateFilter)
        {
            _paths = new List<List<GraphNode<string>>>();
            _predicate = predicateFilter;

            //the following will eventually be passed in
            GraphNode<string> startNode = _graph.Nodes.First(p => p.Value == start);
            GraphNode<string> endNode = _graph.Nodes.First(p => p.Value == end);

            _start = startNode;
            _end = endNode;
            _visited = new LinkedList< GraphNode<string>>();
            _visited.AddFirst(_start);

            DepthFirst(_visited);

            //the following is not efficient
            //and will need to be changed eventually
            return _paths.AsEnumerable();
        }

        public IEnumerable<List<PathContext>> CalculatePathResults(string start, string end, Func<List<GraphNode<string>>, bool> predicateFilter)
        {
            _pathResults = new List<List<PathContext>>();
            _predicate = predicateFilter;

            //the following will eventually be passed in
            GraphNode<string> startNode = _graph.Nodes.First(p => p.Value == start);
            GraphNode<string> endNode = _graph.Nodes.First(p => p.Value == end);

            _start = startNode;
            _end = endNode;
            _visited = new LinkedList<GraphNode<string>>();
            _visited.AddFirst(_start);

            DepthFirst(_visited);

            //the following is not efficient
            //and will need to be changed eventually
            return _pathResults;
        }

        private List<List<GraphNode<string>>> _paths;
        private List<List<PathContext>> _pathResults;
        private Func<List<GraphNode<string>>, bool> _predicate;
        private void DepthFirst(LinkedList<GraphNode<string>> visited)
        {
            var neighbors = visited.Last.Value.Neighbors.ToList();

            //examine adjacent nodes
            foreach (var neighbor in neighbors)
            {
                //check if this is the end node
                if (neighbor.Equals(_end))
                {
                    visited.AddLast(neighbor);
                    PrintPath(visited);

                    if(_predicate(visited.ToList()))
                        _paths.Add(visited.ToList());

                    visited.RemoveLast();
                    break;
                }
            }

            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor) ||
                    neighbor.Equals(_end))
                    continue;

                visited.AddLast(neighbor);
                DepthFirst(visited);
                visited.RemoveLast();
            }
        }

        private void DepthFirst(PathContext context)
        {
            var neighbors = context.Visited.Last.Value.Neighbors.ToList();

            //examine adjacent nodes
            foreach (var neighbor in neighbors)
            {
                //check if this is the end node
                if (neighbor.Equals(_end))
                {
                    context.Visited.AddLast(neighbor);
                    PrintPath(context.Visited);

                    if (_predicate(context.Visited.ToList()))
                        _paths.Add(context.Visited.ToList());

                    context.Visited.RemoveLast();
                    break;
                }
            }

            foreach (var neighbor in neighbors)
            {
                if (context.Visited.Contains(neighbor) ||
                    neighbor.Equals(_end))
                    continue;

                context.Visited.AddLast(neighbor);
                DepthFirst(context.Visited);
                context.Visited.RemoveLast();
            }
        }
        private void PrintPath(IEnumerable<GraphNode<string>> visited)
        {
            foreach (GraphNode<string> node in visited)
            {
                Console.Write(node.Value);
                Console.Write(" -> ");
            }
            Console.WriteLine();
        }
    }

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