using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Simple.Mocking;
using TrainRoutes.Graph;
using TrainRoutes.Service;

namespace TrainRoutes.Test
{
    [TestFixture]
    public class RouteProviderTests
    {
        //mock implementations
        private IGraph<string> _graph;

        //concrete implementations
        private IRouteProvider<string> _routeProvider;

        [SetUp]
        public void Setup()
        {
             //initialize test graph
            _graph = Mock.Interface<IGraph<string>>();

            //allow mock graph to response to AddNode(...) and AddEdge(...)
            Expect.MethodCall(() => _graph.AddNode(Any<GraphNode<string>>.Value));
            Expect.MethodCall(
                () => _graph.AddEdge(Any<GraphNode<string>>.Value, Any<GraphNode<string>>.Value, Any<double>.Value));

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

            //initialize concrete IRouteProvider<string>
            //and inject mock IGraph<string> implementation
            _routeProvider = new RouteProvider(_graph);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// when an empty route definition is passed to 
        /// CalculateDistance(...) that an ArgumentException
        /// is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateDistance_EmptyRouteDefinition()
        {
            string routeDefinition = string.Empty;//must be empty string

            //The following call should result in an ArgumentException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinition);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// when an null route definition is passed to 
        /// CalculateDistance(...) that an ArgumentNullException
        /// is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateDistance_NullRouteDefinition()
        {
            string routeDefinition = null;//must be null

            //The following call should result in an ArgumentException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinition);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// when a one stop route definition is passed to 
        /// CalculateDistance(...) that an InvalidOperationException
        /// is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CalculateDistance_OneStopRouteDefinition()
        {
            string route = "A";//must be one character

            //The following call should result in an InvalidOperationException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(route);

        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// when repeating route sotps are passed to 
        /// CalculateDistance(...) that an InvalidOperationException
        /// is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateDistance_RepeatedRouteStop()
        {
            string routeDefinition = "AA";

            //The following call should result in an InvalidOperationException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinition);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if an impossible route is provided with valid
        /// format route definition to CalculateDistance(...)
        /// that it will throw a InvalidOperationException
        /// </summary>
        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CalculateDistance_NoSuchRoute()
        {
            string routeDefinition = "CB";//this needs to be an impossible route

            //For this test, we need an actual initialized concrete graph to inject
            //into the route provider so we can test the integration logic between the two
            IRouteProvider<string> routeProvider = new RouteProvider(GetConcreteGraph());

            //The following call should result in an InvalidOperationException
            //being thrown so we do not need to assert any result
            routeProvider.CalculateDistance(routeDefinition);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if an impossible route is provided with valid
        /// format route definition to CalculateDistance(...)
        /// that it will throw a InvalidOperationException
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateDistance_NullCurrentNode()
        {
            string routeDefinition = "-CB";//this needs to be an impossible route

            //For this test, we need an actual initialized concrete graph to inject
            //into the route provider so we can test the integration logic between the two
            IRouteProvider<string> routeProvider = new RouteProvider(GetConcreteGraph());

            //The following call should result in an InvalidOperationException
            //being thrown so we do not need to assert any result
            routeProvider.CalculateDistance(routeDefinition);
        }

        /// <summary>
        /// The purpose of this test is to assert that
        /// if an impossible route is provided with valid
        /// format route definition to CalculateDistance(...)
        /// that it will throw a InvalidOperationException
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CalculateDistance_NullNextNode()
        {
            string routeDefinition = "C-B";//this needs to be an impossible route

            //For this test, we need an actual initialized concrete graph to inject
            //into the route provider so we can test the integration logic between the two
            IRouteProvider<string> routeProvider = new RouteProvider(GetConcreteGraph());

            //The following call should result in an InvalidOperationException
            //being thrown so we do not need to assert any result
            routeProvider.CalculateDistance(routeDefinition);
        }


        /// <summary>
        /// The purpose of this test is to assert that if an empty
        /// route definition list is provided to CalculateDistance(..)
        /// that an ArgumentException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof (ArgumentException))]
        public void CalculateDistance_EmptyRouteDefinitionList()
        {
            string[] routeDefinitions = new string[0];  //must be empty

            //The following call should result in an ArgumentException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinitions);
        }


        /// <summary>
        /// The purpose of this test is to assert that if a null
        /// route definition list is provided to CalculateDistance(..)
        /// that an ArgumentNullException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateDistance_NullRouteDefinitionList()
        {
            string[] routeDefinitions = null;  //must be empty

            //The following call should result in an ArgumentNullException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinitions);
        }

        /// <summary>
        /// The purpose of this test is to assert that if an empty
        /// route definition list is provided to CalculateDistance(..)
        /// that an ArgumentException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateDistance_EmptyGraphNodeRouteDefinitionList()
        {
            string[] routeDefinitions = new string[0];  //must be empty

            //The following call should result in an ArgumentException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinitions);
        }


        /// <summary>
        /// The purpose of this test is to assert that if a null
        /// route definition list is provided to CalculateDistance(..)
        /// that an ArgumentNullException is thrown
        /// </summary>
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CalculateDistance_NullGraphNodeRouteDefinitionList()
        {
            string[] routeDefinitions = null;  //must be empty

            //The following call should result in an ArgumentNullException
            //being thrown so we do not need to assert any result
            _routeProvider.CalculateDistance(routeDefinitions);
        }

















        /// <summary>
        /// This method is used for the functional tests for
        /// route provider that need to access concrete implementation
        /// of it to test end-to-end integration logic with IGraph<typeparamref name="string"/>
        /// </summary>
        /// <returns></returns>
        private IGraph<string> GetConcreteGraph()
        {
            //initialize test graph
            IGraph<string> graph = new Graph<string>();

            //initialize test nodes
            var a = new GraphNode<string>("A");
            var b = new GraphNode<string>("B");
            var c = new GraphNode<string>("C");
            var d = new GraphNode<string>("D");
            var e = new GraphNode<string>("E");

            //CA3,DA5,DC4,DB10,DE2,BA9,BD8,BE1

            graph.AddNode(a);
            graph.AddNode(b);
            graph.AddNode(c);
            graph.AddNode(d);
            graph.AddNode(e);

            graph.AddEdge(c, a, 3);
            graph.AddEdge(d, a, 5);
            graph.AddEdge(d, c, 4);
            graph.AddEdge(d, b, 10);
            graph.AddEdge(d, e, 2);
            graph.AddEdge(b, a, 9);
            graph.AddEdge(b, d, 8);
            graph.AddEdge(b, e, 1);

            return graph;
        }
    }
}
