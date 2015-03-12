using System;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class Program
    {
        private static RouteProvider _routeProvider;
        
        public static void Main(string[] args)
        {
            Graph<string> cities = new Graph<string>();

            GraphNode<string> a = new GraphNode<string>("A");
            GraphNode<string> b = new GraphNode<string>("B");
            GraphNode<string> c = new GraphNode<string>("C");
            GraphNode<string> d = new GraphNode<string>("D");
            GraphNode<string> e = new GraphNode<string>("E");

            cities.AddNode(a);
            cities.AddNode(b);
            cities.AddNode(c);
            cities.AddNode(d);
            cities.AddNode(e);

            cities.AddDirectedEdge(a, b, 5);
            cities.AddDirectedEdge(b, c, 4);
            cities.AddDirectedEdge(c, d, 8);
            cities.AddDirectedEdge(d, c, 8);
            cities.AddDirectedEdge(d, e, 6);
            cities.AddDirectedEdge(a, d, 5);
            cities.AddDirectedEdge(c, e, 2);
            cities.AddDirectedEdge(e, b, 3);
            cities.AddDirectedEdge(a, e, 7);

            _routeProvider = new RouteProvider(cities);

            CalculateDistanceUsingRouteDefinition("ABC");
            CalculateDistanceUsingRouteDefinition("AD");
            CalculateDistanceUsingRouteDefinition("ADC");
            CalculateDistanceUsingRouteDefinition("AEBCD");
            CalculateDistanceUsingRouteDefinition("AED");

            //We want to reduce the route.Count by 1 because
            //we want to use the number of stops and not nodes.
            //C -> D -> C has three total nodes, but only two stops
            CalculateNumberOfTrips("C", "C", (route) => route.Count - 1 <= 3);
            CalculateNumberOfTrips("A", "C",(route) => route.Count - 1 == 4);

            Console.ReadLine();
        }

        private static void CalculateDistanceUsingRouteDefinition(string input)
        {
            try
            {
                double routeDistance = _routeProvider.CalculateRouteDistance(input);

                Console.WriteLine(routeDistance);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CalculateNumberOfTrips(string start, string end,Func<List<GraphNode<string>>,bool> predicateFilter)
        {
            var paths = _routeProvider.CalculatePaths(start, end, predicateFilter);
            Console.WriteLine(paths.Count());
        }

        private static void CalculateShortestRoute(string start, string end)
        {
            
        }

    }
}
