using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simple.Mocking;
using TrainRoutes.Graph;
using TrainRoutes.Service;

namespace TrainRoutes.Test
{
    [TestFixture]
    public class TrainServiceTests
    {
        //concrete implementations
        private IGraph<string> _graph;
        private ITrainService _trainService;

        //mock implementations
        private IRouteProvider<string> _mockRouteProvider;
            
        [SetUp]
        public void Setup()
        {
            //initialize test graph
            _graph = new Graph<string>();

            //initialize test nodes
            var a = new GraphNode<string>("A");
            var b = new GraphNode<string>("B");
            var c = new GraphNode<string>("C");
            var d = new GraphNode<string>("D");
            var e = new GraphNode<string>("E");

            //CA3,DA5,DC4,DB10,DE2,BA9,BD8,BE1

            _graph.AddNode(a);
            _graph.AddNode(b);
            _graph.AddNode(c);
            _graph.AddNode(d);
            _graph.AddNode(e);

            _graph.AddEdge(c, a, 3);
            _graph.AddEdge(d, a, 5);
            _graph.AddEdge(d, c, 4);
            _graph.AddEdge(d, b, 10);
            _graph.AddEdge(d, e, 2);
            _graph.AddEdge(b, a, 9);
            _graph.AddEdge(b, d, 8);
            _graph.AddEdge(b, e, 1);

            //create mock IRouteProvider<string> implementation
            _mockRouteProvider = Mock.Interface<IRouteProvider<string>>();

            //initialize ITrainService and inject mock provider
            _trainService = new TrainService(_mockRouteProvider);

        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if a collection of rempty route definitions is passed in
        /// to CalculateNumberOfTrips(..), that it will handle it correctly
        /// and return a result of 0.
        /// </summary>
        [Test]
        public void CalculateNumberOfTrips_EmptyList()
        {
            List<string> testDefinitions = new List<string>();
            int trips = _trainService.CalculateNumberOfTrips(testDefinitions, (route) => true);
            Assert.IsTrue(trips == 0);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if an unknown node is passed into start argument
        /// for CalculateNumberOfTrips(start,end,min,max) that it
        /// will handle it correctly and return a result of 0
        /// </summary>
        [Test]
        public void CalculateNumberOfTrips_UnknownStartNode()
        {
            //sample start and end with invalid start node
            string start = "R";
            string end = "A";

            //Mock call to IRouteProvider.GetRoute(string,string,predicate)
            Expect.MethodCall(
                () =>
                _mockRouteProvider.GetRoutes(Any<string>.Value, Any<string>.Value, Any<Func<Route<string>, bool>>.Value)).Returns(new List<Route<string>>());

            int trips = _trainService.CalculateNumberOfTrips(start, end, 0, 4);

            Assert.IsTrue(trips == 0);
        }

        /// <summary>
        /// The purpose of this test is to assert that if a 
        /// minimum value greater than the maximum value
        /// for CalculateNumberOfTrips(start,end,min,max)
        /// that an ArgumentException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateNumberOfTrips_MaxLessThanMin()
        {
            string start = "A"; //any value works
            string end = "B";   //any value works
            int min = 4;    //must be greater than max
            int max = 2;    //must be less than min

            //the following call should result in an ArgumentException
            //being thrown so we do not need to assert any result
            _trainService.CalculateNumberOfTrips(start, end, min, max);
        }
    }
}