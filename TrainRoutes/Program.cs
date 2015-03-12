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

            #region [Problems 1-5]
            CalculateDistanceUsingRouteDefinition("ABC");
            CalculateDistanceUsingRouteDefinition("AD");
            CalculateDistanceUsingRouteDefinition("ADC");
            CalculateDistanceUsingRouteDefinition("AEBCD");
            CalculateDistanceUsingRouteDefinition("AED");

            #endregion

            #region [Problems 6-7]

            //We want to reduce the route.Count by 1 because
            //we want to use the number of stops and not nodes.
            //C -> D -> C has three total nodes, but only two stops
            CalculateNumberOfTrips(c, c, (path) => path.Visited.Count - 1 <= 3);
            CalculateNumberOfTrips(a, c, (path) => path.Visited.Count - 1 == 4);
            
            #endregion

            #region [Problems 8-9]

            CalculateShortestRoute(b,b,(path)=> true);

            #endregion

            #region [Problem 10]

            var sampleData = new[]
                {
                    "CDC",
                    "CEBC",
                    "CEBCDC",
                    "CDCEBC",
                    "CDEBC",
                    "CEBCEBC",
                    "CEBCEBCEBC"
                };

            //CalculateNumberOfDifferentRoutes(sampleData, (path) => path.Distance < 30);

            #endregion

            Console.ReadLine();
        }

        #region [Problems 1-5]

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

        #endregion

        #region [Problems 6-7]

        private static void CalculateNumberOfTrips(GraphNode<string> start,GraphNode<string> end,Func<Path,bool> predicate)
        {
            var numberOfTrips = _routeProvider.CalculatePaths(start, end, predicate).Count;
            Console.WriteLine(numberOfTrips);
        }
        #endregion

        #region [Problems 8-9]

        private static void CalculateShortestRoute(GraphNode<string> startNode, GraphNode<string> endNode,Func<Path,bool> predicate)
        {
            var shortestRoute = _routeProvider
                .CalculatePaths(startNode, endNode,predicate)
                .OrderBy(p => p.Distance)
                .FirstOrDefault();
            
            Console.WriteLine("Shortest Route from {0} to {1} = {2}", startNode.Value, endNode.Value,
                shortestRoute != null ? shortestRoute.Distance.ToString() : "Error");
        }

        #endregion

        #region [Problems 10]

        
        private static void CalculateNumberOfDifferentRoutes(IEnumerable<string> inputData, Func<Path,bool> predicate)
        {
            //foreach (var routeDefinition in inputData)
            //{
            //    double distance = _routeProvider.CalculateShortestRoute(routeDefinition,predicate);
            //    Console.WriteLine(distance);
            //}
        }


        #endregion


    }
}
