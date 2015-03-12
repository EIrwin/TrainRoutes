using System;
using System.Collections;
using System.Collections.Generic;

namespace TrainRoutes.Graph
{
    public class Graph<TValue> : IGraph<TValue>
    {
        #region [Private Fields]

        private readonly NodeCollection<TValue> _nodeCollection;
        private readonly bool _isDirected;

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
        }

        #endregion

        #region [Constructors]

        public Graph(){}
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

        public void AddNode(GraphNode<TValue> node)
        {
            // adds a node to the graph
            _nodeCollection.Add(node);
        }

        public void AddEdge(GraphNode<TValue> start, GraphNode<TValue> end)
        {
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
                    gnode.Neighbors.RemoveAt(index);
                    gnode.Costs.RemoveAt(index);
                }
            }

            return true;
        }


        /// <summary>
        /// Load graph from file path
        /// </summary>
        /// <param name="path">Path to file</param>
        public void Load(string path)
        {
            //Load graph from a comme separated text file
        }

        /// <summary>
        /// Load graph from list of route definitions
        /// </summary>
        /// <param name="list">List of route definitions</param>
        public void Load(IEnumerable<string> list)
        {
            
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
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region [Private/Protected Methods]

        private void AddDirectedEdge(GraphNode<TValue> from, GraphNode<TValue> to, double cost)
        {
            from.NeighborCosts.Add(to.Id, cost);
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        private void AddUndirectedEdge(GraphNode<TValue> from, GraphNode<TValue> to, double cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);

            to.Neighbors.Add(from);
            to.Costs.Add(cost);
        }

        #endregion

    }
}