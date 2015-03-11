using System.Collections.Generic;
using TrainRoutes.Graph;

namespace TrainRoutes
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Graph<string> cities = new Graph<string>();

            GraphNode<string> a = new GraphNode<string>("A");
            GraphNode<string> b = new GraphNode<string>("B");
            GraphNode<string> c = new GraphNode<string>("C");
            GraphNode<string> d = new GraphNode<string>("D");
            GraphNode<string> e = new GraphNode<string>("E");

            cities.AddDirectedEdge(a, b, 5);
            cities.AddDirectedEdge(b, c, 4);
            cities.AddDirectedEdge(c, d, 8);
            cities.AddDirectedEdge(d, c, 8);
            cities.AddDirectedEdge(d, e, 6);
            cities.AddDirectedEdge(a, d, 5);
            cities.AddDirectedEdge(c, e, 2);
            cities.AddDirectedEdge(e, b, 3);
            cities.AddDirectedEdge(a, e, 7);

            RouteProvider routeProvider = new RouteProvider(cities);

            routeProvider.CalculateDistanceBetweenRoutes("");


        }
    }
}
