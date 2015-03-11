using System;
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

        /* Calcualte the distance between two routes */
        public double CalculateDistanceBetweenRoutes(string routeDefinition)
        {
            if (routeDefinition == null)
                throw new ArgumentNullException("routeDefinition");

            if (routeDefinition.Length <= 2)
                throw new InvalidOperationException("Route length must be specified");

            char[] route = routeDefinition.ToCharArray();

            string source = GetSource(route);
            string destination = GetDestination(route);
            double distance = GetDistance(route);

            var sourceNode = _graph.Nodes.FindByValue(source);
            

            return 0;
        }

        private string GetSource(char[] route)
        {
            string source = route[0].ToString();

            if (!_graph.Contains(source))
                throw new InvalidOperationException();

            return source;
        }

        private string GetDestination(char[] route)
        {
            string destination = route[1].ToString();

            if (_graph.Contains(destination))
                throw new InvalidOperationException();

            return destination;
        }

        private double GetDistance(char[] route)
        {
            double distance;

            if (!double.TryParse(route[2].ToString(), out distance))
                throw new FormatException("invalid distance format");

            return distance;
        }
    }
}