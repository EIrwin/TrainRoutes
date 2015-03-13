using NUnit.Framework;
using TrainRoutes.Graph;

namespace TrainRoutes.Test
{
    [TestFixture]
    public class GraphTests
    {
        private IGraph<string> _graph;
        
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
        }
    }
}
