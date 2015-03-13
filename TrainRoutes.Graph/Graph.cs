using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TrainRoutes.Graph
{
    public class Graph<TValue> : IGraph<TValue> 
    {
        #region [Private Fields]

        private readonly NodeCollection<TValue> _nodeCollection;
        private bool _isDirected;

        #endregion

        #region [Public Properties]

        public NodeCollection<TValue> Nodes
        {
            get
            {
                return _nodeCollection;
            }
        }

        public int Count
        {
            get { return _nodeCollection.Count; }
        }

        public bool IsDirected
        {
            get { return _isDirected; }
            private set { _isDirected = value; }
        }

        #endregion

        #region [Constructors]

        public Graph()
        {
            _isDirected = true;
            _nodeCollection = new NodeCollection<TValue>();
        }
        public Graph(bool isDirected)
        {
            _isDirected = isDirected;
            _nodeCollection = new NodeCollection<TValue>();
        }
        public Graph(bool isDirected, NodeCollection<TValue> nodeCollection)
        {
            _isDirected = isDirected;
            _nodeCollection = nodeCollection ?? new NodeCollection<TValue>();
        }

        #endregion

        #region [Public Methods]
        public void LoadFromFile(string filePath)
        {
            var input = new List<string>();
            var reader = new StreamReader(File.OpenRead(filePath));
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                input.AddRange(values);
            }

            IsDirected = true;
            foreach (var value in input)
            {
                char[] route = value.Trim().ToCharArray();
                string start = route[0].ToString();
                string end = route[1].ToString();

                var startNode = new GraphNode<TValue>(start);
                var endNode = new GraphNode<TValue>(end);
                var distance = double.Parse(route[2].ToString());

                //we do not want to add the values again if they exist
                if (!Nodes.Any(p => p.Value.ToString() == start))
                    AddNode(startNode);
                else
                    startNode = Nodes.First(p => p.Value.ToString() == start);

                //we do not want to add the values again if they exist
                if (!Nodes.Any(p => p.Value.ToString() == end))
                    AddNode(endNode);
                else
                    endNode = Nodes.First(p => p.Value.ToString() == end);

                AddEdge(startNode, endNode, distance);
            }
        }

        public void AddNode(GraphNode<TValue> node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            // adds a node to the graph
            _nodeCollection.Add(node);
        }

        public void AddEdge(GraphNode<TValue> start, GraphNode<TValue> end)
        {
            if (start == null)
                throw new ArgumentNullException("start");

            if (end == null)
                throw new ArgumentNullException("end");

            if (_isDirected)
                AddDirectedEdge(start, end, 0);
            else
                AddUndirectedEdge(start, end, 0);
        }

        public void AddEdge(GraphNode<TValue> start, GraphNode<TValue> end, double cost)
        {
            if (_isDirected)
                AddDirectedEdge(start,end, cost);
            else
                AddUndirectedEdge(start,end, cost);
        }

        public void AddNode(TValue value)
        {
            // adds a node to the graph
            _nodeCollection.Add(new GraphNode<TValue>(value));
        }

        public bool Contains(TValue value)
        {
            return _nodeCollection.FindByValue(value) != null;
        }

        public bool Remove(TValue value)
        {
            // first remove the node from the nodeset
            GraphNode<TValue> nodeToRemove = (GraphNode<TValue>)_nodeCollection.FindByValue(value);
            if (nodeToRemove == null)
                // node wasn't found
                return false;

            // otherwise, the node was found
            _nodeCollection.Remove(nodeToRemove);

            // enumerate through each node in the nodeCollection, removing edges to this node
            foreach (GraphNode<TValue> gnode in _nodeCollection)
            {
                int index = gnode.Neighbors.IndexOf(nodeToRemove);
                if (index != -1)
                {
                    // remove the reference to the node and associated cost
                    gnode.Costs.Remove(nodeToRemove.Id);
                    gnode.Neighbors.RemoveAt(index);
                }
            }

            return true;
        }

        /// <summary>
        /// Load graph from list of and edges
        /// </summary>
        /// <param name="nodes">List of nodes</param>
        /// <param name="edges">List of edges</param>
        public void Load(IEnumerable<GraphNode<TValue>> nodes,IEnumerable<Edge<TValue>> edges)
        {
            //Load graph from list of nodes
            foreach (var node in nodes)
                AddNode(node);

            foreach (var edge in edges)
                AddEdge(edge.Start, edge.End, edge.Cost);
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _nodeCollection.ToList().Select(p => p.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region [Private/Protected Methods]

        private void AddDirectedEdge(GraphNode<TValue> from, GraphNode<TValue> to, double cost)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (to == null)
                throw new ArgumentNullException("to");

            from.Costs.Add(to.Id, cost);
            from.Neighbors.Add(to);
        }

        private void AddUndirectedEdge(GraphNode<TValue> from, GraphNode<TValue> to, double cost)
        {
            if (from == null)
                throw new ArgumentNullException("from");

            if (to == null)
                throw new ArgumentNullException("to");

            from.Neighbors.Add(to);
            from.Costs.Add(to.Id, cost);

            to.Neighbors.Add(from);
            to.Costs.Add(from.Id, cost);
        }

        #endregion

    }
}