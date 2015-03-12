using System;
using System.Collections.Generic;
using System.Linq;
using TrainRoutes.Graph;
using TrainRoutes.Service;

namespace TrainRoutes
{
    public class Program
    {
        
        public static void Main(string[] args)
        {

            IGraph<string> graph = new Graph<string>(true);
            IRouteProvider<string> routeProvider = new RouteProvider(graph);
            ITrainService trainservice = new TrainService(routeProvider);

            //initialize nodes
            GraphNode<string> a = new GraphNode<string>("A");
            GraphNode<string> b = new GraphNode<string>("B");
            GraphNode<string> c = new GraphNode<string>("C");
            GraphNode<string> d = new GraphNode<string>("D");
            GraphNode<string> e = new GraphNode<string>("E");


            //initialize edges
            graph.AddEdge(a, b, 5);
            graph.AddEdge(b, c, 4);
            graph.AddEdge(c, d, 8);
            graph.AddEdge(d, c, 8);
            graph.AddEdge(d, e, 6);
            graph.AddEdge(a, d, 5);
            graph.AddEdge(c, e, 2);
            graph.AddEdge(e, b, 3);
            graph.AddEdge(a, e, 7);

            
            
            
            

        }

        #region [Old Code]

        //private static ITrainService _trainService;

        //private static OldRouteProvider _oldRouteProvider;
        //private static Graph<string> _graph;
        //public static void Main(string[] args)
        //{
        //    Graph<string> cities = new Graph<string>(true);

        //    GraphNode<string> a = new GraphNode<string>("A");
        //    GraphNode<string> b = new GraphNode<string>("B");
        //    GraphNode<string> c = new GraphNode<string>("C");
        //    GraphNode<string> d = new GraphNode<string>("D");
        //    GraphNode<string> e = new GraphNode<string>("E");

        //    cities.AddNode(a);
        //    cities.AddNode(b);
        //    cities.AddNode(c);
        //    cities.AddNode(d);
        //    cities.AddNode(e);
            
        //    cities.AddEdge(a, b, 5);
        //    cities.AddEdge(b, c, 4);
        //    cities.AddEdge(c, d, 8);
        //    cities.AddEdge(d, c, 8);
        //    cities.AddEdge(d, e, 6);
        //    cities.AddEdge(a, d, 5);
        //    cities.AddEdge(c, e, 2);
        //    cities.AddEdge(e, b, 3);
        //    cities.AddEdge(a, e, 7);

        //    _oldRouteProvider = new OldRouteProvider(cities);

        //    #region [Problems 1-5]
        //    CalculateDistanceUsingRouteDefinition("ABC");
        //    CalculateDistanceUsingRouteDefinition("AD");
        //    CalculateDistanceUsingRouteDefinition("ADC");
        //    CalculateDistanceUsingRouteDefinition("AEBCD");
        //    CalculateDistanceUsingRouteDefinition("AED");

        //    #endregion

        //    #region [Problems 6-7]

        //    //We want to reduce the route.Count by 1 because
        //    //we want to use the number of stops and not nodes.
        //    //C -> D -> C has three total nodes, but only two stops
        //    CalculateNumberOfTrips(c, c, (path) => path.Visited.Count - 1 <= 3);
        //    CalculateNumberOfTrips(a, c, (path) => path.Visited.Count - 1 == 4);
            
        //    #endregion

        //    #region [Problems 8-9]

        //    //CalculateShortestRoute(b,b,(path)=> true);

        //    #endregion

        //    #region [Problem 10]

        //    var sampleData = new[]
        //        {
        //            "CDC",
        //            "CEBC",
        //            "CEBCDC",
        //            "CDCEBC",
        //            "CDEBC",
        //            "CEBCEBC",
        //            "CEBCEBCEBC"
        //        };

        //    CalculateNumberOfDifferentRoutes(sampleData, (path) => path.Distance < 30);

        //    #endregion

        //    Console.ReadLine();
        //}


        //#region [Problems 1-5]

        //private static void CalculateDistanceUsingRouteDefinition(string input)
        //{
        //    try
        //    {
        //        double routeDistance = _oldRouteProvider.CalculateRouteDistance(input);

        //        Console.WriteLine(routeDistance);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //}

        //#endregion

        //#region [Problems 6-7]

        //private static void CalculateNumberOfTrips(GraphNode<string> start,GraphNode<string> end,Func<Route,bool> predicate)
        //{
        //    var numberOfTrips = _oldRouteProvider.CalculatePaths(start, end, predicate).Count;
        //    Console.WriteLine(numberOfTrips);
        //}
        //#endregion

        //#region [Problems 8-9]

        //private static void CalculateShortestRoute(GraphNode<string> startNode, GraphNode<string> endNode,Func<Route,bool> predicate)
        //{
        //    var shortestRoute = _oldRouteProvider
        //        .CalculatePaths(startNode, endNode,predicate)
        //        .OrderBy(p => p.Distance)
        //        .FirstOrDefault();
            
        //    Console.WriteLine("Shortest Route from {0} to {1} = {2}", startNode.Value, endNode.Value,
        //        shortestRoute != null ? shortestRoute.Distance.ToString() : "Error");
        //}

        //#endregion

        //#region [Problems 10]

        
        //private static void CalculateNumberOfDifferentRoutes(IEnumerable<string> inputData, Func<Route,bool> predicate)
        //{
        //    foreach (var routeDefinition in inputData)
        //    {
        //        double distance = _oldRouteProvider.CalculateRouteDistance()
        //        Console.WriteLine(distance);
        //    }
        //}


        //#endregion

        #endregion

    }
}
