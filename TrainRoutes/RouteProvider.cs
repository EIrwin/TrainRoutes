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
        private GraphNode<string> _start;
        private GraphNode<string> _end;
        private List<Path> _pathResults;
        private Func<Path,bool> _predicateFilter;

        #endregion

        #region [Constructors]
        public RouteProvider(Graph<string> graph)
        {
            _graph = graph;
        }
        #endregion

        #region [Public Methods]

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
                var cost = sourceNode.Costs[destinationNode.Id];
                distance += cost;

                //'destination' becomes source'
                sourceNode = destinationNode;

                sourceIndex++;
                destinationIndex++;
            }

            return distance;
        }
        
        /// <summary>
        /// Calculate all possible paths between two nodes
        /// </summary>
        /// <param name="startNode">Node to start graph traversal on</param>
        /// <param name="endNode">Node to finish graph traversal on</param>
        /// <returns></returns>
        public List<Path> CalculatePaths(GraphNode<string> startNode, GraphNode<string> endNode)
        {
            return CalculatePaths(startNode, endNode, (path) => true);
        }

        /// <summary>
        /// Calculate all possible paths between two nodes
        /// using a predicate to short circuit the results
        /// </summary>
        /// <param name="startNode">Node to start graph traversal on</param>
        /// <param name="endNode">Node to finish graph traversal on</param>
        /// <param name="predicate">Predicate to run against each node to short circuit traversal</param>
        /// <returns></returns>
        public List<Path> CalculatePaths(GraphNode<string> startNode, GraphNode<string> endNode,Func<Path, bool> predicate)
        {
            _pathResults = new List<Path>();
            _predicateFilter = predicate;

            _start = startNode;
            _end = endNode;

            Path initialRoute = new Path();

            initialRoute.Visited.AddFirst(_start);

            DepthFirstSearch(initialRoute);

            return _pathResults;
        }

        #endregion

        #region [Private/Protected Methods]

        private void DepthFirstSearch(Path path)
        {
            var neighbors = path.Visited.Last.Value.Neighbors.ToList();

            //examine adjacent nodes
            foreach (var neighbor in neighbors)
            {
                //check if this is the end node
                if (neighbor.Equals(_end))
                {
                    var distance = path.Visited.Last.Value.Costs[neighbor.Id];
                    path.Distance += distance;
                    path.Visited.AddLast(neighbor);

                    if (_predicateFilter(path))
                        _pathResults.Add(new Path() {Visited = path.Visited, Distance = path.Distance});
                    

                    path.Visited.RemoveLast();
                    path.Distance -= distance;
                    break;
                }
            }

            foreach (var neighbor in neighbors)
            {
                if (path.Visited.Contains(neighbor) ||
                    neighbor.Equals(_end))
                    continue;

                var distance = path.Visited.Last.Value.Costs[neighbor.Id];
                path.Distance += distance;
                path.Visited.AddLast(neighbor);
                DepthFirstSearch(path);
                path.Visited.RemoveLast();
                path.Distance -= distance;
            }
        }

        #endregion
    }

    public interface IRouteProvider
    {
        
    }
}