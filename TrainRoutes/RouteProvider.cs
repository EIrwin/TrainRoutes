using System;using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class RouteProvider
    {
        private Graph<string> _graph;

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
        public int CalculateNumberOfTrips(string start,string end,int maxLength)
        {

            //the following will eventually be passed in
            GraphNode<string> startNode = _graph.Nodes.First(p => p.Value == start);
            GraphNode<string> endNode = _graph.Nodes.First(p => p.Value == end);

            _start = startNode;
            _end = endNode;
            _visited = new LinkedList<GraphNode<string>>();
            _visited.AddFirst(_start);

            BreadthFirstRecursive(_visited);
            //recursively find all simple paths between 'start' and 'end'

            return _paths.Count(p => p.Count <= maxLength + 1);
        }

        private readonly List<List<GraphNode<string>>> _paths = new List<List<GraphNode<string>>>();

        //TODO: Need to get this to stop running after it has reached max limit
        private void BreadthFirstRecursive(LinkedList<GraphNode<string>> visited)
        {
            var neighbors = visited.Last.Value.Neighbors;

            //examine adjacent nodes
            foreach (var neighbor in neighbors)
            {
                //check if this is the end node
                if (neighbor.Equals(_end))
                {
                    visited.AddLast(neighbor);
                    PrintPath(visited);
                    _paths.Add(visited.ToList());
                    visited.RemoveLast();
                    break;
                }

                //check to see if we have already
                //visited this neighbor or not
                if (visited.Contains(neighbor))
                    continue;
            }

            foreach (var neighbor in neighbors)
            {
                if (visited.Contains(neighbor) ||
                    neighbor.Equals(_end))
                    continue;

                visited.AddLast(neighbor);
                BreadthFirstRecursive(visited);
                visited.RemoveLast();
            }
        }

        private void DepthFirstIterative(GraphNode<string> start,GraphNode<string> endNode)
        {
            var visited = new LinkedList<GraphNode<string>>();
            var stack = new Stack<GraphNode<string>>();

            stack.Push(start);

            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (visited.Contains(current))
                    continue;

                visited.AddLast(current);

                var neighbours = current.Neighbors;

                foreach (var neighbour in neighbours)
                {
                    if (visited.Contains(neighbour))
                        continue;

                    if (neighbour.Equals(endNode))
                    {
                        visited.AddLast(neighbour);
                        PrintPath(visited);
                        visited.RemoveLast();
                        break;
                    }
                }

                bool isPushed = false;
                foreach (var neighbour in neighbours.Reverse())
                {
                    if (neighbour.Equals(endNode) || visited.Contains(neighbour) || stack.Contains(neighbour))
                    {
                        continue;
                    }

                    isPushed = true;
                    stack.Push(neighbour);
                }

                if (!isPushed)
                    visited.RemoveLast();
            }
        }

        private void PrintPath(LinkedList<GraphNode<string>> visited)
        {
            foreach (GraphNode<string> node in visited)
            {
                Console.Write(node.Value);
                Console.Write(" -> ");
            }
            Console.WriteLine();
        }
    }
}