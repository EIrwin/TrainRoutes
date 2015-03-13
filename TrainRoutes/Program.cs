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
            
            //initialize new ITrainService object
            //and inject graph and route provider
            IGraph<string> graph = new Graph<string>(true);
            graph.LoadFromFile("SampleGraphData.txt");
            IRouteProvider<string> routeProvider = new RouteProvider(graph);
            ITrainService trainservice = new TrainService(routeProvider);

            //initialize nodes
            //GraphNode<string> a = new GraphNode<string>("A");
            //GraphNode<string> b = new GraphNode<string>("B");
            //GraphNode<string> c = new GraphNode<string>("C");
            //GraphNode<string> d = new GraphNode<string>("D");
            //GraphNode<string> e = new GraphNode<string>("E");
            //graph.AddNode(a);
            //graph.AddNode(b);
            //graph.AddNode(c);
            //graph.AddNode(d);
            //graph.AddNode(e);

            ////initialize edges
            //graph.AddEdge(a, b, 5);
            //graph.AddEdge(b, c, 4);
            //graph.AddEdge(c, d, 8);
            //graph.AddEdge(d, c, 8);
            //graph.AddEdge(d, e, 6);
            //graph.AddEdge(a, d, 5);
            //graph.AddEdge(c, e, 2);
            //graph.AddEdge(e, b, 3);
            //graph.AddEdge(a, e, 7);


            #region [Problem 1]

            //#1 A->B->C
            double result1 = trainservice.CalculateDistance("ABC");
            Console.WriteLine("Output #1: {0}", result1);

            #endregion

            #region [Problem 2]

            //#2 A->D
            double result2 = trainservice.CalculateDistance("AD");
            Console.WriteLine("Output #2: {0}", result2);

            #endregion

            #region [Problem 3]

            //#3 A->D->C
            double result3 = trainservice.CalculateDistance("ADC");
            Console.WriteLine("Output #3: {0}", result3);

            #endregion

            #region [Problem 4]

            //#4 A->E->B->C->D
            double result4 = trainservice.CalculateDistance("AEBCD");
            Console.WriteLine("Output #4: {0}", result4);

            #endregion

            #region [Problem 5]

            //#5 A->E->D
            try
            {
                double result5 = trainservice.CalculateDistance("AED");

                //If we hit this, then this problem is wrong
                Console.WriteLine("Output #5: {0}", result5);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Output #5: {0}", exception.Message);
            }


            #endregion

            #region [Problem 6]

            //#6 # of Trips C->C, Max 3 stops
            int result6 = trainservice.CalculateNumberOfTrips("C", "C", 0, 3);
            Console.WriteLine("Output #6: {0}", result6);

            #endregion

            #region [Problem 7]

            //#7 # of Trips A->C, 4 stops
            double result7 = trainservice.CalculateNumberOfTrips("A", "C", 4, 4);
            Console.WriteLine("Output #7: {0}",result7);

            #endregion

            #region [Problem 8]

            //#8 length of shortest route A->C
            double result8 = trainservice.CalculateLengthOfShortestRoute("A", "C");
            Console.WriteLine("Output #8: {0}", result8);

            #endregion

            #region [Problem 9]

            //#9 length of shortest route B->B
            double result9 = trainservice.CalculateLengthOfShortestRoute("B", "B");
            Console.WriteLine("Output #9: {0}", result9);

            #endregion

            #region [Problem 10]

            List<string> sampleRoutes = new List<string>()
                {
                    "CDC",
                    "CEBC",
                    "CEBCDC",
                    "CDCEBC",
                    "CDEBC",
                    "CEBCEBC",
                    "CEBCEBCEBC"
                };

            int result10 = trainservice.CalculateNumberOfTrips(sampleRoutes, (distance) => distance < 30);
            Console.WriteLine("Option #10: {0}", result10);
            
            #endregion

            Console.ReadLine();
        }

    }
}
