using System;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class RouteProvider
    {
        #region [Private Fields]

        private readonly Graph<string> _graph;

        private LinkedList<GraphNode<string>> _visited;

        private GraphNode<string> _start;
        private GraphNode<string> _end;

        private List<List<GraphNode<string>>> _paths;
        private List<List<PathContext>> _pathResults;

        private Func<List<GraphNode<string>>, bool> _predicate;

        #endregion

        #region [Constructors]
        public RouteProvider(Graph<string> graph)
        {
            _graph = graph;
        }
        #endregion

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

        public IEnumerable<List<GraphNode<string>>> CalculatePaths(GraphNode<string> startNode,GraphNode<string> endNode)
        {
            return CalculatePaths(startNode,endNode, (route) => true);
        }

        public IEnumerable<List<GraphNode<string>>> CalculatePaths(GraphNode<string> startNode,GraphNode<string> endNode,Func<List<GraphNode<string>>,bool> predicateFilter)
        {
            _paths = new List<List<GraphNode<string>>>();
            _predicate = predicateFilter;

            _start = startNode;
            _end = endNode;
            _visited = new LinkedList< GraphNode<string>>();
            _visited.AddFirst(_start);

            DepthFirst(_visited);

            return _paths.AsEnumerable();
        }

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

                    if (_predicate(visited.ToList()))
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


        #region [Helper Functions]

        private void PrintPath(IEnumerable<GraphNode<string>> visited)
        {
            foreach (GraphNode<string> node in visited)
            {
                Console.Write(node.Value);
                Console.Write(" -> ");
            }
            Console.WriteLine();
        }

        #endregion
    }
}