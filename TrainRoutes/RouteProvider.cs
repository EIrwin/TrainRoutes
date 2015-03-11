using System;
using System.Linq;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class RouteProvider
    {
        private Graph<string> graph;

        public RouteProvider(Graph<string> graph)
        {
            graph = graph;
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

            return 0;
        }

        /* Calcualte the distance of the given route */
        public double CalculateDistanceFromRoute(string[] route)
        {
            return 0;
        }

        /*Calculate the number of trips given the start and end cities*/
        public int CalculteNumberOfTrips(string start, string end)
        {
            return 1;
        }

        /* Calcualte the number of trips given the start and end cities
         *that are equal to or greater than the minimum amount of stops provided
         *and less than or equal to the maximum amount of stops provided.*/
        public int CalculateNumberOfTrips(string start, string end, int minStops, int maxStops)
        {
            return 1;
        }

        /* Calculate the length of the shortest route between two cities*/
        public double CalculateShortestRoute(string start, string end)
        {
            return 1;
        }
        /* Calculate the number of possible routes between two given cities*/
        public int CalculateNumberOfRoutes(string start, string end)
        {
            return 1;
        }

        /*Calculate the number of routes between two given cities that are greater than
         *or equal to the minimum distance given and less than or equal to the max distance given*/
        public int CalculateNumberOfRoutes(string start, string end, double minDistance, double maxDistance)
        {
            return 1;
        }

        private string GetSource(char[] route)
        {
            string source = route[0].ToString();

            if (!graph.Contains(source))
                throw new InvalidOperationException();

            return source;
        }

        private string GetDestination(char[] route)
        {
            string destination = route[1].ToString();

            if (graph.Contains(destination))
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